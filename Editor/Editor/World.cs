using System.Collections.Generic;
using System.IO;
using System;
using System.ComponentModel;

namespace WindEditor
{
    public partial class WWorld
    {
        public WUndoStack UndoStack { get { return m_undoStack; } }
        public WActorEditor ActorEditor { get { return m_actorEditor; } }
        public BindingList<WScene> SceneList { get { return m_sceneList; } }

        private List<WSceneView> m_sceneViews = new List<WSceneView>();

        private WLineBatcher m_persistentLines;
        private System.Diagnostics.Stopwatch m_dtStopwatch;
        private WUndoStack m_undoStack;
        private WActorEditor m_actorEditor;
        private BindingList<WScene> m_sceneList;

        public WWorld()
        {
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
            m_persistentLines = new WLineBatcher();
            m_undoStack = new WUndoStack();
            m_actorEditor = new WActorEditor(this);
            m_sceneList = new BindingList<WScene>();

            WSceneView perspectiveView = new WSceneView(this);
            m_sceneViews.AddRange(new[] { perspectiveView });
        }

        public void LoadMapFromDirectory(string dirPath)
        {
            //UnloadMap();
            foreach(var sceneFolder in Directory.GetDirectories(dirPath))
            {
                WScene scene = new WScene(this);
                scene.LoadLevel(sceneFolder);

                m_sceneList.Add(scene);
            }
        }

        public void UnloadMap()
        {
            throw new NotImplementedException();
        }

        [Obsolete("Please bring back persistent lines :-(")]
        public void ProcessTick()
        {
            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();

            UpdateSceneViews();

            m_persistentLines.Tick(deltaTime);
            m_actorEditor.Tick(deltaTime);

            foreach(WScene scene in m_sceneList)
            {
                scene.ProcessTick(deltaTime);
            }

            // Todo: Figure out how to make this work.
            //m_persistentLines.Render();
            foreach (WSceneView view in m_sceneViews)
            {
                view.Render();
            }
        }

        public void OnViewportResized(int width, int height)
        {
            foreach(WSceneView view in m_sceneViews)
            {
                view.SetViewportSize(width, height);
            }
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
