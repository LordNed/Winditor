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

        private WLineBatcher m_persistentLines;
        private System.Diagnostics.Stopwatch m_dtStopwatch;

        public WWorld()
        {
            m_dtStopwatch = new System.Diagnostics.Stopwatch();

            WSceneView sceneView = new WSceneView(this, m_renderableObjects);
            m_sceneViews.Add(sceneView);
        }

        public void LoadMap(string filePath)
        {
            UnloadMap();
            AllocateDefaultWorldResources();

            foreach (var folder in Directory.GetDirectories(filePath))
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

        public void RegisterObject(object obj)
        {
            // This is awesome.
            if (obj is IRenderable)
            {
                m_renderableObjects.Add(obj as IRenderable);
            }

            if(obj is ITickableObject)
            {
                m_tickableObjects.Add(obj as ITickableObject);
            }
        }

        public void UnregisterObject(object obj)
        {
            if(obj is IRenderable)
            {
                IRenderable renderable = obj as IRenderable;
                renderable.ReleaseResources();

                m_renderableObjects.Remove(renderable);
            }

            if(obj is ITickableObject)
            {
                m_tickableObjects.Remove(obj as ITickableObject);
            }
        }

        public void ProcessTick()
        {
            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();

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

        private void AllocateDefaultWorldResources()
        {
            m_persistentLines = new WLineBatcher();
            RegisterObject(m_persistentLines);

            // DEBAUG
            m_persistentLines.DrawLine(OpenTK.Vector3.Zero, new OpenTK.Vector3(250, 50, 250), WLinearColor.White, 25f, 5);
        }
    }
}
