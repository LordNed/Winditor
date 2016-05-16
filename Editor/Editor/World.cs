using WindEditor.Collision;
using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using System;

namespace WindEditor
{
    public partial class WWorld
    {
        public WUndoStack UndoStack { get { return m_undoStack; } }
        public WActorEditor ActorEditor { get { return m_actorEditor; } }

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
            m_actorEditor = new WActorEditor(this, m_tickableObjects);


            WSceneView perspectiveView = new WSceneView(this, m_renderableObjects);
            m_sceneViews.AddRange(new[] { perspectiveView });


            AllocateDefaultWorldResources();
        }

        public void LoadMap(string filePath)
        {
            //UnloadMap();
            //AllocateDefaultWorldResources();

            foreach (var folder in Directory.GetDirectories(filePath))
            {
                LoadLevel(folder);                    
            }
        }

        public void UnloadMap()
        {
            ReleaseResources();
        }

        public void ProcessTick()
        {
            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();

            UpdateSceneViews();

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

            // dflskdf
            /*WActor testActor = new WStaticMeshActor("resources/editor/EditorCube.obj");
            WActor testActor2 = new WStaticMeshActor("resources/editor/EditorCube.obj");
            WActor testActor3 = new WStaticMeshActor("resources/editor/EditorCube.obj");
            RegisterObject(testActor);
            RegisterObject(testActor2);
            RegisterObject(testActor3);

            testActor2.Transform.Position = new OpenTK.Vector3(500, 0, 0);
            testActor3.Transform.Position = new OpenTK.Vector3(0, 0, 500);*/
        }


        private void UpdateSceneViews()
        {
            // If they've clicked, check which view is in focus.
            if(WInput.GetMouseButtonDown(0) || WInput.GetMouseButtonDown(1) || WInput.GetMouseButtonDown(2))
            {
                WSceneView focusedScene = GetFocusedSceneView();
                foreach (var scene in m_sceneViews)
                {
                    scene.IsFocused = false;
                    WRect viewport = scene.GetViewportDimensions();
                    if(viewport.Contains(WInput.MousePosition.X, WInput.MousePosition.Y))
                    {
                        focusedScene = scene;
                    }
                }

                focusedScene.IsFocused = true;
            }
        }


        public WSceneView GetFocusedSceneView()
        {
            foreach (var scene in m_sceneViews)
                if (scene.IsFocused)
                    return scene;

            return m_sceneViews[0];
        }
    }
}
