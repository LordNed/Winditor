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
    class ByteTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel byte_row = new WDetailSingleRowViewModel(display_name);

            ByteUpDown byteupdown = new ByteUpDown();
            byteupdown.IsEnabled = is_editable;

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            byteupdown.SetBinding(ByteUpDown.ValueProperty, tbind);

            byte_row.PropertyControl = byteupdown;

            return new List<WDetailSingleRowViewModel>() { byte_row };
        }
    }
}
