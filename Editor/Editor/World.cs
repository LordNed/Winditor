using Editor.Collision;
using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;

namespace Editor
{
    public class WWorld
    {
        public WUndoStack UndoStack { get { return m_undoStack; } }
        public TUndoRedoObject TestUndoRedoObj { get; set; }

        private List<IRenderable> m_renderableObjects = new List<IRenderable>();
        private List<ITickableObject> m_tickableObjects = new List<ITickableObject>();
        private List<WSceneView> m_sceneViews = new List<WSceneView>();

        private WLineBatcher m_persistentLines;
        private System.Diagnostics.Stopwatch m_dtStopwatch;
        private WUndoStack m_undoStack;
        private WActorEditor m_actorEditor;

        public WWorld()
        {
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
            m_undoStack = new WUndoStack();
            TestUndoRedoObj = new TUndoRedoObject(m_undoStack);
            m_actorEditor = new WActorEditor(this, m_tickableObjects);


            WSceneView perspectiveView = new WSceneView(this, m_renderableObjects); perspectiveView.SetViewRect(new WRect(0, 0, 0.5f, 0.5f));
            WSceneView topView = new WSceneView(this, m_renderableObjects); topView.SetViewRect(new WRect(0.5f, 0, 0.5f, 0.5f));
            WSceneView leftView = new WSceneView(this, m_renderableObjects); leftView.SetViewRect(new WRect(0f, 0.5f, 0.5f, 0.5f));
            WSceneView frontView = new WSceneView(this, m_renderableObjects); frontView.SetViewRect(new WRect(0.5f, 0.5f, 0.5f, 0.5f));
            m_sceneViews.AddRange(new[] { perspectiveView, topView, leftView, frontView });


            AllocateDefaultWorldResources();

            // dflskdf
            WActor testActor = new WStaticMeshActor("resources/editor/EditorCube.obj");
            WActor testActor2 = new WStaticMeshActor("resources/editor/EditorCube.obj");
            WActor testActor3 = new WStaticMeshActor("resources/editor/EditorCube.obj");
            RegisterObject(testActor);
            RegisterObject(testActor2);
            RegisterObject(testActor3);

            testActor2.Transform.Position = new OpenTK.Vector3(500, 0, 0);
            testActor3.Transform.Position = new OpenTK.Vector3(0, 0, 500);
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
                ITickableObject tickableObj = (ITickableObject)obj;
                tickableObj.SetWorld(this);
                m_tickableObjects.Add(tickableObj);
                
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

            m_actorEditor.Tick(deltaTime);

            foreach (WSceneView view in m_sceneViews)
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
        }
    }
}
