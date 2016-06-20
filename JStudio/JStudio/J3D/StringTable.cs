using GameFormatReader.Common;
using System.Collections.Generic;

namespace JStudio.J3D
{
    public class StringTable
    {
        public class Entry
        {
            public ushort StringHash { get; protected set; }
            public string String { get; private set; }

            public Entry(string str, ushort hash)
            {
                String = str;
                StringHash = hash;
            }

            public override string ToString()
            {
                return String;
            }
        }

        public List<Entry> Strings { get; private set; }

        public string this[int index]
        {
            get { return Strings[index].String; }
            set
            {
                // Calculate the string hash and then insert the string into the table.
                ushort hash = 0;
                foreach (char c in value)
                {
                    hash *= 3;
                    hash += (ushort)c;
                }

                Strings[index] = new Entry(value, hash);
            }
        }

        public StringTable()
        {
            Strings = new List<Entry>();
        }

        public static StringTable FromStream(EndianBinaryReader stream)
        {
            StringTable newTable = new StringTable();
            long headerStart = stream.BaseStream.Position;

            ushort stringCount = stream.ReadUInt16();
            stream.ReadUInt16(); // Padding

            for (int i = 0; i < stringCount; i++)
            {
                // Jump us to the string 'header' by calculating 0x4 for for the StringTable header and then 0x4 for each string header.
                stream.BaseStream.Position = headerStart + ((i + 1) * 0x4);
                ushort stringHash = stream.ReadUInt16();
                ushort stringOffset = stream.ReadUInt16();

                // Jump forward to the offset to read the string.
                stream.BaseStream.Position = headerStart + stringOffset;
                string value = stream.ReadStringUntil('\0');

                newTable.Strings.Add(new Entry(value, stringHash));
            }

            return newTable;
        }
    }
}
