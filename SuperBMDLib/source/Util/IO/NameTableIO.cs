using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Util
{
    public static class NameTableIO
    {
        public static List<string> Load(EndianBinaryReader reader, int offset)
        {
            List<string> names = new List<string>();

            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);

            short stringCount = reader.ReadInt16();
            reader.SkipInt16();

            for (int i = 0; i < stringCount; i++)
            {
                reader.SkipInt16();
                short nameOffset = reader.ReadInt16();
                long saveReaderPos = reader.BaseStream.Position;
                reader.BaseStream.Position = offset + nameOffset;

                names.Add(reader.ReadStringUntil('\0'));

                reader.BaseStream.Position = saveReaderPos;
            }

            return names;
        }

        public static void Write(EndianBinaryWriter writer, List<string> names)
        {
            long start = writer.BaseStream.Position;

            writer.Write((short)names.Count);
            writer.Write((short)-1);

            foreach (string st in names)
            {
                writer.Write(HashString(st));
                writer.Write((short)0);
            }

            long curOffset = writer.BaseStream.Position;
            for (int i = 0; i < names.Count; i++)
            {
                writer.Seek((int)(start + (6 + i * 4)), System.IO.SeekOrigin.Begin);
                writer.Write((short)(curOffset - start));
                writer.Seek((int)curOffset, System.IO.SeekOrigin.Begin);

                writer.Write(names[i].ToCharArray());
                writer.Write((byte)0);

                curOffset = writer.BaseStream.Position;
            }
        }

        private static ushort HashString(string str)
        {
            ushort hash = 0;

            foreach (char c in str)
            {
                hash *= 3;
                hash += (ushort)c;
            }

            return hash;
        }
    }
}
