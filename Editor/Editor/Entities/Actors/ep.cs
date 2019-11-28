using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
    public partial class ep
    {
        [WProperty("Torch", "Is Brazier Wooden", true, "If checked, and the Torch Type is Brazier, the brazier will be wooden.")]
        public bool IsBrazierWooden
        {
            get { return Convert.ToBoolean(IsWooden); }
            set
            {
                int converted_value = Convert.ToInt32(value);

                if (IsWooden != converted_value)
                {
                    IsWooden = converted_value;
                    OnPropertyChanged("IsBrazierWooden");
                }
            }
        }

        [WProperty("Torch", "Lit Switch ID", true, "The switch to set when the torch is lit, OR the switch that lights the torch when set.")]
        public byte LitSwitchID
        {
            get { return (byte)OnSwitch; }
            set
            {
                if (value != OnSwitch)
                {
                    OnSwitch = value;
                    OnPropertyChanged("LitSwitchID");
                }
            }
        }

        public override void PostLoad()
        {
            base.PostLoad();

            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 0.7f, 0.7f, 1f), true);
        }

        public override void PreSave()
		{

        }

        public override void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }
    }
}
