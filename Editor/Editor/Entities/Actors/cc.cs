using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class cc
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
            switch (ColorType)
            {
                case ColorTypeEnum.Green:
                case ColorTypeEnum.Green_and_attacks_instantly:
                    m_actorMeshes = WResourceManager.LoadActorResource("Green ChuChu");
                    break;
                case ColorTypeEnum.Red:
                case ColorTypeEnum.Red_and_attacks_instantly:
                case ColorTypeEnum.Red_and_attacks_instantly_and_more_vulnerabilities:
                    m_actorMeshes = WResourceManager.LoadActorResource("Red ChuChu");
                    break;
                case ColorTypeEnum.Blue:
                case ColorTypeEnum.Blue_and_attacks_instantly:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue ChuChu");
                    break;
                case ColorTypeEnum.Dark:
                case ColorTypeEnum.Dark_and_attacks_instantly:
                    m_actorMeshes = WResourceManager.LoadActorResource("Dark ChuChu");
                    break;
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Yellow ChuChu");
                    break;
            }
        }
	}
}
