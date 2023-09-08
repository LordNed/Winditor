using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class ikari
    {
        public override void PostLoad()
        {
            UpdateModel();
        }

        private void UpdateModel()
        {
            if (Model == ModelEnum.Three_Prongs)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Three-Pronged Anchor");
            }
            else if (Model == ModelEnum.Four_Prongs)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Four-Pronged Anchor");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Two-Pronged Anchor");
            }

            if (Size == SizeEnum.Big)
            {
                VisualScaleMultiplier = new Vector3(1.27f);
            } else
            {
                VisualScaleMultiplier = Vector3.One;
            }
        }

        protected override Vector3 VisualScale
        {
            get
            {
                if (Size == SizeEnum.Scalable)
                {
                    return Vector3.Multiply(Transform.LocalScale, VisualScaleMultiplier);
                } else
                {
                    // Not affected by local scale.
                    return VisualScaleMultiplier;
                }
            }
        }
    }
}
