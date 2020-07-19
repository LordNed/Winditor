using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using Xceed.Wpf.Toolkit;
using System.Windows.Data;

namespace WindEditor.Editor
{
    class ShortTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel short_row = new WDetailSingleRowViewModel(display_name);

            ShortUpDown shortupdown = new ShortUpDown();
            shortupdown.IsEnabled = is_editable;

            Binding tbind = new Binding(property.Name);
            tbind.Source = source;
            tbind.Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay;
            tbind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            shortupdown.SetBinding(ShortUpDown.ValueProperty, tbind);

            short_row.PropertyControl = shortupdown;

            return new List<WDetailSingleRowViewModel>() { short_row };
        }
    }
}
