using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WindEditor.Editor;

namespace WindEditor.ViewModel
{
    public class WDetailsViewViewModel : INotifyPropertyChanged
    {
        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, WDetailsCategoryRowViewModel> Categories
        {
            get { return m_Categories; }
            set
            {
                if (value != m_Categories)
                {
                    m_Categories = value;
                    OnPropertyChanged("Categories");
                }
            }
        }

        private Dictionary<string, WDetailsCategoryRowViewModel> m_Categories;
        private Dictionary<string, IPropertyTypeCustomization> m_TypeCustomizations;

        public WDetailsViewViewModel()
        {
            m_Categories = new Dictionary<string, WDetailsCategoryRowViewModel>();
            m_TypeCustomizations = new Dictionary<string, IPropertyTypeCustomization>();

            m_TypeCustomizations.Add(typeof(string).Name, new StringTypeCustomization());
            m_TypeCustomizations.Add(typeof(int).Name, new IntegerTypeCustomization());
            m_TypeCustomizations.Add(typeof(byte).Name, new ByteTypeCustomization());
            m_TypeCustomizations.Add(typeof(short).Name, new ShortTypeCustomization());
            m_TypeCustomizations.Add(typeof(float).Name, new SingleTypeCustomization());
            m_TypeCustomizations.Add(typeof(WLinearColor).Name, new LinearColorTypeCustomization());
            m_TypeCustomizations.Add(typeof(WTransform).Name, new WTransformTypeCustomization());
        }

        public void ReflectObject(object obj)
        {
            Dictionary<string, WDetailsCategoryRowViewModel> new_details = new Dictionary<string, WDetailsCategoryRowViewModel>();

            PropertyInfo[] obj_properties = obj.GetType().GetProperties();

            foreach (PropertyInfo p in obj_properties)
            {
                CustomAttributeData[] custom_attributes = p.CustomAttributes.ToArray();
                if (custom_attributes.Length == 0 || custom_attributes[0].AttributeType.Name != "WProperty")
                    continue;

                string category_name = (string)custom_attributes[0].ConstructorArguments[0].Value;
                string property_name = (string)custom_attributes[0].ConstructorArguments[1].Value;
                string type_name = p.PropertyType.Name;

                if (!new_details.ContainsKey(category_name))
                    new_details.Add(category_name, new WDetailsCategoryRowViewModel(category_name));

                if (m_TypeCustomizations.ContainsKey(type_name))
                {
                    List<WDetailSingleRowViewModel> property_rows = m_TypeCustomizations[type_name].CustomizeHeader(p, property_name, obj);

                    // Saw online that adding multiple things to a binding list can be slow,
                    // so I'll do what that guy suggested. Disable raising changed events, then re-enable when we're done.
                    new_details[category_name].PropertyRows.RaiseListChangedEvents = false;

                    foreach (var row in property_rows)
                    {
                        new_details[category_name].PropertyRows.Add(row);
                    }

                    new_details[category_name].PropertyRows.RaiseListChangedEvents = true;
                    new_details[category_name].PropertyRows.ResetBindings();
                }
                else
                {
                    new_details[category_name].PropertyRows.Add(new WDetailSingleRowViewModel(property_name));
                }
            }

            Categories = new_details;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WProperty : System.Attribute
    {
        private string Category;
        private string DisplayName;

        public WProperty(string category, string name)
        {
            Category = category;
            DisplayName = name;
        }
    }
}
