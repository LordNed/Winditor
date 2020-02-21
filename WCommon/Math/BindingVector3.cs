using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.ComponentModel;

namespace WindEditor
{
    public class BindingVector3 : INotifyPropertyChanged
    {
        private Vector3 m_BackingVector;

        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        public float X
        {
            get { return m_BackingVector.X; }
            set
            {
                if (value != m_BackingVector.X)
                {
                    m_BackingVector.X = value;
                    OnPropertyChanged("X");
                }
            }
        }

        public float Y
        {
            get { return m_BackingVector.Y; }
            set
            {
                if (value != m_BackingVector.Y)
                {
                    m_BackingVector.Y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        public float Z
        {
            get { return m_BackingVector.Z; }
            set
            {
                if (value != m_BackingVector.Z)
                {
                    m_BackingVector.Z = value;
                    OnPropertyChanged("Z");
                }
            }
        }

        public Vector3 BackingVector
        {
            get { return m_BackingVector; }
            set
            {
                if (value.X != m_BackingVector.X)
                {
                    m_BackingVector.X = value.X;
                    OnPropertyChanged("X");
                }
                if (value.Y != m_BackingVector.Y)
                {
                    m_BackingVector.Y = value.Y;
                    OnPropertyChanged("Y");
                }
                if (value.Z != m_BackingVector.Z)
                {
                    m_BackingVector.Z = value.Z;
                    OnPropertyChanged("Z");
                }
            }
        }

        public BindingVector3()
        {
            m_BackingVector = new Vector3();
        }

        public BindingVector3(Vector3 v)
        {
            m_BackingVector = v;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
