using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GameFormatReader.Common;

namespace WindEditor
{
    public struct FilePatch
    {
        public string FileName;
        public List<Patchlet> Patchlets { get; set; }
    }

    public struct Patchlet
    {
        public long Offset;
        public List<byte> Data { get; set; }

        public Patchlet(long offset, List<byte> data)
        {
            Offset = offset;
            Data = data;
        }
    }

    public class Patch
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FilePatch> Files { get; set; }

        public void Apply(string working_dir)
        {
            foreach (FilePatch p in Files)
            {
                string file_name = Path.Combine(working_dir, p.FileName);
                if (!File.Exists(file_name))
                {
                    Console.WriteLine("Could not apply patch \"{}\" to file \"{}\"! The file does not exist!");
                    continue;
                }

                using (FileStream strm = new FileStream(file_name, FileMode.Open, FileAccess.ReadWrite))
                {
                    EndianBinaryWriter writer = new EndianBinaryWriter(strm, Endian.Big);
                    EndianBinaryReader reader = new EndianBinaryReader(strm, Endian.Big);

                    foreach (Patchlet plet in p.Patchlets)
                    {
                        long offset = plet.Offset;
                        if (file_name.EndsWith(".dol"))
                        {
                            offset = DOL.AddressToOffset(plet.Offset, reader);
                        }
                        writer.BaseStream.Seek(offset, SeekOrigin.Begin);
                        writer.Write(plet.Data.ToArray());
                    }

                    strm.Flush();
                }
            }
        }
    }
}
