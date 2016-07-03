using OpenTK;
using System.ComponentModel;
using System.IO;
using System;
using JStudio.J3D;
using System.Collections.Generic;

namespace WindEditor
{
    public class WRoomTable
    {
        public struct AdjacentRoom
        {
            public bool LoadRoom { get; set; }
            public bool Unknown1 { get; set; } // No idea. Always set for the first room but not the others.
            public byte RoomIndex { get; set; }

            public AdjacentRoom(bool loadRoom, bool unknown1, byte roomIndex)
            {
                LoadRoom = loadRoom;
                Unknown1 = unknown1;
                RoomIndex = roomIndex;
            }

            public override string ToString()
            {
                return string.Format("LoadRoom: {0} Unknown1: {1} RoomIndex: {2}", LoadRoom, Unknown1, RoomIndex);
            }
        }

        public BindingList<AdjacentRoom> AdjacentRooms { get; set; }
        public byte ReverbAmount { get; set; }
        public byte TimePass { get; set; }
        public byte Unknown1 { get; set; }

        public WRoomTable()
        {
            AdjacentRooms = new BindingList<AdjacentRoom>();
        }

        public override string ToString()
        {
            return string.Format("Reverb Amount: {0} TimePass: {1} Unknown1: {2} Adjacent Room Count: {3}", ReverbAmount, TimePass, Unknown1, AdjacentRooms.Count);
        }
    }

    public class WRoomTransform
    {
        public Vector2 Translation { get; set; }
        public float YRotation { get; set; }
        public byte RoomNumber { get; set; }
        public byte Unknown1 { get; set; }

        public WRoomTransform(Vector2 translation, float yRot, byte roomIndex, byte unknown1)
        {
            Translation = translation;
            YRotation = yRot;
            RoomNumber = roomIndex;
            Unknown1 = unknown1;
        }
    }



    public class WRoom : WScene, IRenderable
    {
        public int RoomIndex { get; protected set; }
        public int MemoryAllocation { get; set; }
        public WRoomTransform RoomTransform { get; set; }

        private List<J3D> m_roomModels;

        public WRoom(WWorld world, int roomIndex):base(world)
        {
            RoomIndex = roomIndex;
            m_roomModels = new List<J3D>();
        }

        public override void Load(string filePath)
        {
            base.Load(filePath);

            LoadRoomModels(filePath);

            foreach (var folder in Directory.GetDirectories(filePath))
            {
                string folderName = Path.GetFileNameWithoutExtension(folder);
                switch (folderName.ToLower())
                {
                    case "dzb":
                        {
                            string fileName = Path.Combine(folder, "room.dzb");
                            //LoadLevelCollisionFromFile(fileName);
                        }
                        break;
                    case "dzr":
                    case "dzs":
                        {
                            string fileName = Path.Combine(folder, "room.dzr");
                            if (File.Exists(fileName))
                                LoadLevelEntitiesFromFile(fileName);
                        }
                        break;
                    //case "bmd":
                    //case "bdl":
                    //    {
                    //        // Wind Waker has a fixed list of models that it can load from the bmd/bdl folder of a given room:
                    //        // model, model1, model2, model3. 
                    //        string[] modelNames = new[] { "model", "model1", "model2", "model3" };

                    //        foreach(var modelName in modelNames)
                    //        {
                    //            J3D mesh = LoadModel(folder, modelName);
                    //            if (mesh != null)
                    //                m_roomModels.Add(mesh);
                    //        }
                    //    }
                    //    break;
                }
            }
        }

        private void LoadRoomModels(string filePath)
        {
            // Search the bmd and bdl folders for valid model names. Then search for a matching brk and btk for those models.
            string[] modelNames = new[] { "model", "model1", "model2", "model3" };
            string[] folderNames = new[] { "bmd", "bdl" };
            bool[] validModels = new bool[modelNames.Length];

            foreach(var subFolder in folderNames)
            {
                string folderPath = Path.Combine(filePath, subFolder);
                foreach (var modelName in modelNames)
                {
                    J3D mesh = LoadModel(folderPath, modelName);
                    if (mesh != null)
                        m_roomModels.Add(mesh);
                }
            }

        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            foreach (var model in m_roomModels)
                model.Tick(deltaTime);
        }

        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddOpaqueMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            Vector3 scale = Transform.LocalScale;
            Quaternion rotation = Transform.Rotation;
            Vector3 translation = Transform.Position;

            if(RoomTransform != null)
            {
                rotation = Quaternion.FromAxisAngle(Vector3.UnitY, RoomTransform.YRotation);
                translation = new Vector3(RoomTransform.Translation.X, 0, RoomTransform.Translation.Y);
            }

            Matrix4 trs = Matrix4.CreateScale(scale) * Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(translation);
            foreach(var mesh in m_roomModels)
                mesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
        }

        Vector3 IRenderable.GetPosition()
        {
            if (RoomTransform != null)
                return new Vector3(RoomTransform.Translation.X, 0, RoomTransform.Translation.Y);

            return Vector3.Zero;
        }

        float IRenderable.GetBoundingRadius()
        {
            return float.MaxValue;
        }
    }
}
