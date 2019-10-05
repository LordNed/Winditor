using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;
using WindEditor.View;

namespace WindEditor.Editor
{
    class FileReferenceTypeCustomization : IPropertyTypeCustomization
    {
        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source, SourceScene source_scene)
        {
            WDetailSingleRowViewModel file_row = new WDetailSingleRowViewModel(display_name);

            FileReferenceControl fileref = new FileReferenceControl();
            fileref.IsEnabled = is_editable;

            FileReference thing = property.GetValue(source) as FileReference;

            Binding tbind = new Binding("FilePath")
            {
                Source = thing,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            fileref.SetBinding(FileReferenceControl.FileNameProperty, tbind);

            file_row.PropertyControl = fileref;

            return new List<WDetailSingleRowViewModel>() { file_row };
        }
    }
}
