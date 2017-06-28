using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace WindEditor
{
    public class WMap : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string FocusedSceneLabel { get { return FocusedScene == null ? "" : FocusedScene is WStage ? "Stage" : FocusedScene.Name; } }
        public string MapName { get { return m_mapName; } }
        public string SavePath { get { return m_savePath; } set { m_savePath = value; } }

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

        public BindingList<WScene> SceneList { get { return m_sceneList; } }

        public float TimeOfDay
        {
            get { return m_timeOfDay; }
            set
            {
                m_timeOfDay = WMath.Clamp(value, 0, 1);
                OnPropertyChanged("TimeOfDay");
            }
        }

        /// <summary> The folder in which this map has been loaded from. </summary>
        private string m_savePath;

        /// <summary> The name of this map ("sea", "GanonM", etc.) Maximum of 8 characters. </summary>
        private string m_mapName;

        private WWorld m_world;
        private BindingList<WScene> m_sceneList;
        private WScene m_focusedScene;
        private float m_timeOfDay;

        public WMap(WWorld parentWorld)
        {
            m_world = parentWorld;
            m_sceneList = new BindingList<WScene>();

            // Set us to mid-day lighting by default.
            TimeOfDay = 0.5f;
        }

        public void LoadFromDirectory(string inPath, string sourcePath)
        {
            if (!Directory.Exists(inPath))
                throw new ArgumentException("Cannot load map from non-existant directory", "filePath");


            m_mapName = Path.GetFileName(inPath);
            m_savePath = Path.GetFullPath(sourcePath);

            Console.WriteLine("Loading map {0}...", m_mapName);
            
            // Sort them alphabetically so we always load the Stage last.
            List<string> sortedScenes = new List<string>(Directory.GetDirectories(inPath));
            sortedScenes.Sort();

            WStage stage = null;

            foreach (var sceneFolder in sortedScenes)
            {
                string sceneName = Path.GetFileName(sceneFolder);
                WScene scene = null;

                if (sceneName.ToLower().StartsWith("room")) //
                {
                    string roomNumberStr = sceneName.Substring(4);
                    int roomNumber;

                    if (int.TryParse(roomNumberStr, out roomNumber))
                        scene = new WRoom(m_world, roomNumber);
                    else
                        Console.WriteLine("Unknown Room Number for Room: \"{0}\", Skipping!", sceneName);
                }
                else if (string.Compare(sceneName, "Stage", true) == 0)
                {
                    stage = new WStage(m_world);
                    scene = stage;
                }
                else
                    Console.WriteLine("Unknown Map Folder: {0}", sceneFolder);

                if (scene != null)
                {
                    m_sceneList.Add(scene);
                    scene.Load(sceneFolder);
                }
            }

            // Now that we've loaded all of the data, we'll do some post processing.
            if (stage != null)
            {
                List<WRoom> allRooms = new List<WRoom>();
                foreach (var scene in m_sceneList)
                    if (scene is WRoom) allRooms.Add((WRoom)scene);

                stage.PostLoadProcessing(inPath, allRooms);
            }

            if (m_sceneList.Count > 0)
                FocusedScene = m_sceneList[m_sceneList.Count - 1];
        }

        public void SaveToDirectory(string savePath)
        {
            foreach(WScene scene in m_sceneList)
            {
                string sceneName = scene is WStage ? "Stage" : scene.Name;
                string folderPath = string.Format("{0}/{1}", savePath, sceneName);
                scene.SaveToDirectory(folderPath);
            }
        }

        public void Tick(float deltaTime)
        {
            foreach (WScene scene in m_sceneList)
            {
                scene.SetTimeOfDay(TimeOfDay);
                scene.Tick(deltaTime);
            }
        }

        public void AddToRenderer(WSceneView view)
        {
            foreach (WScene scene in m_sceneList)
            {
                foreach (var renderable in scene.GetChildrenOfType<IRenderable>())
                    renderable.AddToRenderer(view);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
