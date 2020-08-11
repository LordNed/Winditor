using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;
using JStudio.J3D;

namespace WindEditor
{
	public partial class tn
	{
		public override void PostLoad()
		{
            UpdateModel();
            base.PostLoad();
		}

		public override void PreSave()
		{

        }

        private void UpdateModel()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Darknut");
            if (m_actorMeshes.Count == 0)
                return;

            int colorAnimFrame = (int)Color;
            if (colorAnimFrame > 5)
                colorAnimFrame = 5;

            var bodyModel = m_actorMeshes[0];
            bodyModel.CurrentRegisterAnimation.SetCurrentFrame(colorAnimFrame);
            bodyModel.Tick(0); // Update changed materials

            var armorModel = WResourceManager.LoadActorResource("Darknut Body Armor")[0];
            armorModel.CurrentRegisterAnimation.SetCurrentFrame(colorAnimFrame);
            armorModel.Tick(0); // Update changed materials
            bodyModel.AddChildModel(armorModel, "j_tn_mune1");

            J3D helmetModel;
            if (ExtraEquipment == ExtraEquipmentEnum.Helmet || ExtraEquipment == ExtraEquipmentEnum.Helmet_and_shield || ExtraEquipment >= ExtraEquipmentEnum.Helmet_shield_and_cape)
            {
                helmetModel = WResourceManager.LoadActorResource("Darknut Full Helmet")[0];
            } else
            {
                helmetModel = WResourceManager.LoadActorResource("Darknut Basic Helmet")[0];
            }
            helmetModel.CurrentRegisterAnimation.SetCurrentFrame(colorAnimFrame);
            helmetModel.Tick(0); // Update changed materials
            bodyModel.AddChildModel(helmetModel, "j_tn_atama1");

            var swordModel = WResourceManager.LoadActorResource("Darknut Sword")[0];
            bodyModel.AddChildModel(swordModel, "j_tn_item_r1");

            if (ExtraEquipment >= ExtraEquipmentEnum.Shield)
            {
                var shieldModel = WResourceManager.LoadActorResource("Darknut Shield")[0];
                bodyModel.AddChildModel(shieldModel, "j_tn_item_l1");
            }
        }
    }
}
