using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace WindEditor.ViewModel
{
    public class PlaytestInventoryViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private byte m_SaveFileIndex;

        public List<byte> ItemIDs { get; private set; }

        public byte SaveFileIndex
        {
            get { return m_SaveFileIndex; }
            set
            {
                if (m_SaveFileIndex != value)
                {
                    m_SaveFileIndex = value;
                    OnPropertyChanged("SaveFileIndex");
                }
            }
        }

        public ICommand CheckboxCheckedCommand { get { return new RelayCommand(x => OnCheckboxChecked(0)); } }
        public ICommand OpenRootDirectoryCommand { get { return new RelayCommand(x => OnCheckboxUnchecked(0)); } }

        public PlaytestInventoryViewModel()
        {
            ItemIDs = new List<byte>();
        }

        public void OnCheckboxChecked(byte ItemID)
        {
            if (!ItemIDs.Contains(ItemID))
                ItemIDs.Add(ItemID);
        }

        public void OnCheckboxUnchecked(byte ItemID)
        {
            ItemIDs.Remove(ItemID);
        }

        public void OnComboBoxChanged(byte OldValue, byte NewValue)
        {
            ItemIDs.Remove(OldValue);

            if (NewValue != 255 && !ItemIDs.Contains(NewValue))
                ItemIDs.Add(NewValue);
        }
    }
}
