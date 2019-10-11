using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using WindEditor.Editor;
using WindEditor.View;
using Xceed.Wpf.Toolkit;

namespace WindEditor.ViewModel
{
    public class WDetailsViewViewModel : INotifyPropertyChanged
    {
        private OrderedDictionary m_Categories;
        private Dictionary<string, IPropertyTypeCustomization> m_TypeCustomizations;

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

        public WDetailsViewViewModel()
        {
            m_Categories = new OrderedDictionary();
            m_TypeCustomizations = new Dictionary<string, IPropertyTypeCustomization>();

            RegisterDefaultCustomizations();
        }

        private void RegisterDefaultCustomizations()
        {
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
                if (p.PropertyType != typeof(WProperty_new) && p.PropertyType.BaseType != typeof(WProperty_new))
                    continue;

                WProperty_new wprop_instance = (WProperty_new)p.GetValue(obj);
                PropertyInfo[] wprops = p.PropertyType.GetProperties();

                // Grab our property values to use
                string category_name = (string)wprops.First(x => x.Name == "CategoryName").GetValue(wprop_instance);
                string property_name = (string)wprops.First(x => x.Name == "PropertyName").GetValue(wprop_instance);
                string display_name  = (string)wprops.First(x => x.Name == "DisplayName").GetValue(wprop_instance);
                string tool_tip      = (string)wprops.First(x => x.Name == "ToolTip").GetValue(wprop_instance);
                bool is_visible      = (bool)wprops.First(x => x.Name == "Visible").GetValue(wprop_instance);

                PropertyInfo base_prop_info = obj_properties.First(x => x.Name == property_name);
                object base_prop_instance = base_prop_info.GetValue(obj);

                Type prop_type = base_prop_info.PropertyType;

                // If this property is currently invisible, don't generate a control for it.
                if (!is_visible)
                {
                    return;
                }

                // Only add the category to the view if it's not blacklisted by the HideCategories attribute.
                if (!new_details.Contains(category_name) && hidden_categories != null && !hidden_categories.CategoryHidden(category_name))
                {
                    // Also, we want to force the Transform category to the top of the list.
                    if (category_name == "Transform")
                    {
                        new_details.Insert(0, category_name, new WDetailsCategoryRowViewModel(category_name));
                    }
                    else
                    {
                        new_details.Add(category_name, new WDetailsCategoryRowViewModel(category_name));
                    }
                }

                WDetailsCategoryRowViewModel current_category = null;

                if (new_details.Contains(category_name))
                    current_category = (WDetailsCategoryRowViewModel)new_details[category_name];
                else
                    continue;
                
                /* This is where we generate the control for the details view. */

                List<WDetailSingleRowViewModel> property_rows = new List<WDetailSingleRowViewModel>();

                switch (prop_type.Name)
                {
                    case "int":
                        WIntProperty wprop_as_int = (WIntProperty)wprop_instance;

                        property_rows = m_TypeCustomizations[prop_type.Name].CustomizeHeader(base_prop_info, property_name, true, obj);
                        IntegerUpDown intupdown = (IntegerUpDown)property_rows[0].PropertyControl;
                        intupdown.Increment = wprop_as_int.Increment;
                        break;
                    case "uint":
                        WIntProperty wprop_as_uint = (WIntProperty)wprop_instance;

                        property_rows = m_TypeCustomizations[prop_type.Name].CustomizeHeader(base_prop_info, property_name, true, obj);
                        UIntegerUpDown uintupdown = (UIntegerUpDown)property_rows[0].PropertyControl;
                        uintupdown.Increment = (uint)wprop_as_uint.Increment;
                        break;
                    case "WDOMNode":
                        WActorReferenceProperty wprop_as_ref = (WActorReferenceProperty)wprop_instance;

                        property_rows = m_TypeCustomizations[prop_type.Name].CustomizeHeader(base_prop_info, property_name, true, obj);
                        WActorReferenceControl refcontrol = (WActorReferenceControl)property_rows[0].PropertyControl;
                        refcontrol.Source = wprop_as_ref.SourceScene;
                        refcontrol.FillComboBox();
                        break;
                    default:
                        // We first check if the type of the property has a customization registered.
                        // If it is, we just grab the customization and generate a control with it.
                        if (m_TypeCustomizations.ContainsKey(prop_type.Name))
                        {
                            property_rows = m_TypeCustomizations[prop_type.Name].CustomizeHeader(base_prop_info, property_name, true, obj);
                        }
                        // If there is no customization registered, and the type is an enum, we
                        // try to use EnumTypeCustomization to generate a control.
                        else if (prop_type.IsEnum)
                        {
                            EnumTypeCustomization enu = new EnumTypeCustomization();
                            property_rows = enu.CustomizeHeader(base_prop_info, property_name, true, obj);
                        }
                        else
                        {
                            property_rows.Add(new WDetailSingleRowViewModel(property_name));
                        }
                        break;
                }

                /*
                // We first check if the type of the property has a customization registered.
                // If it is, we just grab the customization and generate a control with it.
                if (m_TypeCustomizations.ContainsKey(prop_type.Name))
                {
                    property_rows = m_TypeCustomizations[prop_type.Name].CustomizeHeader(base_prop_info, property_name, true, obj);
                }
                // If there is no customization registered, and the type is an enum, we
                // use EnumTypeCustomization to generate a control.
                else if (prop_type.IsEnum)
                {
                    EnumTypeCustomization enu = new EnumTypeCustomization();
                    property_rows = enu.CustomizeHeader(p, property_name, true, obj);
                }
                // Failing the prior checks, we see if the base type of the property is WDOMNode,
                // in which case we just use the WDOMNode customization to generate a control.
                else if (prop_type.Name == typeof(WDOMNode).Name)
                {
                    property_rows = m_TypeCustomizations[typeof(WDOMNode).Name].CustomizeHeader(base_prop_info, property_name, true, obj);

                    WActorReferenceControl c = (WActorReferenceControl)property_rows[0].PropertyControl;
                    //c.Source = source_scene;
                    c.FillComboBox();
                }
                // If the property type is completely unknown or unsupported, we create an empty row with
                // just the property's name.
                else
                {
                    property_rows.Add(new WDetailSingleRowViewModel(property_name));
                }
                */

                // Saw online that adding multiple things to a binding list can be slow,
                // so I'll do what that guy suggested. Disable raising changed events, then re-enable when we're done.
                current_category.PropertyRows.RaiseListChangedEvents = false;

                foreach (var row in property_rows)
                {
                    current_category.PropertyRows.Add(row);
                    row.PropertyToolTip = tool_tip;
                }

                current_category.PropertyRows.RaiseListChangedEvents = true;
                current_category.PropertyRows.ResetBindings();
            }

            Categories = new_details;
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
