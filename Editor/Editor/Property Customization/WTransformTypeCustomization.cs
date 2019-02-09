using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor.Editor
{
    class WTransformTypeCustomization : IPropertyTypeCustomization
    {
        void IPropertyTypeCustomization.CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        List<WDetailSingleRowViewModel> IPropertyTypeCustomization.CustomizeHeader(PropertyInfo property, string display_name, object source)
        {
            WTransform transform = (WTransform)property.GetValue(source);

            return new List<WDetailSingleRowViewModel>() { new WDetailSingleRowViewModel("Translation"), new WDetailSingleRowViewModel("Rotation"),
            new WDetailSingleRowViewModel("Scale")};
        }
    }
}
