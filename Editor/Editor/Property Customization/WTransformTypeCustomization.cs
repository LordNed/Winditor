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
    class WTransformTypeCustomization : IPropertyTypeCustomization
    {
        void IPropertyTypeCustomization.CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        List<WDetailSingleRowViewModel> IPropertyTypeCustomization.CustomizeHeader(PropertyInfo property, string display_name, object source)
        {
            WTransformControl pos_ctrl = new WTransformControl();
            WTransformControl scale_ctrl = new WTransformControl();
            WTransformControl rot_ctrl = new WTransformControl();

            WTransform transform = property.GetValue(source) as WTransform;

            Binding pos_bind = new Binding("X");
            pos_bind.Source = transform.Position;
            pos_bind.Mode = BindingMode.TwoWay;
            pos_bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            pos_ctrl.XUpDown.SetBinding(SingleUpDown.ValueProperty, pos_bind);

            WDetailSingleRowViewModel pos_row = new WDetailSingleRowViewModel("Position");
            pos_row.PropertyControl = pos_ctrl;
            WDetailSingleRowViewModel rot_row = new WDetailSingleRowViewModel("Rotation");
            rot_row.PropertyControl = rot_ctrl;
            WDetailSingleRowViewModel scale_row = new WDetailSingleRowViewModel("Scale");
            scale_row.PropertyControl = scale_ctrl;

            return new List<WDetailSingleRowViewModel>() { pos_row, rot_row, scale_row };
        }
    }
}
