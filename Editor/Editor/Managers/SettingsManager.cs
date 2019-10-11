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

        public WIntProperty RootDirectoryProperty { get; set; } = new WIntProperty("FileReference", "RootDirectory", "Main", "Root Directory", 0);
        //[WProperty("Main", "Root Directory", true, "The path to an extracted TWW ISO, dumped with a recent version of Dolphin.")]
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
        private bool m_DumpTextures;
        private bool m_DumpShaders;

        public WSettingsContainer()
        {
            RootDirectory = new FileReference();
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
