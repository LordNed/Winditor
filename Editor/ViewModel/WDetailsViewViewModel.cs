using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public OrderedDictionary Categories
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

        private OrderedDictionary m_Categories;
        private Dictionary<string, IPropertyTypeCustomization> m_TypeCustomizations;

        public WDetailsViewViewModel()
        {
            m_Categories = new OrderedDictionary();
            m_TypeCustomizations = new Dictionary<string, IPropertyTypeCustomization>();

            m_TypeCustomizations.Add(typeof(string).Name, new StringTypeCustomization());
            m_TypeCustomizations.Add(typeof(int).Name, new IntegerTypeCustomization());
            m_TypeCustomizations.Add(typeof(byte).Name, new ByteTypeCustomization());
            m_TypeCustomizations.Add(typeof(short).Name, new ShortTypeCustomization());
            m_TypeCustomizations.Add(typeof(float).Name, new SingleTypeCustomization());
            m_TypeCustomizations.Add(typeof(WLinearColor).Name, new LinearColorTypeCustomization());
            m_TypeCustomizations.Add(typeof(WTransform).Name, new WTransformTypeCustomization());
            m_TypeCustomizations.Add(typeof(WDOMNode).Name, new WDOMNodeTypeCustomization());
        }

        public void ReflectObject(object obj)
        {
            OrderedDictionary new_details = new OrderedDictionary();

            HideCategoriesAttribute hidden_categories = (HideCategoriesAttribute)obj.GetType().GetCustomAttribute(typeof(HideCategoriesAttribute));
            PropertyInfo[] obj_properties = obj.GetType().GetProperties();

            foreach (PropertyInfo p in obj_properties)
            {
                CustomAttributeData[] custom_attributes = p.CustomAttributes.ToArray();
                if (custom_attributes.Length == 0 || custom_attributes[0].AttributeType.Name != "WProperty")
                    continue;

                string category_name = (string)custom_attributes[0].ConstructorArguments[0].Value;
                string property_name = (string)custom_attributes[0].ConstructorArguments[1].Value;
                bool is_editable = (bool)custom_attributes[0].ConstructorArguments[2].Value;

                Type base_type = p.PropertyType;
                while (base_type.BaseType != typeof(object))
                {
                    base_type = base_type.BaseType;
                }

                string type_name = p.PropertyType.Name;

                if (!new_details.Contains(category_name) && !hidden_categories.CategoryHidden(category_name))
                {
                    if (category_name == "Transform")
                    {
                        new_details.Insert(0, category_name, new WDetailsCategoryRowViewModel(category_name));
                    }
                    else
                        new_details.Add(category_name, new WDetailsCategoryRowViewModel(category_name));
                }

                WDetailsCategoryRowViewModel current_category = null;

                if (new_details.Contains(category_name))
                    current_category = (WDetailsCategoryRowViewModel)new_details[category_name];
                else
                    continue;

                if (m_TypeCustomizations.ContainsKey(type_name))
                {
                    List<WDetailSingleRowViewModel> property_rows = m_TypeCustomizations[type_name].CustomizeHeader(p, property_name, is_editable, obj);

                    // Saw online that adding multiple things to a binding list can be slow,
                    // so I'll do what that guy suggested. Disable raising changed events, then re-enable when we're done.
                    current_category.PropertyRows.RaiseListChangedEvents = false;

                    foreach (var row in property_rows)
                    {
                        current_category.PropertyRows.Add(row);
                    }

                    current_category.PropertyRows.RaiseListChangedEvents = true;
                    current_category.PropertyRows.ResetBindings();
                }
                else if (p.PropertyType.IsEnum)
                {
                    EnumTypeCustomization enu = new EnumTypeCustomization();
                    List<WDetailSingleRowViewModel> property_rows = enu.CustomizeHeader(p, property_name, is_editable, obj);

                    // Saw online that adding multiple things to a binding list can be slow,
                    // so I'll do what that guy suggested. Disable raising changed events, then re-enable when we're done.
                    current_category.PropertyRows.RaiseListChangedEvents = false;

                    foreach (var row in property_rows)
                    {
                        current_category.PropertyRows.Add(row);
                    }

                    current_category.PropertyRows.RaiseListChangedEvents = true;
                    current_category.PropertyRows.ResetBindings();
                }
                else if (base_type.Name == typeof(WDOMNode).Name)
                {
                    List<WDetailSingleRowViewModel> property_rows = m_TypeCustomizations[typeof(WDOMNode).Name].CustomizeHeader(p, property_name, is_editable, obj);

                    // Saw online that adding multiple things to a binding list can be slow,
                    // so I'll do what that guy suggested. Disable raising changed events, then re-enable when we're done.
                    current_category.PropertyRows.RaiseListChangedEvents = false;

                    foreach (var row in property_rows)
                    {
                        current_category.PropertyRows.Add(row);
                    }

                    current_category.PropertyRows.RaiseListChangedEvents = true;
                    current_category.PropertyRows.ResetBindings();
                }
                else
                {
                    current_category.PropertyRows.Add(new WDetailSingleRowViewModel(property_name));
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
        private bool IsEditable;

        public WProperty(string category, string name, bool editable)
        {
            Category = category;
            DisplayName = name;
            IsEditable = editable;
        }
    }

    public class HideCategoriesAttribute : System.Attribute
    {
        private string[] HiddenCategories;

        public HideCategoriesAttribute()
        {
            HiddenCategories = new string[1];
        }

        public HideCategoriesAttribute(string[] categories)
        {
            HiddenCategories = categories;
        }

        public bool CategoryHidden(string cat)
        {
            return HiddenCategories.Contains(cat);
        }
    }
}
