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

namespace WindEditor.Minitors
{
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
            if (m_MinitorWindow != null && m_MinitorWindow.IsVisible == true)
            {
                m_MinitorWindow.Focus();
                return;
            }

            WindowTitle = "Song Input Editor - " + Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol");

            m_MinitorWindow = new InputMinitorWindow();
            m_MinitorWindow.DataContext = this;

            LoadSongData();

            m_MinitorWindow.Show();
        }

        private void LoadSongData()
        {
            Songs = new WindWakerSong[NUM_SONGS];

            using (FileStream stream = new FileStream(Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol"), FileMode.Open, FileAccess.Read))
            {
                EndianBinaryReader reader = new EndianBinaryReader(stream, Endian.Big);
                reader.BaseStream.Seek(SONGS_OFFSET, SeekOrigin.Begin);

                for (int i = 0; i < NUM_SONGS; i++)
                {
                    Songs[i] = new WindWakerSong(reader);
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
