using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class StageProperties
    {
        [WProperty("Stage Properties", "Is Dungeon", true, "", SourceScene.Room)]
        public bool IsDungeon
        {
            get
            {
                int value_as_int = (int)((m_Parameters1 & 0x01) >> 0);
                return value_as_int == 0 ? false : true;
            }

            set
            {
                int value_as_int = Convert.ToInt32(value);
                m_Parameters1 = (byte)(m_Parameters1 & ~0x01 | (value_as_int << 0 & 0x01));
                OnPropertyChanged("IsDungeon");
            }
        }

        [WProperty("Stage Properties", "Stage Save Info ID", true, "The index of which stage save info in the save file this stage should use to store its variables.", SourceScene.Room)]
        public int StageSaveInfoID
        {
            get
            {
                int value_as_int = (int)((m_Parameters1 & 0xFE) >> 1);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters1 = (byte)(m_Parameters1 & ~0x01 | (value_as_int << 1 & 0xFE));
                OnPropertyChanged("StageSaveInfoID");
            }
        }

        public enum MinimapTypeEnum
        {
            Sea_chart = 0,
            Dungeon_map = 1,
            Unknown_1 = 2,
            Unknown_2 = 3,
        }

        [WProperty("Stage Properties", "Minimap Type", true, "", SourceScene.Room)]
        public MinimapTypeEnum MinimapType
        {
            get
            {
                int value_as_int = (int)((m_Parameters2 & 0x0003) >> 0);
                return (MinimapTypeEnum)value_as_int;
            }

            set
            {
                int value_as_int = (int)value;
                m_Parameters2 = (short)(m_Parameters2 & ~0x0003 | (value_as_int << 0 & 0x0003));
                OnPropertyChanged("MinimapType");
            }
        }

        [WProperty("Stage Properties", "Unknown 2", true, "", SourceScene.Room)]
        public bool Unknown2
        {
            get
            {
                int value_as_int = (int)((m_Parameters2 & 0x0004) >> 2);
                return value_as_int == 0 ? false : true;
            }

            set
            {
                int value_as_int = Convert.ToInt32(value);
                m_Parameters2 = (short)(m_Parameters2 & ~0x0004 | (value_as_int << 2 & 0x0004));
                OnPropertyChanged("Unknown2");
            }
        }

        [WProperty("Stage Properties", "Particle Bank", true, "Which particle bank number to load for this stage, from the res/Particle folder.", SourceScene.Room)]
        public int ParticleBank
        {
            get
            {
                int value_as_int = (int)((m_Parameters2 & 0x07F8) >> 3);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters2 = (short)(m_Parameters2 & ~0x07F8 | (value_as_int << 3 & 0x07F8));
                OnPropertyChanged("ParticleBank");
            }
        }

        [WProperty("Stage Properties", "Unknown 3", true, "", SourceScene.Room)]
        public int Unknown3
        {
            get
            {
                int value_as_int = (int)((m_Parameters2 & 0xF800) >> 11);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters2 = (short)(m_Parameters2 & ~0xF800 | (value_as_int << 11 & 0xF800));
                OnPropertyChanged("Unknown3");
            }
        }

        public enum StageTypeEnum
        {
            Unknown_0 = 0,
            Dungeon = 1,
            Indoors = 2,
            Boss_room = 3,
            Cave = 4,
            Unknown_5 = 5,
            Miniboss_room = 6,
            Sea = 7,
        }

        [WProperty("Stage Properties", "Stage Type", true, "", SourceScene.Room)]
        public StageTypeEnum StageType
        {
            get
            {
                int value_as_int = (int)((m_Parameters3 & 0x00070000) >> 16);
                return (StageTypeEnum)value_as_int;
            }

            set
            {
                int value_as_int = (int)value;
                m_Parameters3 = (int)(m_Parameters3 & ~0x00070000 | (value_as_int << 16 & 0x00070000));
                OnPropertyChanged("StageType");
            }
        }
    }
}
