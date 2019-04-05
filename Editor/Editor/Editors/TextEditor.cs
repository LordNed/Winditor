using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using WindEditor.ViewModel;
using WindEditor.Editor;
using WArchiveTools;
using WArchiveTools.FileSystem;
using System.IO;
using GameFormatReader.Common;
using WindEditor.Editors.Text;
using System.Windows.Input;
using System.Windows.Data;

namespace WindEditor.Editors
{
    public struct MessageReference
    {
        public uint MessageID;
        public uint MessageIndex;
    }

    public class TextEditor : IEditor, INotifyPropertyChanged
    {
        #region IEditor Interface
        public MenuItem GetMenuItem()
        {
            return new MenuItem()
            {
                Header = "Text Editor",
                ToolTip = "Edits text :)",
                Command = OpenEditorCommand
            };
        }

        public void InitModule(WDetailsViewViewModel details_view_model)
        {
            details_view_model.TypeCustomizations.Add(typeof(MessageReference).Name, new MessageReferenceTypeCustomization(this));
        }

        public bool RequestCloseModule()
        {
            return true;
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

        public ICommand OpenEditorCommand { get { return new RelayCommand(x => OnRequestOpenTextEditor()); } }
        public ICommand SaveMessageDataCommand { get { return new RelayCommand(x => OnRequestSaveMessageData()); } }
        public ICommand SaveMessageDataAsCommand { get { return new RelayCommand(x => OnRequestSaveMessageDataAs()); } }

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
                    m_SelectedMessage = value;
                    OnPropertyChanged("SelectedMessage");

                    if (m_DetailsModel != null)
                    {
                        m_DetailsModel.ReflectObject(m_SelectedMessage);
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

                    if (m_EditorWindow != null)
                    {
                        CollectionViewSource.GetDefaultView(m_EditorWindow.TextListView.ItemsSource).Refresh();
                    }
                }
            }
        }

        public TextEncoding OriginalEncoding
        {
            get { return m_OriginalEncoding; }
            set
            {
                if (value != m_OriginalEncoding)
                {
                    m_OriginalEncoding = value;
                    OnPropertyChanged("OriginalEncoding");
                }
            }
        }

        private TextEditorWindow m_EditorWindow;
        private WDetailsViewViewModel m_DetailsModel;

        private List<Message> m_Messages;
        private Message m_SelectedMessage;
        private string m_SearchFilter;
        private VirtualFilesystemDirectory m_MessageArchive;

        private TextEncoding m_OriginalEncoding;

        public void OnRequestOpenTextEditor(int default_message = 0)
        {
            if (m_EditorWindow != null && m_EditorWindow.IsVisible == true)
            {
                m_EditorWindow.Focus();
                return;
            }

            LoadMessageArchive();

            m_EditorWindow = new TextEditorWindow();
            m_EditorWindow.DataContext = this;
            m_DetailsModel = (WDetailsViewViewModel)m_EditorWindow.DetailsPanel.DataContext;

            SelectedMessage = Messages[default_message];
            m_EditorWindow.Show();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(m_EditorWindow.TextListView.ItemsSource);
            view.Filter = FilterMessages;

            SearchFilter = "";
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
                    return mes.Text.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
        }

        private void OnRequestSaveMessageData()
        {
            SaveMessageArchive(Path.Combine(Properties.Settings.Default.RootDirectory, "files", "res", "Msg", "bmgres.arc"));
        }

        private void OnRequestSaveMessageDataAs()
        {

        }

        private void LoadMessageArchive()
        {
            string bmgres_path = Path.Combine(Properties.Settings.Default.RootDirectory, "files", "res", "Msg", "bmgres.arc");

            m_MessageArchive = ArchiveUtilities.LoadArchive(bmgres_path);

            VirtualFilesystemFile text_bank = m_MessageArchive.GetFileAtPath("zel_00.bmg");

            using (MemoryStream strm = new MemoryStream(text_bank.Data))
            {
                EndianBinaryReader reader = new EndianBinaryReader(strm, Endian.Big);
                LoadMessageData(reader);
            }
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

                    bmg_writer.Write("DAT1".ToCharArray());
                    bmg_writer.Write((uint)text_data_strm.Length + 8);

                    bmg_writer.Write(text_data_strm.ToArray());
                }

                text_bank.Data = new_bmg_strm.ToArray();
            }
        }
    }
}
