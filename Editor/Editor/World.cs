using System.Collections.Generic;
using System.IO;
using System;
using System.ComponentModel;

namespace WindEditor
{
    public partial class WWorld : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WUndoStack UndoStack { get { return m_undoStack; } }
        public WActorEditor ActorEditor { get { return m_actorEditor; } }
        public BindingList<WScene> SceneList { get { return m_sceneList; } }

        public WScene FocusedScene
        {
            get { return m_focusedScene; }
            set
            {
                m_focusedScene = value;
                OnPropertyChanged("FocusedScene");
                OnPropertyChanged("FocusedSceneLabel");
            }
        }
        public string FocusedSceneLabel { get { return FocusedScene == null ? "" : string.Format("Level: {0}", FocusedScene.Name); } }

        private List<WSceneView> m_sceneViews = new List<WSceneView>();

        private WLineBatcher m_persistentLines;
        private System.Diagnostics.Stopwatch m_dtStopwatch;
        private WUndoStack m_undoStack;
        private WActorEditor m_actorEditor;
        private BindingList<WScene> m_sceneList;
        private WScene m_focusedScene;


        public WWorld()
        {
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
            m_persistentLines = new WLineBatcher();
            m_undoStack = new WUndoStack();
            m_actorEditor = new WActorEditor(this);
            m_sceneList = new BindingList<WScene>();

            WSceneView perspectiveView = new WSceneView();
            m_sceneViews.AddRange(new[] { perspectiveView });
        }

        public void LoadMapFromDirectory(string dirPath)
        {
            //UnloadMap();
            foreach(var sceneFolder in Directory.GetDirectories(dirPath))
            {
                string sceneName = Path.GetFileName(sceneFolder);
                WScene scene = null;

                if (sceneName.Contains("Room"))
                {
                    scene = new WScene(this);
                    scene.LoadRoom(sceneFolder);
                }
                else if (string.Compare(sceneName, "Stage", true) == 0)
                {
                    scene = new WScene(this);
                    scene.LoadStage(sceneFolder);
                }
                else
                {
                    Console.WriteLine("Unknown Map Folder: {0}", sceneFolder);
                }

                m_sceneList.Add(scene);
            }

            if (m_sceneList.Count > 0)
                FocusedScene = m_sceneList[m_sceneList.Count - 1];
        }

        public void UnloadMap()
        {
            throw new NotImplementedException();
        }

        public void ProcessTick()
        {
            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();

            UpdateSceneViews();

            m_persistentLines.Tick(deltaTime);

            foreach(WScene scene in m_sceneList)
            {
                scene.Tick(deltaTime);
            }

            foreach (WSceneView view in m_sceneViews)
            {
                view.UpdateSceneCamera(deltaTime);
                view.StartFrame();

                // Iterate through all of the things that need to be added to the viewport
                // and call AddToRenderer on them.
                foreach(WScene scene in m_sceneList)
                {
                    foreach (var renderable in scene.GetChildrenOfType<IRenderable>())
                        renderable.AddToRenderer(view);
                }

                // Add our Actor Editor and Persistent Lines.
                m_actorEditor.UpdateForSceneView(view);
                m_persistentLines.AddToRenderer(view);

                view.DrawFrame();
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

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
