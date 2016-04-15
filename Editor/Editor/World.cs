using Editor.Collision;
using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using System;

namespace Editor
{
    public class WWorld
    {
        private List<IRenderable> m_renderableObjects = new List<IRenderable>();
        private List<ITickableObject> m_tickableObjects = new List<ITickableObject>();
        private List<WSceneView> m_sceneViews = new List<WSceneView>();

        private DateTime m_lastDateTime;

        public WWorld()
        {
            m_lastDateTime = DateTime.Now;


            WSceneView sceneView = new WSceneView(this, m_renderableObjects);
            m_sceneViews.Add(sceneView);
        }

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
            ReleaseResources();
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
            long tickCount = System.Diagnostics.Stopwatch.GetTimestamp();
            DateTime highResDateTime = new DateTime(tickCount);
            float deltaTime = (float)highResDateTime.Subtract(m_lastDateTime).TotalSeconds;
            m_lastDateTime = highResDateTime;

            foreach (var item in m_tickableObjects)
            {
                item.Tick(deltaTime);
            }

            foreach(WSceneView view in m_sceneViews)
            {
                view.Render();
            }
        }

        public void ReleaseResources()
        {
            foreach (var item in m_renderableObjects)
            {
                item.ReleaseResources();
            }
        }

        public void OnViewportResized(int width, int height)
        {
            foreach(WSceneView view in m_sceneViews)
            {
                view.SetViewportSize(width, height);
            }
        }
    }
}
