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
    class SingleTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, object source)
        {
            WDetailSingleRowViewModel single_row = new WDetailSingleRowViewModel(display_name);

            SingleUpDown singleupdown = new SingleUpDown();

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };

            singleupdown.SetBinding(SingleUpDown.ValueProperty, tbind);

            single_row.PropertyControl = singleupdown;

            return new List<WDetailSingleRowViewModel>() { single_row };
        }
    }
}
