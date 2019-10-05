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
    class IntegerTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source, SourceScene source_scene)
        {
            WDetailSingleRowViewModel integer_row = new WDetailSingleRowViewModel(display_name);

            IntegerUpDown integerupdown = new IntegerUpDown();
            integerupdown.IsEnabled = is_editable;

            Binding tbind = new Binding(property.Name);
            tbind.Source = source;
            tbind.Mode = BindingMode.TwoWay;
            tbind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            integerupdown.SetBinding(IntegerUpDown.ValueProperty, tbind);

            integer_row.PropertyControl = integerupdown;

            return new List<WDetailSingleRowViewModel>() { integer_row };
        }
    }
}
