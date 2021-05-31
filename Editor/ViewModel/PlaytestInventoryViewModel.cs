using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using WindEditor.View;
using System.Windows.Controls;

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

        private bool m_DisableUpdates;
        private PlaytestInventoryWindow m_ParentWindow;
        private byte m_SaveFileIndex;
        private List<byte> m_TempIDs;

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

        public List<CheckBox> TempCheckedBoxes { get; private set; }
        public List<CheckBox> TempUncheckedBoxes { get; private set; }
        public List<SelectionChangedEventArgs> TempComboBoxChanges { get; private set; }

        public ICommand CheckboxCheckedCommand { get { return new RelayCommand(x => OnCheckboxChecked(0)); } }
        public ICommand OpenRootDirectoryCommand { get { return new RelayCommand(x => OnCheckboxUnchecked(0)); } }
        public ICommand SaveChangesCommand { get { return new RelayCommand(x => OnSaveChanges()); } }
        public ICommand CancelChangesCommand { get { return new RelayCommand(x => OnCancelChanges()); } }

        public PlaytestInventoryViewModel()
        {
            m_TempIDs = new List<byte>();
            ItemIDs = new List<byte>();

            TempCheckedBoxes = new List<CheckBox>();
            TempUncheckedBoxes = new List<CheckBox>();
            TempComboBoxChanges = new List<SelectionChangedEventArgs>();
        }

        public PlaytestInventoryViewModel(PlaytestInventoryWindow Parent)
        {
            m_ParentWindow = Parent;

            m_TempIDs = new List<byte>();
            ItemIDs = new List<byte>();

            TempCheckedBoxes = new List<CheckBox>();
            TempUncheckedBoxes = new List<CheckBox>();
            TempComboBoxChanges = new List<SelectionChangedEventArgs>();
        }

        public void OnCheckboxChecked(byte ItemID)
        {
            if (!m_TempIDs.Contains(ItemID))
                m_TempIDs.Add(ItemID);
        }

        public void OnCheckboxUnchecked(byte ItemID)
        {
            m_TempIDs.Remove(ItemID);
        }

        public void OnComboBoxChanged(byte OldValue, byte NewValue)
        {
            m_TempIDs.Remove(OldValue);

            if (NewValue != 255 && !m_TempIDs.Contains(NewValue))
                m_TempIDs.Add(NewValue);
        }

        public void OnSaveChanges()
        {
            ItemIDs = m_TempIDs;
            m_ParentWindow.Hide();

            TempCheckedBoxes.Clear();
            TempUncheckedBoxes.Clear();
            TempComboBoxChanges.Clear();
        }

        public void OnCancelChanges()
        {
            m_ParentWindow.Hide();

            foreach (CheckBox C in TempCheckedBoxes)
                C.IsChecked = false;
            foreach (CheckBox C in TempUncheckedBoxes)
                C.IsChecked = true;
            foreach (SelectionChangedEventArgs S in TempComboBoxChanges)
            {
                ComboBox Cbox = S.Source as ComboBox;
                Cbox.SelectedItem = S.RemovedItems[0];
            }

            TempCheckedBoxes.Clear();
            TempUncheckedBoxes.Clear();
            TempComboBoxChanges.Clear();

            ItemIDs = m_TempIDs;
        }
    }
}
