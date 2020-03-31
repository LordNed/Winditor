using JStudio.J3D;
using OpenTK;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WindEditor
{
	public partial class SpawnPoint
	{
		public override string ToString()
		{
			return Name;
		}

        public override void PostLoad()
        {
            if (string.IsNullOrEmpty(Name))
                Name = "Link";

            m_actorMeshes = WResourceManager.LoadActorResource("Link");
        }

        [WProperty("Spawn Properties", "Room", true, "", SourceScene.Room)]
        public int Room
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
                OnPropertyChanged("Room");
            }
        }

        [WProperty("Unknowns", "Unknown 1", true, "", SourceScene.Room)]
        public bool Unknown1
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00000040) >> 6);
                return value_as_int == 0 ? false : true;
            }

            set
            {
                int value_as_int = Convert.ToInt32(value);
                m_Parameters = (int)(m_Parameters & ~0x00000040 | (value_as_int << 6 & 0x00000040));
                OnPropertyChanged("Unknown1");
            }
        }

        [WProperty("Unknowns", "Unknown 2", true, "", SourceScene.Room)]
        public bool Unknown2
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00000080) >> 7);
                return value_as_int == 0 ? false : true;
            }

            set
            {
                int value_as_int = Convert.ToInt32(value);
                m_Parameters = (int)(m_Parameters & ~0x00000080 | (value_as_int << 7 & 0x00000080));
                OnPropertyChanged("Unknown2");
            }
        }

        [WProperty("Unknowns", "Unknown 3", true, "", SourceScene.Room)]
        public int Unknown3
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
                OnPropertyChanged("Unknown3");
            }
        }

        [WProperty("Spawn Properties", "Spawn Type", true, "", SourceScene.Room)]
        public int SpawnType
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x0000F000) >> 12);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters = (int)(m_Parameters & ~0x0000F000 | (value_as_int << 12 & 0x0000F000));
                OnPropertyChanged("SpawnType");
            }
        }

        [WProperty("Unknowns", "Unknown 4", true, "", SourceScene.Room)]
        public int Unknown4
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
                OnPropertyChanged("Unknown4");
            }
        }

        [WProperty("Spawn Properties", "Event", true, "", SourceScene.Stage)]
        public MapEvent Event
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
                if (value_as_int == 0xFF) { return null; }
                WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
                List<MapEvent> list = stage.GetChildrenOfType<MapEvent>();
                if (value_as_int >= list.Count) { return null; }
                return list[value_as_int];
            }

            set
            {
                WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
                List<MapEvent> list = stage.GetChildrenOfType<MapEvent>();
                int value_as_int = list.IndexOf(value);
                m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
                OnPropertyChanged("Event");
            }
        }

        [WProperty("Spawn Properties", "Spawn ID", true, "", SourceScene.Room)]
        public int SpawnID
        {
            get
            {
                int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
                OnPropertyChanged("SpawnID");
            }
        }

        [WProperty("Unknowns", "Unknown 6", true, "", SourceScene.Room)]
        public int Unknown6
        {
            get
            {
                int value_as_int = (int)((m_AuxillaryParameters2 & 0xFF00) >> 8);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
                OnPropertyChanged("Unknown6");
            }
        }

        public override void PopulateDefaultProperties()
        {
            Name = "Link";
            Event = null;
            Unknown4 = -1;
            Unknown6 = -1;
            Unknown7 = -1;

            // Try to assign the room index automatically if this is a room spawn.
            WDOMNode cur_object = this;
            while (cur_object.Parent != null)
            {
                cur_object = cur_object.Parent;
            }
            WScene scene = cur_object as WScene;
            if (scene is WRoom)
            {
                WRoom room = scene as WRoom;
                Room = room.RoomIndex;
            }

            // Automatically assign it the first unused spawn ID in this scene.
            List<int> possibleSpawnIDs = Enumerable.Range(0, 256).ToList();
            List<SpawnPoint> otherSpawns = scene.GetChildrenOfType<SpawnPoint>();
            foreach (var spawn in otherSpawns)
            {
                if (spawn == this)
                    continue;
                possibleSpawnIDs.Remove(spawn.SpawnID);
            }

            if (possibleSpawnIDs.Count == 0)
            {
                SpawnID = 255;
            }
            else
            {
                SpawnID = possibleSpawnIDs.First();
            }
        }
    }
}
