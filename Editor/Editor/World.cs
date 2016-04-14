using Editor.Collision;
using GameFormatReader.Common;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class WWorld
    {
        private List<IRenderable> m_renderableObjects = new List<IRenderable>();

        public void LoadMap(string filePath)
        {
            UnloadMap();

            foreach(var folder in Directory.GetDirectories(filePath))
            {
                LoadLevel(folder);                    
            }
        }

        public void UnloadMap()
        {

        }

        private void LoadLevel(string filePath)
        {
            foreach (var folder in Directory.GetDirectories(filePath))
            {
                string folderName = Path.GetFileNameWithoutExtension(folder);
                switch(folderName.ToLower())
                {
                    case "dzb":
                        string fileName = Path.Combine(folder, "room.dzb");
                        LoadLevelCollisionFromFolder(fileName);
                        break;
                }
            }
        }

        private void LoadLevelCollisionFromFolder(string filePath)
        {
            var collision = new WCollisionMesh();
            using (EndianBinaryReader reader = new EndianBinaryReader(File.OpenRead(filePath), Endian.Big))
            {
                collision.Load(reader);
            }

            RegisterObject(collision);
        }

        private void RegisterObject(object obj)
        {
            // This is awesome.
            if(obj is IRenderable)
            {
                m_renderableObjects.Add(obj as IRenderable);
            }
        }

        public void ProcessTick()
        {
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw); // Windwaker is backwards!
            GL.Enable(EnableCap.CullFace);
            GL.DepthMask(true);

            Matrix4 modelMatrix = Matrix4.CreateTranslation(Vector3.Zero);
            Matrix4 viewMatrix = Matrix4.LookAt(new Vector3(0, 0, -100), Vector3.Zero, Vector3.UnitY);
            Matrix4 projMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), 1.5f /* hack */, 1, 1000000);

            foreach (var item in m_renderableObjects)
            {
                item.Render(viewMatrix, projMatrix);
            }
        }

        public void ReleaseResources()
        {
            foreach (var item in m_renderableObjects)
            {
                item.ReleaseResources();
            }
        }
    }
}
