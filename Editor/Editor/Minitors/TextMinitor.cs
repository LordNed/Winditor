using GameFormatReader.Common;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WArchiveTools;
using WArchiveTools.FileSystem;
using WindEditor.Minitors.Text;
using WindEditor.ViewModel;

namespace WindEditor.Minitors
{
    public class MessageReference : INotifyPropertyChanged
    {
        private ushort m_MessageID;

        public ushort MessageID
        {
            get { return m_MessageID; }
            set
            {
                if (value != m_MessageID)
                {
                    m_MessageID = value;
                    OnPropertyChanged("MessageID");
                }
            }
        }

        public MessageReference(ushort id)
        {
            MessageID = id;
        }

        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class TextMinitor : IMinitor, INotifyPropertyChanged
    {
        #region IMinitor Interface
        public MenuItem GetMenuItem()
        {
            return new MenuItem()
            {
                Header = "Text Editor",
                ToolTip = "Editor for the game's main text bank.",
                Command = OpenMinitorCommand,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };
        }

        public void InitModule(WDetailsViewViewModel details_view_model)
        {
            details_view_model.TypeCustomizations.Add(typeof(MessageReference).Name, new MessageReferenceTypeCustomization(this));
        }

        public bool RequestCloseModule()
        {
            if (!m_IsDataDirty)
                return true;

            MessageBoxResult result = MessageBox.Show("You have unsaved changes to the text data. Save them?", "Unsaved Text Changes", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    OnRequestSaveMessageData();
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

        public ICommand OpenMinitorCommand { get { return new RelayCommand(x => OnRequestOpenTextEditor(),
            x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); } }
        public ICommand SaveMessageDataCommand { get { return new RelayCommand(x => OnRequestSaveMessageData()); } }
        public ICommand SaveMessageDataAsCommand { get { return new RelayCommand(x => OnRequestSaveMessageDataAs()); } }

        public ICommand SaveMessageDataAsTxtCommand { get { return new RelayCommand(x => OnRequestSaveOtherFormatMessageData()); } }
        public ICommand AddMessageCommand { get { return new RelayCommand(x => OnRequestAddMessage()); } }
        public ICommand OpenTutorialCommand { get { return new RelayCommand(x => OnRequestOpenTutorial()); } }

        public List<Message> Messages
        {
            get { return m_Messages; }
            set
            {
                if (value != m_Messages)
                {
                    m_Messages = value;
                    OnPropertyChanged("Messages");
                }
            }
        }

        public Message SelectedMessage
        {
            get { return m_SelectedMessage; }
            set
            {
                if (value != m_SelectedMessage)
                {
                    if (m_SelectedMessage != null)
                    {
                        m_SelectedMessage.PropertyChanged -= OnSelectedMessagePropertyChanged;
                    }

                    m_SelectedMessage = value;
                    OnPropertyChanged("SelectedMessage");

                    if (m_DetailsModel != null)
                    {
                        m_DetailsModel.ReflectObject(m_SelectedMessage);
                    }

                    if (m_SelectedMessage != null)
                    {
                        m_SelectedMessage.PropertyChanged += OnSelectedMessagePropertyChanged;
                    }
                }
            }
        }

        public string SearchFilter
        {
            get { return m_SearchFilter; }
            set
            {
                if (value != m_SearchFilter)
                {
                    m_SearchFilter = value;
                    OnPropertyChanged("SearchFilter");

                    if (m_MinitorWindow != null)
                    {
                        CollectionViewSource.GetDefaultView(m_MinitorWindow.TextListView.ItemsSource).Refresh();
                    }
                }
            }
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

        public TextEncoding OriginalEncoding
        {
            get
            {
                return m_OriginalEncoding;
            }
            set
            {
                if (value != m_OriginalEncoding)
                {
                    m_OriginalEncoding = value;
                    OnPropertyChanged("OriginalEncoding");
                }
            }
        }

        private TextMinitorWindow m_MinitorWindow;
        private WDetailsViewViewModel m_DetailsModel;

        private List<Message> m_Messages;
        private Message m_SelectedMessage;
        private VirtualFilesystemDirectory m_MessageArchive;

        private string m_SearchFilter;
        private string m_WindowTitle;

        private TextEncoding m_OriginalEncoding;

        private bool m_IsDataDirty;

        public void OnRequestOpenTextEditor()
        {
            if (m_MinitorWindow != null && m_MinitorWindow.IsVisible == true)
            {
                m_MinitorWindow.Focus();
                return;
            }

            if (Messages == null)
            {
                if (!TryLoadMessageArchive())
                {
                    MessageBox.Show($"The file " +
                        $"\"{ Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Msg", "bmgres.arc") }\" " +
                        $"could not be found. The text editor cannot be opened.\n\n" +
                        $"Please check that the Root Directory in your settings includes this file.",
                        "Archive Not Found", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }

            WindowTitle = "Text Editor - " + Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Msg", "bmgres.arc");

            m_MinitorWindow = new TextMinitorWindow();
            m_MinitorWindow.DataContext = this;
            m_DetailsModel = (WDetailsViewViewModel)m_MinitorWindow.DetailsPanel.DataContext;

            SelectedMessage = Messages[0];
            m_MinitorWindow.Show();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(m_MinitorWindow.TextListView.ItemsSource);
            view.Filter = FilterMessages;

            SearchFilter = "";
        }

        private void OnRequestSaveMessageData()
        {
            SaveMessageArchive(Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Msg", "bmgres.arc"));
        }

        private void OnRequestSaveMessageDataAs()
        {
            var sfd = new CommonSaveFileDialog()
            {
                Title = "Export Script Archive",
                DefaultExtension = "arc",
                AlwaysAppendDefaultExtension = true,
            };
            sfd.Filters.Add(new CommonFileDialogFilter("Archives", ".arc"));
            if (WSettingsManager.GetSettings().LastScriptArchivePath.FilePath != "")
            {
                sfd.InitialDirectory = WSettingsManager.GetSettings().LastScriptArchivePath.FilePath;
            }

            if (sfd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SaveMessageArchive(sfd.FileName);

                WSettingsManager.GetSettings().LastScriptArchivePath.FilePath = Path.GetDirectoryName(sfd.FileName);
                WSettingsManager.SaveSettings();
            }
        }

        private void OnRequestSaveOtherFormatMessageData()
        {
            var sfd = new CommonSaveFileDialog()
            {
                Title = "Export Script Archive",
                DefaultExtension = "txt",
                AlwaysAppendDefaultExtension = true,
            };
            sfd.Filters.Add(new CommonFileDialogFilter("Archives", "txt"));
            if (WSettingsManager.GetSettings().LastScriptArchivePath.FilePath != "")
            {
                sfd.InitialDirectory = WSettingsManager.GetSettings().LastScriptArchivePath.FilePath;
            }

            if (sfd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SaveMessageOther(sfd.FileName);

                WSettingsManager.GetSettings().LastScriptArchivePath.FilePath = Path.GetDirectoryName(sfd.FileName);
                WSettingsManager.SaveSettings();
            }
        }

        private void OnRequestAddMessage()
        {
            ushort highest_msg_id = GetHighestID();

            // There are many empty message entries in the vanilla file.
            // We will first try to find a message with an ID of 0.
            Message new_message = Messages.Find(x => x.MessageID == 0);

            // If we find a message with a MessageID of 0, we will give it a valid
            // ID and focus it for the user.
            if (new_message != null)
            {
                new_message.MessageID = (ushort)(highest_msg_id + 1);
                new_message.LineCount = 1;
                new_message.ItemImage = ItemID.No_item;
            }
            // If the user has used up all the blank messages, we have no
            // choice but to add a completely new message to the file.
            else
            {
                new_message = new Message();
                new_message.MessageID = (ushort)(highest_msg_id + 1);
                new_message.Index = Messages.Count;

                Messages.Add(new_message);
                OnPropertyChanged("Messages");
            }

            // This allows us to update the UI to show the new MessageID even if the new message
            // is on-screen when ScrollIntoView() is called.
            ICollectionView view = CollectionViewSource.GetDefaultView(m_MinitorWindow.TextListView.ItemsSource);
            view.Refresh();

            m_MinitorWindow.TextListView.SelectedItem = new_message;
            m_MinitorWindow.TextListView.ScrollIntoView(new_message);
        }

        private void OnRequestOpenTutorial()
        {
            System.Diagnostics.Process.Start("https://lordned.github.io/Winditor/tutorials/text/text.html");
        }

        private ushort GetHighestID()
        {
            ushort highest_id = 0;

            foreach (Message mes in Messages)
            {
                if (highest_id < mes.MessageID)
                    highest_id = mes.MessageID;
            }

            return highest_id;
        }

        private bool FilterMessages(object item)
        {
            if (string.IsNullOrEmpty(SearchFilter))
            {
                return true;
            }
            else
            {
                Message mes = item as Message;

                if (SearchFilter.Contains(':'))
                {
                    string[] split_filter = SearchFilter.Split(':');

                    if (split_filter.Length < 2 || !int.TryParse(split_filter[1], out int val))
                    {
                        return true;
                    }

                    if (split_filter[0].ToLowerInvariant() == "msgid")
                    {
                        return mes.MessageID == val;
                    }
                    else if (split_filter[0].ToLowerInvariant() == "index")
                    {
                        return Messages.IndexOf(mes) == val;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return mes.Text.Replace("\n", " ").IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
        }

        private bool TryLoadMessageArchive()
        {
            string bmgres_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Msg", "bmgres.arc");

            if (!File.Exists(bmgres_path))
            {
                return false;
            }

            m_MessageArchive = ArchiveUtilities.LoadArchive(bmgres_path);

            VirtualFilesystemFile text_bank = m_MessageArchive.GetFileAtPath("zel_00.bmg");

            using (MemoryStream strm = new MemoryStream(text_bank.Data))
            {
                EndianBinaryReader reader = new EndianBinaryReader(strm, Endian.Big);
                LoadMessageData(reader);
            }

            return true;
        }

        private void LoadMessageData(EndianBinaryReader reader)
        {
            List<Message> new_message_list = new List<Message>();

            string file_magic = reader.ReadString(8);
            int file_size = reader.ReadInt32();
            int section_count = reader.ReadInt32();

            m_OriginalEncoding = (TextEncoding)reader.ReadByte();

            reader.Skip(15);

            int inf1_offset = (int)reader.BaseStream.Position;

            string inf1_magic = reader.ReadString(4);
            int inf1_size = reader.ReadInt32();
            ushort message_count = reader.ReadUInt16();
            short message_size = reader.ReadInt16();

            reader.Skip(4);

            int text_bank_start = inf1_offset + inf1_size + 8;

            Encoding enc = Encoding.ASCII;
            switch (m_OriginalEncoding)
            {
                case TextEncoding.CP1252:
                    enc = Encoding.GetEncoding(1252);
                    break;
                case TextEncoding.Shift_JIS:
                    enc = Encoding.GetEncoding(932);
                    break;
                case TextEncoding.UTF_16:
                    enc = Encoding.BigEndianUnicode;
                    break;
                case TextEncoding.UTF_8:
                    enc = Encoding.UTF8;
                    break;
            }

            for (int i = 0; i < message_count; i++)
            {
                Message msg = new Message(reader, text_bank_start, enc);
                msg.Index = i;

                new_message_list.Add(msg);
            }

            Messages = new_message_list;
        }

        private void SaveMessageArchive(string file_path)
        {
            SaveMessageData();

            ArchiveUtilities.WriteArchive(file_path, m_MessageArchive);
        }

        private void SaveMessageOther(string filename)
        {
            var rawText = new StringBuilder();

            foreach (Message m in m_Messages)
            {
                rawText.Append(m.Index + "::: ");
                rawText.Append(m.Text);
                rawText.AppendLine("\n");
            }

            File.AppendAllText(filename, rawText.ToString());
            rawText.Clear();
        }

        private void SaveMessageData()
        {
            VirtualFilesystemFile text_bank = m_MessageArchive.GetFileAtPath("zel_00.bmg");

            Encoding enc = Encoding.ASCII;
            switch (m_OriginalEncoding)
            {
                case TextEncoding.CP1252:
                    enc = Encoding.GetEncoding(1252);
                    break;
                case TextEncoding.Shift_JIS:
                    enc = Encoding.GetEncoding(932);
                    break;
                case TextEncoding.UTF_16:
                    enc = Encoding.BigEndianUnicode;
                    break;
                case TextEncoding.UTF_8:
                    enc = Encoding.UTF8;
                    break;
            }

            using (MemoryStream new_bmg_strm = new MemoryStream())
            {
                EndianBinaryWriter bmg_writer = new EndianBinaryWriter(new_bmg_strm, Endian.Big);
                bmg_writer.Write("MESGbmg1".ToCharArray());
                bmg_writer.Write(0);
                bmg_writer.Write(2);
                bmg_writer.Write((byte)m_OriginalEncoding);
                bmg_writer.Write(new byte[15]);

                using (MemoryStream text_data_strm = new MemoryStream())
                {
                    EndianBinaryWriter text_data_writer = new EndianBinaryWriter(text_data_strm, Endian.Big);
                    text_data_writer.Write((byte)0);

                    using (MemoryStream message_data_strm = new MemoryStream())
                    {
                        EndianBinaryWriter message_data_writer = new EndianBinaryWriter(message_data_strm, Endian.Big);

                        foreach (Message m in m_Messages)
                        {
                            m.Save(message_data_writer, text_data_writer, enc);
                        }

                        int delta = WMath.Pad16Delta(message_data_strm.Length);

                        bmg_writer.Write("INF1".ToCharArray());
                        bmg_writer.Write((uint)(message_data_strm.Length + 16 + delta));
                        bmg_writer.Write((ushort)m_Messages.Count);
                        bmg_writer.Write((ushort)0x18);
                        bmg_writer.Write(0);

                        bmg_writer.Write(message_data_strm.ToArray());

                        for (int i = 0; i < delta; i++)
                            bmg_writer.Write((byte)0);
                    }

                    // Seek to the end of the stream, and then pad us to 32-byte alignment.
                    text_data_strm.Seek(0, SeekOrigin.End);
                    int pad32delta = WMath.Pad32Delta(text_data_strm.Position + 8);
                    for (int i = 0; i < pad32delta; i++)
                        text_data_writer.Write((byte)0x00);

                    bmg_writer.Write("DAT1".ToCharArray());
                    bmg_writer.Write((uint)text_data_strm.Length + 8);

                    bmg_writer.Write(text_data_strm.ToArray());
                }

                text_bank.Data = new_bmg_strm.ToArray();
            }

            m_IsDataDirty = false;
            OnPropertyChanged("WindowTitle");
        }

        private void OnSelectedMessagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            m_IsDataDirty = true;
            OnPropertyChanged("WindowTitle");
        }

        public void OnUserRequestOpenReference(ushort id)
        {
            OnRequestOpenTextEditor();

            Message requested_message = Messages.Find(x => x.MessageID == id);

            if (requested_message != null)
            {
                m_MinitorWindow.TextListView.SelectedItem = requested_message;
                m_MinitorWindow.TextListView.ScrollIntoView(requested_message);
            }
            else
            {

            }
        }
    }
}
