using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace WindEditor.Editor
{
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
                cbox.Items.Add(enum_values.GetValue(i));
            }

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            cbox.SetBinding(WatermarkComboBox.SelectedItemProperty, tbind);

            enum_row.PropertyControl = cbox;

            return new List<WDetailSingleRowViewModel>() { enum_row };
        }
    }
}
