using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using WindEditor.Editor;
using WindEditor.View;
using WindEditor.Events;

namespace WindEditor.ViewModel
{
    public class WDetailsViewViewModel : INotifyPropertyChanged
    {
        private OrderedDictionary m_Categories;
        private static Dictionary<string, IPropertyTypeCustomization> m_TypeCustomizations;

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

        public Dictionary<string, IPropertyTypeCustomization> TypeCustomizations
        {
            get { return m_TypeCustomizations; }
            set
            {
                if (value != m_TypeCustomizations)
                {
                    m_TypeCustomizations = value;
                    OnPropertyChanged("TypeCustomizations");
                }
            }
        }

        static WDetailsViewViewModel()
        {
            m_TypeCustomizations = new Dictionary<string, IPropertyTypeCustomization>();

            m_TypeCustomizations.Add(typeof(string).Name, new StringTypeCustomization());
            m_TypeCustomizations.Add(typeof(int).Name, new IntegerTypeCustomization());
            m_TypeCustomizations.Add(typeof(uint).Name, new UIntegerTypeCustomization());
            m_TypeCustomizations.Add(typeof(byte).Name, new ByteTypeCustomization());
            m_TypeCustomizations.Add(typeof(short).Name, new ShortTypeCustomization());
            m_TypeCustomizations.Add(typeof(ushort).Name, new UShortTypeCustomization());
            m_TypeCustomizations.Add(typeof(float).Name, new SingleTypeCustomization());
            m_TypeCustomizations.Add(typeof(bool).Name, new BooleanTypeCustomization());
            m_TypeCustomizations.Add(typeof(WLinearColor).Name, new LinearColorTypeCustomization());
            m_TypeCustomizations.Add(typeof(WTransform).Name, new WTransformTypeCustomization());
            m_TypeCustomizations.Add(typeof(WDOMNode).Name, new WDOMNodeTypeCustomization());
            m_TypeCustomizations.Add(typeof(FileReference).Name, new FileReferenceTypeCustomization());
            m_TypeCustomizations.Add(typeof(Cut).Name, new CutTypeCustomization());
        }

        public WDetailsViewViewModel()
        {
            m_Categories = new OrderedDictionary();
        }

        public void ReflectObject(object obj)
        {
            OrderedDictionary new_details = new OrderedDictionary();

            if (obj == null)
            {
                Categories = new_details;
                return;
            }

            HideCategoriesAttribute hidden_categories = (HideCategoriesAttribute)obj.GetType().GetCustomAttribute(typeof(HideCategoriesAttribute));
            PropertyInfo[] obj_properties = obj.GetType().GetProperties();

            foreach (PropertyInfo p in obj_properties)
            {
                // We want to ignore all properties that are not marked with the WProperty attribute.
                CustomAttributeData[] custom_attributes = p.CustomAttributes.ToArray();
                CustomAttributeData wproperty_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "WProperty");
                if (wproperty_attribute == null)
                    continue;
                
                // Grab our custom attribute data for use
                string category_name = (string)wproperty_attribute.ConstructorArguments[0].Value;
                string property_name = (string)wproperty_attribute.ConstructorArguments[1].Value;
                bool is_editable     =   (bool)wproperty_attribute.ConstructorArguments[2].Value;
                string tool_tip      = (string)wproperty_attribute.ConstructorArguments[3].Value;
                SourceScene source_scene = (SourceScene)wproperty_attribute.ConstructorArguments[4].Value;

                // Get the base type for possible use later
                Type base_type = p.PropertyType;
                while (base_type.BaseType != typeof(object))
                {
                    base_type = base_type.BaseType;
                }

                // Only add the category to the view if it's not blacklisted by the HideCategories attribute.
                if (!new_details.Contains(category_name) && (hidden_categories == null || !hidden_categories.CategoryHidden(category_name)))
                {
                    new_details.Add(category_name, new WDetailsCategoryRowViewModel(category_name));
                }

                WDetailsCategoryRowViewModel current_category = null;

                if (new_details.Contains(category_name))
                    current_category = (WDetailsCategoryRowViewModel)new_details[category_name];
                else
                    continue;
                
                /* This is where we generate the control for the details view. */

                List<WDetailSingleRowViewModel> property_rows = new List<WDetailSingleRowViewModel>();

                // We first check if the type of the property has a customization registered.
                // If it is, we just grab the customization and generate a control with it.
                if (m_TypeCustomizations.ContainsKey(p.PropertyType.Name))
                {
                    property_rows = m_TypeCustomizations[p.PropertyType.Name].CustomizeHeader(p, property_name, is_editable, obj);
                }
                // If there is no customization registered, and the type is an enum, we
                // use EnumTypeCustomization to generate a control.
                else if (p.PropertyType.IsEnum)
                {
                    EnumTypeCustomization enu = new EnumTypeCustomization();
                    property_rows = enu.CustomizeHeader(p, property_name, is_editable, obj);
                }
                // Check if the type is an AndvancedBindingList.
                else if (p.PropertyType.Name == typeof(AdvancedBindingList<object>).Name)
                {
                    Type underlying_type = p.PropertyType.GetGenericArguments().Single();
                    Type type_from_generic = typeof(AdvancedBindingListTypeCustomization<>).MakeGenericType(underlying_type);

                    IPropertyTypeCustomization adv_cus = Activator.CreateInstance(type_from_generic) as IPropertyTypeCustomization;
                    property_rows = adv_cus.CustomizeHeader(p, property_name, is_editable, obj);

                    WAdvancedBindingListControl base_ctrl = property_rows[0].PropertyControl as WAdvancedBindingListControl;
                    base_ctrl.entry_combo.SelectedIndex = 0;
                    property_rows.AddRange(base_ctrl.GenerateBoundFields());
                    base_ctrl.OnEntryComboSelectionChanged();
                }
                // Failing the prior checks, we see if the base type of the property is WDOMNode,
                // in which case we just use the WDOMNode customization to generate a control.
                else if (base_type.Name == typeof(WDOMNode).Name)
                {
                    property_rows = m_TypeCustomizations[typeof(WDOMNode).Name].CustomizeHeader(p, property_name, is_editable, obj);

                    WActorReferenceControl c = (WActorReferenceControl)property_rows[0].PropertyControl;
                    c.Source = source_scene;
                    c.FillComboBox();
                }
                // If the property type is completely unknown or unsupported, we create an empty row with
                // just the property's name.
                else
                {
                    property_rows.Add(new WDetailSingleRowViewModel(property_name));
                }

                // Saw online that adding multiple things to a binding list can be slow,
                // so I'll do what that guy suggested. Disable raising changed events, then re-enable when we're done.
                current_category.PropertyRows.RaiseListChangedEvents = false;

                foreach (var row in property_rows)
                {
                    current_category.PropertyRows.Add(row);

                    if (tool_tip != "")
                        row.PropertyToolTip = tool_tip;
                }

                current_category.PropertyRows.RaiseListChangedEvents = true;
                current_category.PropertyRows.ResetBindings();
            }

