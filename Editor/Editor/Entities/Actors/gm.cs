using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class gm
    {
        [WProperty("Mothula's Missing Wings", "Missing Lower Right Wing", true, "", SourceScene.Room)]
        public bool MissingLowerRightWing
        {
            get
            {
                if (MissingWings == 0xFF)
                    return false;
                return (MissingWings & 0x01) != 0;
            }

            set
            {
                if (MissingWings == 0xFF)
                    MissingWings = 0x00;
                int value_as_int = value ? 1 : 0;
                MissingWings = (int)(MissingWings & ~0x01 | (value_as_int << 0 & 0x01));
                OnPropertyChanged("MissingLowerRightWing");
            }
        }

        [WProperty("Mothula's Missing Wings", "Missing Lower Left Wing", true, "", SourceScene.Room)]
        public bool MissingLowerLeftWing
        {
            get
            {
                if (MissingWings == 0xFF)
                    return false;
                return (MissingWings & 0x02) != 0;
            }

            set
            {
                if (MissingWings == 0xFF)
                    MissingWings = 0x00;
                int value_as_int = value ? 1 : 0;
                MissingWings = (int)(MissingWings & ~0x02 | (value_as_int << 0 & 0x02));
                OnPropertyChanged("MissingLowerLeftWing");
            }
        }

        [WProperty("Mothula's Missing Wings", "Missing Upper Right Wing", true, "", SourceScene.Room)]
        public bool MissingUpperRightWing
        {
            get
            {
                if (MissingWings == 0xFF)
                    return false;
                return (MissingWings & 0x04) != 0;
            }

            set
            {
                if (MissingWings == 0xFF)
                    MissingWings = 0x00;
                int value_as_int = value ? 1 : 0;
                MissingWings = (int)(MissingWings & ~0x04 | (value_as_int << 0 & 0x04));
                OnPropertyChanged("MissingUpperRightWing");
            }
        }

        [WProperty("Mothula's Missing Wings", "Missing Upper Left Wing", true, "", SourceScene.Room)]
        public bool MissingUpperLeftWing
        {
            get
            {
                if (MissingWings == 0xFF)
                    return false;
                return (MissingWings & 0x08) != 0;
            }

            set
            {
                if (MissingWings == 0xFF)
                    MissingWings = 0x00;
                int value_as_int = value ? 1 : 0;
                MissingWings = (int)(MissingWings & ~0x08 | (value_as_int << 0 & 0x08));
                OnPropertyChanged("MissingUpperLeftWing");
            }
        }

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
            m_actorMeshes.Clear();
            m_objRender = null;
            switch (Type)
            {
                case TypeEnum.Mothula:
                    if (MothulaType == MothulaTypeEnum.Wingless_Mothula)
                    {
                        m_actorMeshes = WResourceManager.LoadActorResource("Wingless Mothula");
                    } else
                    {
                        m_actorMeshes = WResourceManager.LoadActorResource("Winged Mothula");
                    }
                    break;
                case TypeEnum.Wing_that_falls_down_spinning:
                case TypeEnum.Wing_that_floats_down_gently:
                    switch (WhichWing)
                    {
                        case WhichWingEnum.Lower_Right_Wing:
                            m_actorMeshes = WResourceManager.LoadActorResource("Mothula Lower Right Wing");
                            break;
                        case WhichWingEnum.Lower_Left_Wing:
                            m_actorMeshes = WResourceManager.LoadActorResource("Mothula Lower Left Wing");
                            break;
                        case WhichWingEnum.Upper_Right_Wing:
                            m_actorMeshes = WResourceManager.LoadActorResource("Mothula Upper Right Wing");
                            break;
                        case WhichWingEnum.Upper_Left_Wing:
                            m_actorMeshes = WResourceManager.LoadActorResource("Mothula Upper Left Wing");
                            break;
                        default:
                            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                            break;
                    }
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
	}
}
