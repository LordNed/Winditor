using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WindEditor.Editor;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;

namespace WindEditor
{
    public class FileReference : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string FilePath
        {
            get { return m_FilePath; }
            set
            {
                if (value != m_FilePath)
                {
                    m_FilePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }

        private string m_FilePath;

        public FileReference()
        {
            FilePath = "";
        }
    }

    [HideCategories(new string[] { })]
    public class WSettingsContainer : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        [WProperty("Paths", "Game Root", true, "Path to an extracted TWW ISO, dumped with a recent version of Dolphin.")]
        public FileReference RootDirectory
        {
            get { return m_RootDirectory; }
            set
            {
                if (value != m_RootDirectory)
                {
                    m_RootDirectory = value;
                    OnPropertyChanged("RootDirectory");
                }
            }
        }

        [WProperty("Paths", "Dolphin", true, "Path to an installation of Dolphin. Required for playtesting.")]
        public FileReference DolphinDirectory
        {
            get { return m_DolphinDirectory; }
            set
            {
                if (value != m_DolphinDirectory)
                {
                    m_DolphinDirectory = value;
                    OnPropertyChanged("DolphinDirectory");
                }
            }
        }

        [HideCategoriesAttribute()]
        [WProperty("Paths", "Last Stage Path", true, "")]
        public FileReference LastStagePath
        {
            get { return m_LastStagePath; }
            set
            {
                if (value != m_LastStagePath)
                {
                    m_LastStagePath = value;
                    OnPropertyChanged("LastStagePath");
                }
            }
        }

        [HideCategoriesAttribute()]
        [WProperty("Paths", "Last Collision Dae Path", true, "")]
        public FileReference LastCollisionDaePath
        {
            get { return m_LastCollisionDaePath; }
            set
            {
                if (value != m_LastCollisionDaePath)
                {
                    m_LastCollisionDaePath = value;
                    OnPropertyChanged("LastCollisionDaePath");
                }
            }
        }

        [WProperty("Advanced", "Dump Textures", true, "When checked, dumps the textures of loaded meshes to file.")]
        public bool DumpTextures
        {
            get { return m_DumpTextures; }
            set
            {
                if (value != m_DumpTextures)
                {
                    m_DumpTextures = value;
                    OnPropertyChanged("DumpTextures");
                }
            }
        }

        [WProperty("Advanced", "Dump Shaders", true, "When checked, dumps the shaders created for loaded models to file.")]
        public bool DumpShaders
        {
            get { return m_DumpShaders; }
            set
            {
                if (value != m_DumpShaders)
                {
                    m_DumpShaders = value;
                    OnPropertyChanged("DumpShaders");
                }
            }
        }

        [JsonIgnore]
        public string RootDirectoryPath
        {
            get { return RootDirectory.FilePath; }
        }

        private FileReference m_RootDirectory;
        private FileReference m_DolphinDirectory;
        private FileReference m_LastStagePath;
        private FileReference m_LastCollisionDaePath;
        private bool m_DumpTextures;
        private bool m_DumpShaders;

        public WSettingsContainer()
        {
            RootDirectory = new FileReference();
            DolphinDirectory = new FileReference();
            LastStagePath = new FileReference();
            LastCollisionDaePath = new FileReference();
        }

        public void SetProperty(string property_name, object value)
        {
            PropertyInfo[] props = this.GetType().GetProperties();

            foreach (PropertyInfo p in props)
            {
                if (p.Name == property_name)
                {
                    p.SetValue(this, value);
                }
            }
        }
    }

    public static class WSettingsManager
    {
        private static WSettingsContainer m_Settings;

        static WSettingsManager()
        {
            LoadSettings();
        }

        public static void LoadSettings()
        {
            if (!File.Exists("settings.json"))
            {
                m_Settings = new WSettingsContainer();
                return;
            }

            m_Settings = JsonConvert.DeserializeObject<WSettingsContainer>(File.ReadAllText("settings.json"));
        }

        public static void SaveSettings()
        {
            string serialized_settings = JsonConvert.SerializeObject(m_Settings, Formatting.Indented);
            File.WriteAllText("settings.json", serialized_settings);
        }

        public static WSettingsContainer GetSettings()
        {
            return m_Settings;
        }
    }
}
