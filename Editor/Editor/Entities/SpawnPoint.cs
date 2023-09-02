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
			return $"Player Spawn: { Name }, Room: { Room }, Spawn ID: { SpawnID }";
        }

        public override void PostLoad()
        {
            if (string.IsNullOrEmpty(Name))
                Name = "Link";

            m_actorMeshes = WResourceManager.LoadActorResource("Link");
        }

        public override void PopulateDefaultProperties()
        {
            Name = "Link";
            EnemyNumber = -1;
            Event = null;
            Unknown4 = -1;
            ShipID = -1;

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
