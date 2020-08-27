using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WindEditor.View;
using System.Windows.Data;
using System.Globalization;
using WindEditor.Events;

namespace WindEditor.Editor
{
    public class CutConverter : IValueConverter
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
                    return value as Cut;
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

    class CutTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel cut_row = new WDetailSingleRowViewModel(display_name);
            Event ev = source as Event;

            CutReferenceControl cut_ctrl = new CutReferenceControl();
            cut_ctrl.IsEnabled = is_editable;

            Binding cbind = new Binding("Actors")
            {
                Source = source,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            cut_ctrl.SetBinding(CutReferenceControl.StaffCollectionReferenceProperty, cbind);

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new CutConverter()
            };
            cut_ctrl.SetBinding(CutReferenceControl.CutReferenceProperty, tbind);

            cut_row.PropertyControl = cut_ctrl;
            cut_ctrl.SetReferences();

            return new List<WDetailSingleRowViewModel>() { cut_row };
        }
    }
}
