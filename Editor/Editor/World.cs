using System.Collections.Generic;
using System.ComponentModel;

namespace WindEditor
{
    public partial class WWorld : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WMap Map { get { return m_currentMap; } }
        public WUndoStack UndoStack { get { return m_undoStack; } }
        public WActorEditor ActorEditor { get { return m_actorEditor; } }
        private List<WSceneView> m_sceneViews = new List<WSceneView>();

        private System.Diagnostics.Stopwatch m_dtStopwatch;
        private WUndoStack m_undoStack;
        private WActorEditor m_actorEditor;
        private WLineBatcher m_persistentLines;
        private WMap m_currentMap;

        public WWorld()
        {
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
            m_persistentLines = new WLineBatcher();
            m_undoStack = new WUndoStack();
            m_actorEditor = new WActorEditor(this);

            WSceneView perspectiveView = new WSceneView();
            m_sceneViews.AddRange(new[] { perspectiveView });
        }

        public void ProcessTick()
        {
            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();

            UpdateSceneViews();

            m_persistentLines.Tick(deltaTime);

            if(m_currentMap != null)
            {
                m_currentMap.Tick(deltaTime);
                
            }

            foreach (WSceneView view in m_sceneViews)
            {
                view.UpdateSceneCamera(deltaTime);
                view.StartFrame();

                // Iterate through all of the things that need to be added to the viewport and call AddToRenderer on them.
                if (m_currentMap != null)
                    m_currentMap.AddToRenderer(view);

                // Add our Actor Editor and Persistent Lines.
                m_actorEditor.UpdateForSceneView(view);
                m_persistentLines.AddToRenderer(view);

                view.DrawFrame();
            }
        }

        public void OnViewportResized(int width, int height)
        {
            foreach (WSceneView view in m_sceneViews)
            {
                view.SetViewportSize(width, height);
            }
        }

        private void UpdateSceneViews()
        {
            // If they've clicked, check which view is in focus.
            if (WInput.GetMouseButtonDown(0) || WInput.GetMouseButtonDown(1) || WInput.GetMouseButtonDown(2))
            {
                WSceneView focusedScene = GetFocusedSceneView();
                foreach (var scene in m_sceneViews)
                {
                    scene.IsFocused = false;
                    FRect viewport = scene.GetViewportDimensions();
                    if (viewport.Contains(WInput.MousePosition.X, WInput.MousePosition.Y))
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

        public void LoadMapFromDirectory(string folderPath)
        {
            m_currentMap = new WMap(this);
            m_currentMap.LoadFromDirectory(folderPath);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Map"));
        }

        public void ShutdownWorld()
        {
            System.Console.WriteLine("Shutdown World");
        }
    }
}
