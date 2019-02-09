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
    class StringTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, object source)
        {
            WDetailSingleRowViewModel textbox_row = new WDetailSingleRowViewModel(display_name);

            TextBox tbox = new TextBox();

            Binding tbind = new Binding(property.Name);
            tbind.Source = source;
            tbind.Mode = BindingMode.TwoWay;
            tbind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            tbox.SetBinding(TextBox.TextProperty, tbind);

            textbox_row.PropertyControl = tbox;

            return new List<WDetailSingleRowViewModel>() { textbox_row };
        }
    }
}
