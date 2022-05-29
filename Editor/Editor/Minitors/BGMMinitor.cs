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

        const string SEQUENCE_NAME_DATABASE = "resources/BGMSequencesDatabase.txt";
        const int SEQUENCE_NAME_COUNT = 96;
        const string STREAM_NAME_DATABASE = "resources/BGMStreamsDatabase.txt";
        const int STREAM_NAME_COUNT = 74;

        const string BANK_NAME_DATABASE = "resources/WaveBanksDatabase.txt";
        const int BANK_NAME_COUNT = 65;

        public ICommand OpenMinitorCommand { get { return new RelayCommand(x => OnRequestOpenBGMEditor(),
            x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); } }

        public ICommand SaveBGMDataCommand { get { return new RelayCommand(x => OnRequestSaveBGMData()); } }

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

                    UpdateMapNameCombobox(m_SelectedMapEntry.Type);
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

                    UpdateIslandNameCombobox(m_SelectedIslandEntry.Type);
                }
            }
        }

        public List<string> SequenceNames
        {
            get { return m_SequenceNames; }
            set 
            {
                if (value != m_SequenceNames)
                {
                    m_SequenceNames = value;
                    OnPropertyChanged("SequenceNames");
                }
            }
        }

        public List<string> StreamNames
        {
            get { return m_StreamNames; }
            set
            {
                if (value != m_StreamNames)
                {
                    m_StreamNames = value;
                    OnPropertyChanged("StreamNames");
                }
            }
        }

        public List<string> BankNames
        {
            get { return m_BankNames; }
            set
            {
                if (value != m_BankNames)
                {
                    m_BankNames = value;
                    OnPropertyChanged("BankNames");
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

        private List<string> m_SequenceNames;
        private List<string> m_StreamNames;
        private List<string> m_BankNames;

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

            RefreshMusicNameLists();
            RefreshBankNameList();

            m_MinitorWindow = new BGMMinitorWindow();
            m_MinitorWindow.DataContext = this;

            SelectedMapEntry = m_MapEntries[0];
            SelectedIslandEntry = m_IslandEntries[0];
            m_MinitorWindow.Show();
        }

        public void OnRequestSaveBGMData()
        {
            SaveBGMData();
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

                if (i == 0)
                    newEntry.Name = "Default";
                else
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

        private void SaveBGMData()
        {
            VirtualFilesystemFile bgm_dat = m_SystemArchive.GetFileAtPath(BGM_DAT_NAME);

            using (MemoryStream ms = new MemoryStream())
            {
                EndianBinaryWriter writer = new EndianBinaryWriter(ms, Endian.Big);

                writer.Write(MapEntries.Count);
                writer.Write(0x20);

                writer.Write(IslandEntries.Count);
                writer.Write(0);

                writer.Write(0);
                writer.Write(0);

                writer.Write(0);
                writer.Write(0);

                List<Tuple<byte, byte>> WaveList1 = new List<Tuple<byte, byte>>();
                List<Tuple<byte, byte>> WaveList2 = new List<Tuple<byte, byte>>();
                List<string> NameList = new List<string>();

                for (int i = 0; i < MapEntries.Count; i++)
                {
                    Tuple<byte, byte> wave1 = new Tuple<byte, byte>(MapEntries[i].WaveBank1, MapEntries[i].WaveBank2);
                    Tuple<byte, byte> wave2 = new Tuple<byte, byte>(MapEntries[i].WaveBank3, MapEntries[i].WaveBank4);

                    if (!WaveList1.Contains(wave1))
                    {
                        MapEntries[i].WaveBankIndex1 = (byte)WaveList1.Count;
                        WaveList1.Add(wave1);
                    }
                    else
                        MapEntries[i].WaveBankIndex1 = (byte)WaveList1.IndexOf(wave1);

                    if (!WaveList2.Contains(wave2))
                    {
                        MapEntries[i].WaveBankIndex2 = (byte)WaveList2.Count;
                        WaveList2.Add(wave2);
                    }
                    else
                        MapEntries[i].WaveBankIndex2 = (byte)WaveList2.IndexOf(wave2);

                    if (i != 0)
                        NameList.Add(MapEntries[i].Name);

                    MapEntries[i].Write(writer);
                }

                int pad32delta = WMath.Pad16Delta(writer.BaseStream.Position);
                for (int i = 0; i < pad32delta; i++)
                    writer.Write((byte)0x00);

                writer.BaseStream.Seek(0x0C, SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                for (int i = 0; i < IslandEntries.Count; i++)
                {
                    Tuple<byte, byte> wave1 = new Tuple<byte, byte>(IslandEntries[i].WaveBank1, IslandEntries[i].WaveBank2);
                    Tuple<byte, byte> wave2 = new Tuple<byte, byte>(IslandEntries[i].WaveBank3, IslandEntries[i].WaveBank4);

                    if (!WaveList1.Contains(wave1))
                    {
                        IslandEntries[i].WaveBankIndex1 = (byte)WaveList1.Count;
                        WaveList1.Add(wave1);
                    }
                    else
                        IslandEntries[i].WaveBankIndex1 = (byte)WaveList1.IndexOf(wave1);

                    if (!WaveList2.Contains(wave2))
                    {
                        IslandEntries[i].WaveBankIndex2 = (byte)WaveList2.Count;
                        WaveList2.Add(wave2);
                    }
                    else
                        IslandEntries[i].WaveBankIndex2 = (byte)WaveList2.IndexOf(wave2);

                    IslandEntries[i].Write(writer);
                }

                pad32delta = WMath.Pad16Delta(writer.BaseStream.Position);
                for (int i = 0; i < pad32delta; i++)
                    writer.Write((byte)0x00);

                writer.BaseStream.Seek(0x10, SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                foreach (Tuple<byte, byte> b in WaveList1)
                {
                    writer.Write(b.Item1);
                    writer.Write(b.Item2);
                }

                pad32delta = WMath.Pad16Delta(writer.BaseStream.Position);
                for (int i = 0; i < pad32delta; i++)
                    writer.Write((byte)0x00);

                writer.BaseStream.Seek(0x14, SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                foreach (Tuple<byte, byte> b in WaveList2)
                {
                    writer.Write(b.Item1);
                    writer.Write(b.Item2);
                }

                pad32delta = WMath.Pad16Delta(writer.BaseStream.Position);
                for (int i = 0; i < pad32delta; i++)
                    writer.Write((byte)0x00);

                writer.BaseStream.Seek(0x18, SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                List<int> nameOffsets = new List<int>();

                foreach (string s in NameList)
                {
                    nameOffsets.Add((int)writer.BaseStream.Position);
                    writer.WriteFixedString(s, s.Length + 1);
                }

                pad32delta = WMath.Pad16Delta(writer.BaseStream.Position);
                for (int i = 0; i < pad32delta; i++)
                    writer.Write((byte)0x00);

                writer.BaseStream.Seek(0x1C, SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                foreach (int i in nameOffsets)
                {
                    writer.Write(i);
                }

                pad32delta = WMath.Pad16Delta(writer.BaseStream.Position);
                for (int i = 0; i < pad32delta; i++)
                    writer.Write((byte)0x00);

                bgm_dat.Data = ms.ToArray();
            }

            string file_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Object", "System.arc");
            ArchiveUtilities.WriteArchive(file_path, m_SystemArchive);
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

        private void RefreshMusicNameLists()
        {
            // Try to load names from file if the file exists
            if (File.Exists(SEQUENCE_NAME_DATABASE))
                SequenceNames = new List<string>(File.ReadAllLines(SEQUENCE_NAME_DATABASE));
            // Otherwise generate generic names!
            else
            {
                SequenceNames = new List<string>();
                for (int i = 0; i < SEQUENCE_NAME_COUNT; i++)
                    SequenceNames.Add($"Sequence { i }");
            }

            // Try to load names from file if the file exists
            if (File.Exists(STREAM_NAME_DATABASE))
                StreamNames = new List<string>(File.ReadAllLines(STREAM_NAME_DATABASE));
            // Otherwise generate generic names!
            else
            {
                StreamNames = new List<string>();
                for (int i = 0; i < STREAM_NAME_COUNT; i++)
                    StreamNames.Add($"Stream {i}");
            }
        }

        private void RefreshBankNameList()
        {
            // Try to load names from file if the file exists
            if (File.Exists(BANK_NAME_DATABASE))
                BankNames = new List<string>(File.ReadAllLines(BANK_NAME_DATABASE));
            // Otherwise generate generic names!
            else
            {
                BankNames = new List<string>();
                for (int i = 0; i < BANK_NAME_COUNT; i++)
                    BankNames.Add($"Bank {i}");
            }
        }

        public void UpdateMapNameCombobox(BGMType newType)
        {
            if (newType == BGMType.Stream)
            {
                m_MinitorWindow.NameMap_ComboBox.ItemsSource = StreamNames;
            }
            else
            {
                m_MinitorWindow.NameMap_ComboBox.ItemsSource = SequenceNames;
            }

            m_MinitorWindow.NameMap_ComboBox.SelectedIndex = m_SelectedMapEntry.ID;
        }

        public void UpdateIslandNameCombobox(BGMType newType)
        {
            if (newType == BGMType.Stream)
            {
                m_MinitorWindow.NameIsland_ComboBox.ItemsSource = StreamNames;
            }
            else
            {
                m_MinitorWindow.NameIsland_ComboBox.ItemsSource = SequenceNames;
            }

            m_MinitorWindow.NameIsland_ComboBox.SelectedIndex = m_SelectedIslandEntry.ID;
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
