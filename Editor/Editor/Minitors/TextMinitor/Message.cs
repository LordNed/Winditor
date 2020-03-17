using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GameFormatReader.Common;

namespace WindEditor.Minitors.Text
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

        [WProperty("Textbox", "Line Count", true, "The maximum number of lines displayed in one textbox.")]
        public short LineCount
        {
            get { return (short)m_LineCount; }
            set
            {
                if (value != m_LineCount)
                {
                    m_LineCount = (ushort)value;
                    OnPropertyChanged("LineCount");
                }
            }
        }

        [WProperty("Misc.", "Initial Animation", true, "An animation for the NPC speaking the message to run when the textbox is first displayed.")]
        public byte InitialAnimation
        {
            get { return m_InitialAnimation; }
            set
            {
                if (value != m_InitialAnimation)
                {
                    m_InitialAnimation = value;
                    OnPropertyChanged("InitialAnimation");
                }
            }
        }

        [WProperty("Misc.", "Initial Camera Position", true, "Not completely known. Index of a camera position to put the camera at when the textbox is first displayed?")]
        public byte InitialCamera
        {
            get { return m_InitialCamera; }
            set
            {
                if (value != m_InitialCamera)
                {
                    m_InitialCamera = value;
                    OnPropertyChanged("InitialCamera");
                }
            }
        }

        [WProperty("Misc.", "Initial Sound", true, "A sound played when the textbox is first displayed.")]
        public byte InitialSound
        {
            get { return m_InitialSound; }
            set
            {
                if (value != m_InitialSound)
                {
                    m_InitialSound = value;
                    OnPropertyChanged("InitialSound");
                }
            }
        }

        [WProperty("Misc.", "Item Price", true, "Not completely known. The price of an item being sold by shopkeepers?")]
        public short ItemPrice
        {
            get { return (short)m_ItemPrice; }
            set
            {
                if (value != m_ItemPrice)
                {
                    m_ItemPrice = (ushort)value;
                    OnPropertyChanged("ItemPrice");
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
        private byte m_Unknown2;
        private byte m_InitialSound;
        private byte m_InitialCamera;
        private byte m_InitialAnimation;
        private ushort m_LineCount;

        public Message()
        {
            LineCount = 1;
            m_ItemImage = ItemID.No_item;
        }

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

            m_Unknown2 = reader.ReadByte();

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

        public void Save(EndianBinaryWriter message_data_stream, EndianBinaryWriter text_data_stream, Encoding enc)
        {
            if (m_MessageID != 0)
            {
                message_data_stream.Write((int)text_data_stream.BaseStream.Length);
            }
            else
            {
                message_data_stream.Write(0);
            }

            message_data_stream.Write(m_MessageID);
            message_data_stream.Write(m_ItemPrice);
            message_data_stream.Write(m_NextMessageID);

            message_data_stream.Write(m_Unknown1);

            message_data_stream.Write((byte)m_TextboxType);
            message_data_stream.Write((byte)m_DrawType);
            message_data_stream.Write((byte)m_TextboxPosition);
            message_data_stream.Write((byte)m_ItemImage);

            message_data_stream.Write(m_Unknown2);

            message_data_stream.Write(m_InitialSound);
            message_data_stream.Write(m_InitialCamera);
            message_data_stream.Write(m_InitialAnimation);

            message_data_stream.Write((byte)0);
            message_data_stream.Write(m_LineCount);
            message_data_stream.Write((byte)0);

            if (m_MessageID != 0)
            {
                List<byte> raw_text_data = new List<byte>(enc.GetBytes(m_Text));
                List<byte> processed_text_data = new List<byte>();

                for (int i = 0; i < raw_text_data.Count; i++)
                {
                    // Control tag start. Process the control tag.
                    if (raw_text_data[i] == (byte)'[')
                    {
                        List<char> tag_chars = new List<char>();

                        for (int j = i + 1; j < raw_text_data.Count; j++)
                        {
                            if (raw_text_data[j] == (byte)']')
                            {
                                break;
                            }
                            else if (j == raw_text_data.Count - 1)
                            {
                                // If we've gotten to this point, there is no closing brakcet ']'.
                                // We'll clear the tag_chars list and proceed as if the tag was empty, ie '[]'.
                                tag_chars.Clear();
                                break;
                            }

                            tag_chars.Add((char)raw_text_data[j]);
                        }

                        if (tag_chars.Count > 0)
                        {
                            processed_text_data.AddRange(ProcessControlTag(new string(tag_chars.ToArray())));

                            // tag_chars contains everything between '[' and ']'. We add 1 to the length to account for '[',
                            // and the 'i++' of the loop will account for ']'.
                            i += tag_chars.Count + 1;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    // Control tag end. We shouldn't encounter ']' outside of the parsing routine above,
                    // so we will just skip it and not include it in the processed data.
                    else if (raw_text_data[i] == (byte)']')
                    {
                        continue;
                    }
                    else if (raw_text_data[i] == '\n')
                    {
                        processed_text_data.Add(0xA);
                    }
                    else if (raw_text_data[i] == '\r')
                    {
                        continue;
                    }
                    else
                    {
                        processed_text_data.Add(raw_text_data[i]);
                    }
                }

                processed_text_data.Add(0);
                text_data_stream.Write(processed_text_data.ToArray());
            }
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

        private byte[] ProcessControlTag(string tag)
        {
            if (tag.Contains('='))
            {
                string[] split_tag = tag.Split('=');

                if (split_tag.Length < 2)
                {
                    return new byte[0];
                }

                ushort tag_arg = Convert.ToUInt16(split_tag[1]);
                byte[] tag_arg_bytes = BitConverter.GetBytes(tag_arg);

                if (Enum.TryParse(split_tag[0], out SevenByteCode seven_result))
                {
                    byte first_byte = 0x00;
                    if ((byte)seven_result == 1)
                        first_byte = 0xFF;
                    return new byte[] { 0x1A, 0x07, first_byte, 0x00, (byte)seven_result, tag_arg_bytes[1], tag_arg_bytes[0] };
                }
                else if (split_tag[0] == "sound")
                {
                    return new byte[] { 0x1A, 0x05, 0x01, tag_arg_bytes[1], tag_arg_bytes[0] };
                }
                else if (split_tag[0] == "camera")
                {
                    return new byte[] { 0x1A, 0x05, 0x02, tag_arg_bytes[1], tag_arg_bytes[0] };
                }
                else if (split_tag[0] == "animation")
                {
                    return new byte[] { 0x1A, 0x05, 0x03, tag_arg_bytes[1], tag_arg_bytes[0] };
                }
                else
                {
                    return new byte[0];
                }
            }
            else if (Enum.TryParse(tag, out FiveByteCode five_result))
            {
                return new byte[] { 0x1A, 0x05, 0x00, 0x00, (byte)five_result };
            }
            else if (Enum.TryParse(tag, out TextColor color_result))
            {
                return new byte[] { 0x1A, 0x06, 0xFF, 0x00, 0x00, (byte)color_result };
            }
            else
            {
                return new byte[0];
            }
        }
    }
}
