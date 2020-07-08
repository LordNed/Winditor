using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace WindEditor.Events
{
    public enum SubstanceType
    {
        Float,
        Vec3,
        Unknown,
        Int,
        String
    }

    public class PrimitiveBinding<T> : INotifyPropertyChanged
    {
        private T m_Value;

        public T Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value == null || !m_Value.Equals(value))
                {
                    m_Value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        public PrimitiveBinding(T value)
        {
            Value = value;
        }

        #region INotifyPropertyChanged Support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public abstract class Substance : INotifyPropertyChanged
    {
        protected string m_Name;

        protected int m_ElementIndex;
        protected int m_ElementCount;
        protected int m_NextSubstanceIndex;

        protected Substance m_NextSubstance;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public Substance NextSubstance
        {
            get { return m_NextSubstance; }
            set
            {
                if (m_NextSubstance != value)
                {
                    m_NextSubstance = value;
                    OnPropertyChanged("NextSubstance");
                }
            }
        }

        public SubstanceType Type { get; private set; }

        public Substance(EndianBinaryReader reader)
        {
            Name = new string(reader.ReadChars(32)).Trim('\0');

            reader.SkipInt32();

            Type = (SubstanceType)reader.ReadInt32();

            m_ElementIndex = reader.ReadInt32();
            m_ElementCount = reader.ReadInt32();
            m_NextSubstanceIndex = reader.ReadInt32();

            reader.Skip(12);
        }

        public void AssignNextSubstance(List<Substance> substances)
        {
            if (m_NextSubstanceIndex != -1)
                NextSubstance = substances[m_NextSubstanceIndex];
        }

        public abstract void Write(EndianBinaryWriter writer, ref int index);

        #region INotifyPropertyChanged Support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class Substance<T> : Substance
    {
        private T m_Data;

        public T Data
        {
            get { return m_Data; }
            set
            {
                if (m_Data == null || !m_Data.Equals(value))
                {
                    m_Data = value;
                    OnPropertyChanged("Data");
                }
            }
        }

        public Substance(EndianBinaryReader reader, Func<int, int, T> loader) : base(reader)
        {
            Data = loader.Invoke(m_ElementIndex, m_ElementCount);
        }

        public void UpdateSubstanceDataForExport(int count, Func<T, int> prep_func)
        {
            m_ElementCount = count;
            m_ElementIndex = prep_func.Invoke(Data);
        }

        public override void Write(EndianBinaryWriter writer, ref int index)
        {
            writer.WriteFixedString(Name, 32);
            writer.Write(index++);
            writer.Write((int)Type);

            writer.Write(m_ElementIndex);
            writer.Write(m_ElementCount);
            writer.Write(m_NextSubstanceIndex);

            writer.Write(new byte[12]);
        }
    }
}
