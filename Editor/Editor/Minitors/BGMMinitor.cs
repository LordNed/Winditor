using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WindEditor.Minitors.BGM;
using WindEditor.ViewModel;
using System.IO;
using WArchiveTools;
using WArchiveTools.FileSystem;
using GameFormatReader.Common;

namespace WindEditor.Minitors
{
    public class BGMMinitor : IMinitor, INotifyPropertyChanged
    {
        #region IMinitor Interface
        public MenuItem GetMenuItem()
        {
            return new MenuItem()
            {
                Header = "Scene BGM Editor",
                ToolTip = "Editor for what background music plays in what maps.",
                Command = OpenMinitorCommand,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };
        }

        public void InitModule(WDetailsViewViewModel details_view_model)
        {

        }

        public bool RequestCloseModule()
        {
            if (!m_IsDataDirty)
                return true;

            MessageBoxResult result = MessageBox.Show("You have unsaved changes to the BGM data. Save them?", "Unsaved Changes", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    OnRequestSaveBGMData();
                    return true;
                case MessageBoxResult.No:
                    return true;
                case MessageBoxResult.Cancel:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        const string BGM_DAT_NAME = "dat/bgm_dat.bin";
        const string ISLAND_NAME_DATABASE = "resources/IslandNamesDatabase.txt";
        const int ISLAND_NAME_COUNT = 50;

        public ICommand OpenMinitorCommand { get { return new RelayCommand(x => OnRequestOpenBGMEditor(),
            x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); } }

        public List<BGMEntry> MapEntries
        {
            get { return m_MapEntries; }
            set
            {
                if (value != m_MapEntries)
                {
                    m_MapEntries = value;
                    OnPropertyChanged("MapEntries");
                }
            }
        }

        public List<BGMEntry> IslandEntries
        {
            get { return m_IslandEntries; }
            set
            {
                if (value != m_IslandEntries)
                {
                    m_IslandEntries = value;
                    OnPropertyChanged("IslandEntries");
                }
            }
        }

        public BGMEntry SelectedMapEntry
        {
            get { return m_SelectedMapEntry; }
            set
            {
                if (value != m_SelectedMapEntry)
                {
                    m_SelectedMapEntry = value;
                    OnPropertyChanged("SelectedMapEntry");
                }
            }
        }

        public BGMEntry SelectedIslandEntry
        {
            get { return m_SelectedIslandEntry; }
            set
            {
                if (value != m_SelectedIslandEntry)
                {
                    m_SelectedIslandEntry = value;
                    OnPropertyChanged("SelectedIslandEntry");
                }
            }
        }

        private BGMMinitorWindow m_MinitorWindow;
        private bool m_IsDataDirty;
        private VirtualFilesystemDirectory m_SystemArchive;

        private List<BGMEntry> m_MapEntries;
        private List<BGMEntry> m_IslandEntries;

        private BGMEntry m_SelectedMapEntry;
        private BGMEntry m_SelectedIslandEntry;

        public void OnRequestOpenBGMEditor()
        {
            if (m_MinitorWindow != null && m_MinitorWindow.IsVisible == true)
            {
                m_MinitorWindow.Focus();
                return;
            }

            if (m_MapEntries == null)
            {
                if (!TryLoadSystemArchive())
                {
                    MessageBox.Show($"The file " +
                        $"\"{Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Object", "System.arc")}\" " +
                        $"could not be found. The BGM editor cannot be opened.\n\n" +
                        $"Please check that the Root Directory in your settings includes this file.",
                        "Archive Not Found", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }

            m_MinitorWindow = new BGMMinitorWindow();
            m_MinitorWindow.DataContext = this;

            SelectedMapEntry = m_MapEntries[0];
            SelectedIslandEntry = m_IslandEntries[0];
            m_MinitorWindow.Show();
        }

        public void OnRequestSaveBGMData()
        {

        }

        private bool TryLoadSystemArchive()
        {
            string system_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Object", "System.arc");

            if (!File.Exists(system_path))
            {
                return false;
            }

            m_SystemArchive = ArchiveUtilities.LoadArchive(system_path);

            VirtualFilesystemFile bgm_dat = m_SystemArchive.GetFileAtPath(BGM_DAT_NAME);

            using (MemoryStream strm = new MemoryStream(bgm_dat.Data))
            {
                EndianBinaryReader reader = new EndianBinaryReader(strm, Endian.Big);
                LoadBGMData(reader);
            }

            return true;
        }

        private void LoadBGMData(EndianBinaryReader reader)
        {
            int mapInfoCount = reader.ReadInt32();
            int mapInfoOffset = reader.ReadInt32();

            int islandInfoCount = reader.ReadInt32();
            int islandInfoOffset = reader.ReadInt32();

            int waveBank1Offset = reader.ReadInt32();
            int waveBank2Offset = reader.ReadInt32();

            int stringBankOffset = reader.ReadInt32();
            int spotNamesOffset = reader.ReadInt32();

            reader.BaseStream.Seek(mapInfoOffset, SeekOrigin.Begin);
            m_MapEntries = new List<BGMEntry>();

            for (int i = 0; i < mapInfoCount; i++)
            {
                BGMEntry newEntry = new BGMEntry(reader);

                long curPos = reader.BaseStream.Position;

                reader.BaseStream.Seek(waveBank1Offset + (2 * newEntry.WaveBankIndex1), SeekOrigin.Begin);
                newEntry.WaveBank1 = reader.ReadByte();
                newEntry.WaveBank2 = reader.ReadByte();

                reader.BaseStream.Seek(waveBank2Offset + (2 * newEntry.WaveBankIndex2), SeekOrigin.Begin);
                newEntry.WaveBank3 = reader.ReadByte();
                newEntry.WaveBank4 = reader.ReadByte();

                if (i != 0)
                {
                    reader.BaseStream.Seek(spotNamesOffset + (4 * (i - 1)), SeekOrigin.Begin);
                    int nameOffset = reader.ReadInt32();

                    reader.BaseStream.Seek(nameOffset, SeekOrigin.Begin);
                    newEntry.Name = reader.ReadStringUntil('\0');
                }

                reader.BaseStream.Seek(curPos, SeekOrigin.Begin);
                m_MapEntries.Add(newEntry);
            }

            reader.BaseStream.Seek(islandInfoOffset, SeekOrigin.Begin);
            m_IslandEntries = new List<BGMEntry>();

            for (int i = 0; i < islandInfoCount; i++)
            {
                BGMEntry newEntry = new BGMEntry(reader);

                long curPos = reader.BaseStream.Position;

                reader.BaseStream.Seek(waveBank1Offset + (2 * newEntry.WaveBankIndex1), SeekOrigin.Begin);
                newEntry.WaveBank1 = reader.ReadByte();
                newEntry.WaveBank2 = reader.ReadByte();

                reader.BaseStream.Seek(waveBank2Offset + (2 * newEntry.WaveBankIndex2), SeekOrigin.Begin);
                newEntry.WaveBank3 = reader.ReadByte();
                newEntry.WaveBank4 = reader.ReadByte();

                reader.BaseStream.Seek(curPos, SeekOrigin.Begin);
                m_IslandEntries.Add(newEntry);
            }

            RefreshIslandNameList();
        }

        private void RefreshIslandNameList()
        {
            string[] islandNames;

            // Try to load names from file if the file exists
            if (File.Exists(ISLAND_NAME_DATABASE))
                islandNames = File.ReadAllLines(ISLAND_NAME_DATABASE);
            // Otherwise generate generic names!
            else
            {
                islandNames = new string[IslandEntries.Count];
                islandNames[0] = "Global";

                for (int i = 1; i < IslandEntries.Count; i++)
                    islandNames[i] = $"Island {i}";
            }

            for (int i = 0; i < IslandEntries.Count; i++)
                m_IslandEntries[i].Name = islandNames[i];
        }

        public IEnumerable<BGMType> BGMTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(BGMType))
                    .Cast<BGMType>();
            }
        }
    }
}
