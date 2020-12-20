using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WindEditor.View;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace WindEditor.Editor
{
    class AdvancedBindingListTypeCustomization<T> : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel combo_row = new WDetailSingleRowViewModel(display_name);

            WAdvancedBindingListControl list_control = new WAdvancedBindingListControl<T>();
            list_control.IsEnabled = is_editable;

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            list_control.SetBinding(WAdvancedBindingListControl<T>.BoundListProperty, tbind);

            combo_row.PropertyControl = list_control;

            return new List<WDetailSingleRowViewModel>() { combo_row };
        }
    }
}
