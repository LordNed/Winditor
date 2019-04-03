using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GameFormatReader.Common;

namespace WindEditor.Editors.Text
{
    [HideCategories(new string[] { })]
    public class Message
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        [WProperty("Textbox", "Style", true, "The style of the box containing the text.")]
        public BoxType TextboxType
        {
            get { return m_TextboxType; }
            set
            {
                if (value != m_TextboxType)
                {
                    m_TextboxType = value;
                    OnPropertyChanged("TextboxType");
                }
            }
        }

        [WProperty("Textbox", "Draw Type", true, "The way the text is initially rendered to the screen - typed out letter by letter or all of it at once.")]
        public DrawType DrawType
        {
            get { return m_DrawType; }
            set
            {
                if (value != m_DrawType)
                {
                    m_DrawType = value;
                    OnPropertyChanged("DrawType");
                }
            }
        }

        [WProperty("Textbox", "Screen Position", true, "The position of the textbox on the screen.")]
        public BoxPosition TextboxPosition
        {
            get { return m_TextboxPosition; }
            set
            {
                if (value != m_TextboxPosition)
                {
                    m_TextboxPosition = value;
                    OnPropertyChanged("TextboxPosition");
                }
            }
        }

        [WProperty("Textbox", "Item Image", true, "The item image to display if the Textbox Type is Get Item.")]
        public ItemID ItemImage
        {
            get { return m_ItemImage; }
            set
            {
                if (value != m_ItemImage)
                {
                    m_ItemImage = value;
                    OnPropertyChanged("ItemImage");
                }
            }
        }

        public string Text
        {
            get { return m_Text; }
            set
            {
                if (value != m_Text)
                {
                    m_Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

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

        public int Index
        {
            get { return m_Index; }
            set
            {
                if (value != m_Index)
                {
                    m_Index = value;
                    OnPropertyChanged("Index");
                }
            }
        }

        private int m_Index;
        private string m_Text;

        private ushort m_MessageID;
        private ushort m_ItemPrice;
        private ushort m_NextMessageID;
        private ushort m_Unknown1;
        private BoxType m_TextboxType;
        private DrawType m_DrawType;
        private BoxPosition m_TextboxPosition;
        private ItemID m_ItemImage;
        private bool m_Unknown2;
        private byte m_InitialSound;
        private byte m_InitialCamera;
        private byte m_InitialAnimation;
        private ushort m_LineCount;

        public Message(EndianBinaryReader reader, int text_bank_offset, Encoding text_encoding)
        {
            uint text_data_offset = reader.ReadUInt32();

            m_MessageID = reader.ReadUInt16();
            m_ItemPrice = reader.ReadUInt16();
            m_NextMessageID = reader.ReadUInt16();

            m_Unknown1 = reader.ReadUInt16();

            m_TextboxType = (BoxType)reader.ReadByte();
            m_DrawType = (DrawType)reader.ReadByte();
            m_TextboxPosition = (BoxPosition)reader.ReadByte();
            m_ItemImage = (ItemID)reader.ReadByte();

            m_Unknown2 = reader.ReadBoolean();

            m_InitialSound = reader.ReadByte();
            m_InitialCamera = reader.ReadByte();
            m_InitialAnimation = reader.ReadByte();

            reader.SkipByte();

            m_LineCount = reader.ReadUInt16();

            reader.SkipByte();

            int next_message_pos = (int)reader.BaseStream.Position;

            reader.BaseStream.Seek(text_bank_offset + text_data_offset, System.IO.SeekOrigin.Begin);

            List<byte> msg_bytes = new List<byte>();

            byte test_byte = reader.ReadByte();

            while (test_byte != 0)
            {
                if (test_byte == 0x1A)
                {
                    byte code_size = reader.ReadByte();
                    byte[] code_data = reader.ReadBytes(code_size - 2);

                    msg_bytes.AddRange(ProcessControlCode(code_size, code_data));
                }
                else
                {
                    msg_bytes.Add(test_byte);
                }

                test_byte = reader.ReadByte();
            }

            Text = text_encoding.GetString(msg_bytes.ToArray());

            reader.BaseStream.Seek(next_message_pos, System.IO.SeekOrigin.Begin);
        }

        private byte[] ProcessControlCode(byte size, byte[] data)
        {
            List<byte> result = new List<byte>();
            result.Add((byte)'['); // Tag start

            if (size == 5)
            {
                switch (data[0])
                {
                    case 0:
                        string five_name = Enum.GetName(typeof(FiveByteCode), data[2]);
                        result.AddRange(Encoding.ASCII.GetBytes(five_name.ToLowerInvariant()));
                        break;
                    case 1:
                        result.AddRange(Encoding.ASCII.GetBytes("sound="));

                        short sound_id = BitConverter.ToInt16(new byte[] { data[2], data[1] }, 0);
                        result.AddRange(Encoding.ASCII.GetBytes(sound_id.ToString()));
                        break;
                    case 2:
                        result.AddRange(Encoding.ASCII.GetBytes("camera="));

                        short cam_id = BitConverter.ToInt16(new byte[] { data[2], data[1] }, 0);
                        result.AddRange(Encoding.ASCII.GetBytes(cam_id.ToString()));
                        break;
                    case 3:
                        result.AddRange(Encoding.ASCII.GetBytes("animation="));

                        short anim_id = BitConverter.ToInt16(new byte[] { data[2], data[1] }, 0);
                        result.AddRange(Encoding.ASCII.GetBytes(anim_id.ToString()));
                        break;
                }
            }
            else if (size == 6)
            {
                string color_name = Enum.GetName(typeof(TextColor), data[3]);
                result.AddRange(Encoding.ASCII.GetBytes(color_name));
            }
            else if (size == 7)
            {
                short seven_type = BitConverter.ToInt16(new byte[] { data[2], data[1] }, 0);
                short seven_arg = BitConverter.ToInt16(new byte[] { data[4], data[3] }, 0);

                string seven_name = Enum.GetName(typeof(SevenByteCode), seven_type);

                result.AddRange(Encoding.ASCII.GetBytes(seven_name));
                result.Add((byte)'=');
                result.AddRange(Encoding.ASCII.GetBytes(seven_arg.ToString()));
            }

            result.Add((byte)']'); // Tag end
            return result.ToArray(); ;
        }
    }
}
