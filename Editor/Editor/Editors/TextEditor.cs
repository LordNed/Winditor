using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WindEditor.ViewModel;
using WindEditor.Editor;
using WArchiveTools.FileSystem;
using System.IO;
using GameFormatReader.Common;

namespace WindEditor.Editors
{
    public struct MessageReference
    {
        public uint MessageID;
        public uint MessageIndex;
    }

    public class Message
    {

    }

    public class TextEditor : IEditor
    {
        public MenuItem GetMenuItem()
        {
            return new MenuItem()
            {
                Header = "Text Editor",
                ToolTip = "Edits text :)"
            };
        }

        private List<Message> m_Messages;
        private VirtualFilesystemDirectory m_MessageArchive;

        public void InitModule(WDetailsViewViewModel details_view_model)
        {
            details_view_model.TypeCustomizations.Add(typeof(MessageReference).Name, new MessageReferenceTypeCustomization(this));

            LoadMessageArchive();
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

        }
    }
}
