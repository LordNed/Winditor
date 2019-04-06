using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WindEditor.ViewModel
{
    class OptionsMenuViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public WSettingsContainer Settings
        {
            get { return m_Settings; }
            set
            {
                m_Settings = value;
                OnPropertyChanged("Settings");
            }
        }

        public ICommand OpenRootDirectoryCommand { get { return new RelayCommand(x => OnUserRequestOpenRootDirectory()); } }
        public ICommand AcceptSettingsCommand { get { return new RelayCommand(x => OnUserAcceptSettings()); } }
        public ICommand CancelSettingsCommand { get { return new RelayCommand(x => OnUserCancelSettings()); } }


        private WSettingsContainer m_Settings;

        public OptionsMenuViewModel()
        {
            Settings = WSettingsManager.GetSettings();
        }

        private void OnUserRequestOpenRootDirectory()
        {
            /*var ofd = new CommonOpenFileDialog();
            ofd.Title = "Choose Directory";
            ofd.IsFolderPicker = true;
            ofd.AddToMostRecentlyUsedList = false;
            ofd.AllowNonFileSystemItems = false;
            ofd.EnsureFileExists = true;
            ofd.EnsurePathExists = true;
            ofd.EnsureReadOnly = false;
            ofd.EnsureValidNames = true;
            ofd.Multiselect = false;
            ofd.ShowPlacesList = true;

            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                RootDirectory = ofd.FileName;
            }*/
        }

        private void OnUserAcceptSettings()
        {
            WSettingsManager.SaveSettings();

            CloseOptionsMenuWindow();
        }

        private void OnUserCancelSettings()
        {
            CloseOptionsMenuWindow();
        }

        private void CloseOptionsMenuWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                // This is an ugly hack.
                if (window.Title == "Options")
                    window.Close();
            }
        }
    }
}
