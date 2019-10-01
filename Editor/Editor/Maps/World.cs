using System.Collections.Generic;
using System.ComponentModel;
using WindEditor.Editor.Modes;
using WindEditor.Minitors;

namespace WindEditor
{
    public partial class WWorld : INotifyPropertyChanged
    {
        public WMap Map { get { return m_currentMap; } }
        public WUndoStack UndoStack { get { return m_undoStack; } }
        //public WActorEditor ActorEditor { get { return m_actorEditor; } }

        public IEditorMode CurrentMode
        {
            get { return m_CurrentMode; }
            set
            {
                if (value != m_CurrentMode)
                {
                    SwitchMode(m_CurrentMode, value);

                    m_CurrentMode = value;
                    OnPropertyChanged("CurrentMode");
                }
            }
        }

        private List<WSceneView> m_sceneViews;
        private System.Diagnostics.Stopwatch m_dtStopwatch;
        private WUndoStack m_undoStack;
        //private WActorEditor m_actorEditor;
        private WLineBatcher m_persistentLines;
        private WMap m_currentMap;

        private IEditorMode m_CurrentMode;
        private ActorMode m_ActorMode;
        private CollisionMode m_CollisionMode;

        public WWorld()
        {
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
            m_persistentLines = new WLineBatcher();
            m_undoStack = new WUndoStack();
            //m_actorEditor = new WActorEditor(this);

            m_ActorMode = new ActorMode(this);
            m_CollisionMode = new CollisionMode(this);

            CurrentMode = m_ActorMode;

            m_sceneViews = new List<WSceneView>();

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

                if (Map != null)
                {
                    if (Map.FocusedScene != null)
                    {
                        foreach (WDOMNode node in Map.FocusedScene.Children)
                        {
                            if (node.GetType() == typeof(WSkyboxNode))
                            {
                                var sky = node as WSkyboxNode;
                                sky.UpdatePosition(view.ViewCamera.Transform.Position);
                            }
                        }
                    }
                }

                view.StartFrame();

                // Iterate through all of the things that need to be added to the viewport and call AddToRenderer on them.
                if (m_currentMap != null)
                {
                    m_CurrentMode.FilterSceneForRenderer(view, this);
                }

                // Add our Actor Editor and Persistent Lines.
                m_CurrentMode.Update(view);
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

        public void LoadMapFromDirectory(string folderPath, string sourcePath)
        {
            m_currentMap = new WMap(this);
            m_currentMap.LoadFromDirectory(folderPath, sourcePath);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Map"));

            m_CollisionMode.OnBecomeActive();
        }

        public void SaveMapToDirectory(string directory)
        {
            if (m_currentMap != null)
            {
                if (string.IsNullOrEmpty(directory))
                    directory = m_currentMap.SavePath;
                m_currentMap.SaveToDirectory(directory);
            }
        }

        public void UnloadMap()
        {
            // Clear our Undo/Redo Stack
            m_undoStack.Clear();

            // Clear our array of currently selected objects as well.
            m_CurrentMode.ClearSelection();

            m_ActorMode.DetailsViewModel.Categories = new System.Collections.Specialized.OrderedDictionary();
            m_CollisionMode.Shutdown();

            // Clear persistent lines from the last map as well.
            m_persistentLines.Clear();

            m_currentMap = null;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Map"));
        }

        public void ShutdownWorld()
        {
            System.Console.WriteLine("Shutdown World");

            // Unload any loaded resources and free all associated memory.
            WResourceManager.UnloadAllResources();

            foreach (var view in m_sceneViews)
                view.Dispose();

            m_ActorMode.Dispose();

            m_persistentLines.Dispose();
        }

        private void SwitchMode(IEditorMode old_mode, IEditorMode new_mode)
        {
            if (new_mode == null)
            {
                throw new System.Exception("World.SwitchMode: new_mode parameter was null!");
            }

            if (old_mode != null)
            {
                old_mode.GenerateUndoEvent -= UndoEventHandler;
            }

            new_mode.GenerateUndoEvent += UndoEventHandler;
        }

        private void UndoEventHandler(object sender, GenerateUndoEventArgs e)
        {
            if (e.Command != null)
            {
                UndoStack.Push(e.Command);
            }
        }

        public void InitMinitorModule(IMinitor minitor)
        {
            minitor.InitModule(m_ActorMode.DetailsViewModel);
        }

        public void SwitchToActorMode()
        {
            CurrentMode = m_ActorMode;
        }

        public void SwitchToCollisionMode()
        {
            CurrentMode = m_CollisionMode;
        }

        #region INotifyPropertyChanged Support
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
