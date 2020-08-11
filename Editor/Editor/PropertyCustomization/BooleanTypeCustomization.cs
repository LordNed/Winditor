using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace WindEditor.Editor
{
    class BooleanTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel bool_row = new WDetailSingleRowViewModel(display_name);

            CheckBox c_box = new CheckBox();
            c_box.IsEnabled = is_editable;

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            c_box.SetBinding(CheckBox.IsCheckedProperty, tbind);

            bool_row.PropertyControl = c_box;

            return new List<WDetailSingleRowViewModel>() { bool_row };
        }
    }
}
