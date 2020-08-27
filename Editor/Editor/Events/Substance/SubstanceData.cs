using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using System.IO;
using OpenTK;

namespace WindEditor.Events
{
    public class SubstanceData
    {
        public List<int> IntegerBank { get; private set; }
        public List<float> FloatBank { get; private set; }
        public List<char> CharBank { get; private set; }

        public SubstanceData(EndianBinaryReader reader)
        {
            IntegerBank = new List<int>();
            FloatBank = new List<float>();
            CharBank = new List<char>();

            int float_offset = reader.ReadInt32At(32);
            int float_count = reader.ReadInt32At(36);

            int integer_offset = reader.ReadInt32At(40);
            int integer_count = reader.ReadInt32At(44);

            int char_offset = reader.ReadInt32At(48);
            int char_count = reader.ReadInt32At(52);

            reader.BaseStream.Seek(float_offset, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < float_count; i++)
            {
                FloatBank.Add(reader.ReadSingle());
            }

            reader.BaseStream.Seek(integer_offset, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < integer_count; i++)
            {
                IntegerBank.Add(reader.ReadInt32());
            }

            reader.BaseStream.Seek(char_offset, System.IO.SeekOrigin.Begin);
            CharBank.AddRange(reader.ReadChars(char_count));

            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
        }

        public ObservableCollection<PrimitiveBinding<int>> GetIntData(int starting_index, int count)
        {
            ObservableCollection<PrimitiveBinding<int>> data = new ObservableCollection<PrimitiveBinding<int>>();

            for (int i = 0; i < count; i++)
            {
                data.Add(new PrimitiveBinding<int>(IntegerBank[starting_index + i]));
            }

            return data;
        }

        public int AddIntData(ObservableCollection<PrimitiveBinding<int>> int_data)
        {
            int new_offset = IntegerBank.Count;

            for (int i = 0; i < int_data.Count; i++)
            {
                IntegerBank.Add(int_data[i].Value);
            }

            return new_offset;
        }

        public ObservableCollection<PrimitiveBinding<float>> GetFloatData(int starting_index, int count)
        {
            ObservableCollection<PrimitiveBinding<float>> data = new ObservableCollection<PrimitiveBinding<float>>();

            for (int i = 0; i < count; i++)
            {
                data.Add(new PrimitiveBinding<float>(FloatBank[starting_index + i]));
            }

            return data;
        }

        public int AddFloatData(ObservableCollection<PrimitiveBinding<float>> float_data)
        {
            int new_offset = FloatBank.Count;

            for (int i = 0; i < float_data.Count; i++)
            {
                FloatBank.Add(float_data[i].Value);
            }

            return new_offset;
        }

        public ObservableCollection<BindingVector3> GetVec3Data(int starting_index, int count)
        {
            ObservableCollection<BindingVector3> data = new ObservableCollection<BindingVector3>();

            for (int i = 0; i < count; i++)
            {
                int base_index = starting_index + (i * 3);
                data.Add(new BindingVector3(new Vector3(FloatBank[base_index], FloatBank[base_index + 1], FloatBank[base_index + 2])));
            }

            return data;
        }

        public int AddVec3Data(ObservableCollection<BindingVector3> vec3_data)
        {
            int new_offset = FloatBank.Count;

            foreach (BindingVector3 v in vec3_data)
            {
                FloatBank.Add(v.X);
                FloatBank.Add(v.Y);
                FloatBank.Add(v.Z);
            }

            return new_offset;
        }

        public PrimitiveBinding<string> GetStringData(int starting_index, int count)
        {
            char[] data = new char[count];
            CharBank.CopyTo(starting_index, data, 0, count);

            return new PrimitiveBinding<string>(new string(data).Trim('\0'));
        }

        public int AddStringData(PrimitiveBinding<string> string_data)
        {
            string dat = string_data.Value;
            int new_offset = CharBank.Count;

            // Formula: (x + (n-1)) & ~(n-1)
            int nextAligned = (dat.Length + 0x7) & ~0x7;
            int delta = nextAligned - dat.Length;

            CharBank.AddRange(Encoding.ASCII.GetChars(Encoding.ASCII.GetBytes(dat)));

            for (int i = 0; i < delta; i++)
                CharBank.Add('\0');

            return new_offset;
        }

        public void CompileData(List<Substance> substances)
        {
            IntegerBank.Clear();
            FloatBank.Clear();
            CharBank.Clear();

            foreach (Substance sub in substances)
            {
                sub.PrepareSubstance(substances);

                switch (sub)
                {
                    case Substance<ObservableCollection<PrimitiveBinding<float>>> float_sub:
                        float_sub.UpdateSubstanceDataForExport(float_sub.Data.Count, AddFloatData);
                        break;
                    case Substance<ObservableCollection<PrimitiveBinding<int>>> int_sub:
                        int_sub.UpdateSubstanceDataForExport(int_sub.Data.Count, AddIntData);
                        break;
                    case Substance<ObservableCollection<BindingVector3>> vec_sub:
                        vec_sub.UpdateSubstanceDataForExport(vec_sub.Data.Count, AddVec3Data);
                        break;
                    case Substance<PrimitiveBinding<string>> string_sub:
                        if (!string_sub.Data.Value.EndsWith("\0"))
                            string_sub.Data.Value += '\0';

                        string_sub.UpdateSubstanceDataForExport(string_sub.Data.Value.Length, AddStringData);
                        break;
                }
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            // Write float bank data offset + count
            writer.BaseStream.Seek(32, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Write(FloatBank.Count);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (float f in FloatBank)
                writer.Write(f);

            // Write int bank data offset + count
            writer.BaseStream.Seek(40, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Write(IntegerBank.Count);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (int i in IntegerBank)
                writer.Write(i);

            // Write char bank data offset + count
            writer.BaseStream.Seek(48, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Write(CharBank.Count);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (char ch in CharBank)
                writer.Write(ch);
        }
    }
}
