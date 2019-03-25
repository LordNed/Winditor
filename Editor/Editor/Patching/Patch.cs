using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameFormatReader.Common;
using SharpYaml.Serialization;

namespace WindEditor
{
    public class Patch
    {
        public List<string> PatchedFileNames { get; set; }

        private Dictionary<long, byte[]> m_PatchContents;

        public Patch(string file_name)
        {
            PatchedFileNames = new List<string>();
            m_PatchContents = new Dictionary<long, byte[]>();

            var test = new Serializer();
            var what = test.Deserialize<Dictionary<string, Dictionary<long, byte[]>>>(new StringReader(File.ReadAllText(file_name)));

            foreach (var entry in what)
            {
                PatchedFileNames.Add(entry.Key);
                
                foreach (var address in entry.Value)
                {
                    m_PatchContents.Add(address.Key, address.Value);
                }
            }
        }

        public void ApplyToFile(string file_name)
        {
            using (FileStream str = new FileStream(file_name, FileMode.Open, FileAccess.Write))
            {
                EndianBinaryWriter writer = new EndianBinaryWriter(str, Endian.Big);

                foreach (long offset in m_PatchContents.Keys)
                {
                    writer.BaseStream.Seek((offset - 0x800056E0) + 0x2620, SeekOrigin.Begin);
                    writer.Write(m_PatchContents[offset].ToArray());
                }
            }
        }
    }
}
