using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;
using System.Globalization;

namespace WindEditor.Editor
{
    public class EnumValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type enum_type = value.GetType();

            Array enum_values = Enum.GetValues(enum_type);

            for (int i = 0; i < enum_values.Length; i++)
            {
                if (enum_values.GetValue(i).Equals(value))
                {
                    return i;
                }
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Array enum_values = Enum.GetValues(targetType);

            return enum_values.GetValue((int)value);
        }
    }

    class EnumTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel enum_row = new WDetailSingleRowViewModel(display_name);

            WatermarkComboBox cbox = new WatermarkComboBox();
            cbox.IsEnabled = is_editable;

            Array enum_values = Enum.GetValues(property.PropertyType);

            for (int i = 0; i < enum_values.Length; i++)
            {
                string display_value = enum_values.GetValue(i).ToString();
                display_value = display_value.Replace("_", " ").Trim();
                cbox.Items.Add(display_value);
            }

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new EnumValueConverter()
            };

            cbox.SetBinding(WatermarkComboBox.SelectedIndexProperty, tbind);

            enum_row.PropertyControl = cbox;

            return new List<WDetailSingleRowViewModel>() { enum_row };
        }
    }
}
