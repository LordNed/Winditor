using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using WindEditor.ViewModel;
using WindEditor.Editor;
using WArchiveTools.FileSystem;
using System.IO;
using GameFormatReader.Common;
using WindEditor.Editors.Text;
using System.Windows.Input;

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

            LoadMessageArchive();
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

        private TextEditorWindow m_EditorWindow;
        private WDetailsViewViewModel m_DetailsModel;

        private List<Message> m_Messages;
        private Message m_SelectedMessage;
        private VirtualFilesystemDirectory m_MessageArchive;

        public void OnRequestOpenTextEditor(int default_message = 0)
        {
            m_EditorWindow = new TextEditorWindow();
            m_EditorWindow.DataContext = this;
            m_DetailsModel = (WDetailsViewViewModel)m_EditorWindow.DetailsPanel.DataContext;

            SelectedMessage = Messages[default_message];
            m_EditorWindow.Show();
        }

        private void LoadMessageArchive()
        {
            string bmgres_path = Path.Combine(Properties.Settings.Default.RootDirectory, "files", "res", "Msg", "bmgres.arc");

            m_MessageArchive = WArchiveTools.ArchiveUtilities.LoadArchive(bmgres_path);

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

            TextEncoding encoding = (TextEncoding)reader.ReadByte();

            reader.Skip(15);

            int inf1_offset = (int)reader.BaseStream.Position;

            string inf1_magic = reader.ReadString(4);
            int inf1_size = reader.ReadInt32();
            ushort message_count = reader.ReadUInt16();
            short message_size = reader.ReadInt16();

            reader.Skip(4);

            int text_bank_start = inf1_offset + inf1_size + 8;

            Encoding enc = Encoding.ASCII;
            switch (encoding)
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
    }
}
