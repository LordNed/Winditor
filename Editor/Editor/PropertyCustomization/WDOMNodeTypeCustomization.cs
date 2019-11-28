using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;
using WindEditor.View;
using System.Globalization;

namespace WindEditor.Editor
{
    public class WDOMNodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            Type base_type = value.GetType();
            while (base_type.BaseType != null)
            {
                if (base_type == targetType)
                {
                    return value as WDOMNode;
                }

                base_type = base_type.BaseType;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType() == targetType)
            {
                return value;
            }

            return null;
        }
    }

    class WDOMNodeTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel ref_row = new WDetailSingleRowViewModel(display_name);

            WActorReferenceControl ref_ctrl = new WActorReferenceControl();
            ref_ctrl.IsEnabled = is_editable;

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new WDOMNodeConverter()
            };

            ref_ctrl.SetBinding(WActorReferenceControl.ActorReferenceProperty, tbind);

            ref_row.PropertyControl = ref_ctrl;

            return new List<WDetailSingleRowViewModel>() { ref_row };
        }
    }
}
