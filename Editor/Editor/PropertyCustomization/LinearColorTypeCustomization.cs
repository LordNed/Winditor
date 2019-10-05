using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using Xceed.Wpf.Toolkit;
using System.Windows.Data;
using WindEditor.WPF;

namespace WindEditor.Editor
{
    class LinearColorTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source, SourceScene source_scene)
        {
            WDetailSingleRowViewModel color_row = new WDetailSingleRowViewModel(display_name);

            ColorPicker colorpicker = new ColorPicker();
            colorpicker.IsEnabled = is_editable;

            Binding tbind = new Binding(property.Name)
            {
                Source = source,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = new LinearToSystemColorConverter()
            };

            colorpicker.SetBinding(ColorPicker.SelectedColorProperty, tbind);

            color_row.PropertyControl = colorpicker;

            return new List<WDetailSingleRowViewModel>() { color_row };
        }
    }
}
