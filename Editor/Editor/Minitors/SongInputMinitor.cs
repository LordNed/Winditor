using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.IO;
using WArchiveTools;
using WArchiveTools.FileSystem;
using WindEditor.Minitors.Input;
using WindEditor.ViewModel;
using Newtonsoft.Json;
using GameFormatReader.Common;
using System.Windows.Media.Imaging;
using System.Globalization;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WindEditor.Minitors
{
    class NoteCountFourSixToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ValueAsInt = (int)value;
            if (ValueAsInt == 4 || ValueAsInt == 6)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    class NoteCountSixToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ValueAsInt = (int)value;
            if (ValueAsInt == 6)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class SongInputMinitor : IMinitor, INotifyPropertyChanged
    {
        #region IMinitor Interface
        public MenuItem GetMenuItem()
        {
            return new MenuItem()
            {
                Header = "Song Input Editor",
                ToolTip = "Editor for the inputs required to trigger a Wind Waker song.",
                Command = OpenMinitorCommand,
            };
        }

        public void InitModule(WDetailsViewViewModel details_view_model)
        {

        }

        public bool RequestCloseModule()
        {
            if (!m_IsDataDirty)
                return true;

            MessageBoxResult result = MessageBox.Show("You have unsaved changes to Wind Waker song inputs. Save them?", "Unsaved Song Input Changes", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    OnRequestSaveSongInputData();
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

        public const int SONGS_OFFSET = 0x3985F4;
        public const int NUM_SONGS = 8;

        public ICommand OpenMinitorCommand
        {
            get { return new RelayCommand(x => OnRequestOpenInputEditor(), x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); }
        }
        public ICommand ApplyPlaybackCommand
        {
            get { return new RelayCommand(x => OnRequestApplyPlaybackPatch(), x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); }
        }
        public ICommand SaveFileCommand
        {
            get { return new RelayCommand(x => OnRequestSaveSongInputData(), x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); }
        }
        public ICommand SaveAsFileCommand
        {
            get { return new RelayCommand(x => OnRequestSaveSongInputDataAs(), x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); }
        }

        public string WindowTitle
        {
            get { return m_IsDataDirty ? m_WindowTitle + "*" : m_WindowTitle; }
            set
            {
                if (value != m_WindowTitle)
                {
                    m_WindowTitle = value;
                    OnPropertyChanged("WindowTitle");
                }
            }
        }

        public WindWakerSong[] Songs
        {
            get { return m_Songs; }
            set
            {
                if (value != m_Songs)
                {
                    m_Songs = value;
                    OnPropertyChanged("Songs");
                }
            }
        }

        public WindWakerSong CurrentSong
        {
            get { return m_CurrentSong; }
            set
            {
                if (value != m_CurrentSong)
                {
                    m_CurrentSong = value;
                    OnPropertyChanged("CurrentSong");

                    CurrentSong.Notes.CollectionChanged -= OnCurrentSongNotesChanged;
                    OnCurrentSongChanged();
                }
            }
        }

        private InputMinitorWindow m_MinitorWindow;

        private string m_WindowTitle;
        private bool m_IsDataDirty;
        
        private WindWakerSong[] m_Songs;
        private WindWakerSong m_CurrentSong;

        private void OnRequestOpenInputEditor()
        {
            if (m_MinitorWindow != null)
            {
                m_MinitorWindow.Show();
                m_MinitorWindow.Focus();
                return;
            }

            WindowTitle = "Song Input Editor - " + Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol");

            m_MinitorWindow = new InputMinitorWindow();
            m_MinitorWindow.DataContext = this;
            m_MinitorWindow.Closing += M_MinitorWindow_Closing;

            LoadSongData();

            m_MinitorWindow.Show();
            m_MinitorWindow.NoteCountCombo.SelectionChanged += OnCurrentSongNoteCountChanged;
        }

        private void M_MinitorWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            m_MinitorWindow.Hide();
        }

        private void OnCurrentSongNoteCountChanged(object sender, SelectionChangedEventArgs e)
        {
            m_IsDataDirty = true;
            OnPropertyChanged("WindowTitle");
        }

        private void LoadSongData()
        {
            const string NameFile = "resources/WindWakerSongNames.txt";
            string[] SongNames;

            Songs = new WindWakerSong[NUM_SONGS];

            // Try to load names from file if the file exists
            if (File.Exists(NameFile))
                SongNames = File.ReadAllLines("resources/WindWakerSongNames.txt");
            // Otherwise generate generic names!
            else
            {
                SongNames = new string[NUM_SONGS];

                for (int i = 0; i < NUM_SONGS; i++)
                    SongNames[i] = $"Song { i }";
            }

            using (FileStream stream = new FileStream(Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol"), FileMode.Open, FileAccess.Read))
            {
                EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);
                reader.BaseStream.Seek(SONGS_OFFSET, SeekOrigin.Begin);

                for (int i = 0; i < NUM_SONGS; i++)
                {
                    Songs[i] = new WindWakerSong(reader);

                    // Grab name from the list we loaded/generated earlier
                    if (SongNames.Length > i)
                        Songs[i].Name = SongNames[i];
                    // If there aren't enough names in the list we read from file, default to a generic name!
                    else
                        Songs[i].Name = $"Song { i }";
                }
            }

            CurrentSong = Songs[0];
        }

        private void OnCurrentSongChanged()
        {
            for (int i = 0; i < WindWakerSong.MAX_NOTES; i++)
            {
                Image img = m_MinitorWindow.ImgHost.Children[i] as Image;
                img.Source = GetNoteIcon(CurrentSong.Notes[i]);
            }

            CurrentSong.Notes.CollectionChanged += OnCurrentSongNotesChanged;
        }

        private void OnCurrentSongNotesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Image img = m_MinitorWindow.ImgHost.Children[e.NewStartingIndex] as Image;
            img.Source = GetNoteIcon(CurrentSong.Notes[e.NewStartingIndex]);

            m_IsDataDirty = true;
            OnPropertyChanged("WindowTitle");
        }

        private BitmapImage GetNoteIcon(WindWakerNote Note)
        {
            string note_name = "WWempty";
            switch (Note)
            {
                case WindWakerNote.Down:
                    note_name = "WWdown";
                    break;
                case WindWakerNote.Left:
                    note_name = "WWLeft";
                    break;
                case WindWakerNote.Middle:
                    note_name = "WWMiddleNew";
                    break;
                case WindWakerNote.Right:
                    note_name = "WWright";
                    break;
                case WindWakerNote.Up:
                    note_name = "WWup";
                    break;
                default:
                    break;
            }

            // See https://stackoverflow.com/a/1651397
            return new BitmapImage(new Uri($"pack://application:,,,/Winditor;component/resources/icons/{ note_name }.png"));
        }

        private void OnRequestSaveSongInputData()
        {
            using (FileStream stream = new FileStream(Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol"), FileMode.Open, FileAccess.Write))
            {
                EndianBinaryWriter writer = new EndianBinaryWriter(stream, Endian.Big);
                writer.BaseStream.Seek(SONGS_OFFSET, SeekOrigin.Begin);

                for (int i = 0; i < NUM_SONGS; i++)
                {
                    Songs[i].Write(writer);
                }
            }

            m_IsDataDirty = false;
            OnPropertyChanged("WindowTitle");
        }

        private void OnRequestSaveSongInputDataAs()
        {
            var sfd = new CommonSaveFileDialog()
            {
                Title = "Save DOL Copy",
                DefaultExtension = "dol",
                AlwaysAppendDefaultExtension = true,
            };
            sfd.Filters.Add(new CommonFileDialogFilter("DOL files", ".dol"));

            if (sfd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                File.Copy(Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol"), sfd.FileName);

                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Open, FileAccess.Write))
                {
                    EndianBinaryWriter writer = new EndianBinaryWriter(stream, Endian.Big);
                    writer.BaseStream.Seek(SONGS_OFFSET, SeekOrigin.Begin);

                    for (int i = 0; i < NUM_SONGS; i++)
                    {
                        Songs[i].Write(writer);
                    }
                }

                m_IsDataDirty = false;
                OnPropertyChanged("WindowTitle");
            }
        }

        private void OnRequestApplyPlaybackPatch()
        {
            if (MessageBox.Show("This will apply a patch to the *.dol file displayed in thie editor's title bar that permanently removes song playback when conducting Wind Waker songs.\n\nAre you sure you want to continue?", "Apply Patch?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Patch songPlaybackPatch = JsonConvert.DeserializeObject<Patch>(File.ReadAllText(@"resources\patches\disable_song_playback.json"));
                songPlaybackPatch.Apply(WSettingsManager.GetSettings().RootDirectoryPath);
            }
        }
    }
}
