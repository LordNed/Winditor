using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace WindEditor.Minitors.Input
{
    public enum WindWakerNote
    {
        Middle,
        Up,
        Right,
        Down,
        Left,
        None = 0xFF
    }

    public class WindWakerSong : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public const int MAX_NOTES = 6;

        public WindWakerNote[] Notes
        {
            get { return m_Notes; }
            set
            {
                if (value != m_Notes)
                {
                    m_Notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        public int NoteCount
        {
            get { return m_NoteCount; }
            set
            {
                if (value != m_NoteCount)
                {
                    m_NoteCount = value;
                    OnPropertyChanged("NoteCount");
                }
            }
        }

        private WindWakerNote[] m_Notes;
        private int m_NoteCount;

        public WindWakerSong(EndianBinaryReader reader)
        {
            Notes = new WindWakerNote[MAX_NOTES];
            NoteCount = reader.ReadByte();

            for (int i = 0; i < MAX_NOTES; i++)
            {
                Notes[i] = (WindWakerNote)reader.ReadByte();
            }
        }
    }
}
