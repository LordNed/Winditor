using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Editor.Modes
{
    public interface IEditorModeGizmo
    {
        WTransformGizmo TransformGizmo { get; set; }
        void UpdateGizmoTransform();
    }
}
