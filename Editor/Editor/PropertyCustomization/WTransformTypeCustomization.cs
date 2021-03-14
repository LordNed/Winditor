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

        List<WDetailSingleRowViewModel> IPropertyTypeCustomization.CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WTransformControl pos_ctrl = new WTransformControl();
            WTransformControl rot_ctrl = new WTransformControl();
            WTransformControl scale_ctrl = new WTransformControl();

            WTransform transform = property.GetValue(source) as WTransform;

            Binding pos_bind = new Binding("PositionBase")
            {
                Source = transform,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            pos_ctrl.SetBinding(WTransformControl.Vector3Property, pos_bind);

            Binding rot_bind = new Binding("RotationBase")
            {
                Source = transform,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            rot_ctrl.SetBinding(WTransformControl.Vector3Property, rot_bind);

            Binding scale_bind = new Binding("ScaleBase")
            {
                Source = transform,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            scale_ctrl.SetBinding(WTransformControl.Vector3Property, scale_bind);

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