            // We want to force certain categories to the top of the list, so reorder it at the end.
            foreach (var cat_name in new string[] { "Actor", "Transform", "Entity" })
            {
                if (new_details.Contains(cat_name))
                {
                    var row = new_details[cat_name];
                    new_details.Remove(cat_name);
                    new_details.Insert(0, cat_name, row);
                }
            }

            Categories = new_details;
        }

        public static List<WDetailSingleRowViewModel> GeneratePropertyRows(Type type)
        {
            List<WDetailSingleRowViewModel> property_rows = new List<WDetailSingleRowViewModel>();

            HideCategoriesAttribute hidden_categories = (HideCategoriesAttribute)type.GetCustomAttribute(typeof(HideCategoriesAttribute));
            PropertyInfo[] obj_properties = type.GetProperties();

            List<string> tooltips = new List<string>();

            foreach (PropertyInfo p in obj_properties)
            {
                // We want to ignore all properties that are not marked with the WProperty attribute.
                CustomAttributeData[] custom_attributes = p.CustomAttributes.ToArray();
                CustomAttributeData wproperty_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "WProperty");
                if (wproperty_attribute == null)
                    continue;

                // Grab our custom attribute data for use
                string category_name = (string)wproperty_attribute.ConstructorArguments[0].Value;
                string property_name = (string)wproperty_attribute.ConstructorArguments[1].Value;
                bool is_editable = (bool)wproperty_attribute.ConstructorArguments[2].Value;
                string tool_tip = (string)wproperty_attribute.ConstructorArguments[3].Value;
                SourceScene source_scene = (SourceScene)wproperty_attribute.ConstructorArguments[4].Value;

                tooltips.Add(tool_tip);

                // Get the base type for possible use later
                Type base_type = p.PropertyType;
                while (base_type.BaseType != typeof(object))
                {
                    base_type = base_type.BaseType;
                }

                /* This is where we generate the control for the details view. */

                // We first check if the type of the property has a customization registered.
                // If it is, we just grab the customization and generate a control with it.
                if (m_TypeCustomizations.ContainsKey(p.PropertyType.Name))
                {
                    property_rows.AddRange(m_TypeCustomizations[p.PropertyType.Name].CustomizeHeader(p, property_name, is_editable, null));
                }
                // If there is no customization registered, and the type is an enum, we
                // use EnumTypeCustomization to generate a control.
                else if (p.PropertyType.IsEnum)
                {
                    EnumTypeCustomization enu = new EnumTypeCustomization();
                    property_rows.AddRange(enu.CustomizeHeader(p, property_name, is_editable, null));
                }
                // Check if the type is an AndvancedBindingList.
                else if (p.PropertyType.Name == typeof(AdvancedBindingList<object>).Name)
                {
                    Type underlying_type = p.PropertyType.GetGenericArguments().Single();
                    Type type_from_generic = typeof(AdvancedBindingListTypeCustomization<>).MakeGenericType(underlying_type);

                    IPropertyTypeCustomization adv_cus = Activator.CreateInstance(type_from_generic) as IPropertyTypeCustomization;
                    property_rows = adv_cus.CustomizeHeader(p, property_name, is_editable, null);

                    WAdvancedBindingListControl base_ctrl = property_rows[0].PropertyControl as WAdvancedBindingListControl;
                    property_rows.AddRange(base_ctrl.GenerateBoundFields());
                }
                // Failing the prior checks, we see if the base type of the property is WDOMNode,
                // in which case we just use the WDOMNode customization to generate a control.
                else if (base_type.Name == typeof(WDOMNode).Name)
                {
                    property_rows.AddRange(m_TypeCustomizations[typeof(WDOMNode).Name].CustomizeHeader(p, property_name, is_editable, null));

                    WActorReferenceControl c = (WActorReferenceControl)property_rows[0].PropertyControl;
                    c.Source = source_scene;
                    c.FillComboBox();
                }
                // If the property type is completely unknown or unsupported, we create an empty row with
                // just the property's name.
                else
                {
                    property_rows.Add(new WDetailSingleRowViewModel(property_name));
                }
            }

            for (int i = 0; i < property_rows.Count; i++)
            {
                property_rows[i].PropertyToolTip = tooltips[i];
            }

            return property_rows;
        }

        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
