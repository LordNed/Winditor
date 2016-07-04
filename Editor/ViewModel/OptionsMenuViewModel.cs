using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WindEditor.ViewModel
{
    class OptionsMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string RootDirectory
        {
            get { return m_rootDirectory; }
            set
            {
                m_rootDirectory = value;
                OnPropertyChanged("RootDirectory");
            }
        }

        public ICommand OpenRootDirectoryCommand { get { return new RelayCommand(x => OnUserRequestOpenRootDirectory()); } }
        public ICommand AcceptSettingsCommand { get { return new RelayCommand(x => OnUserAcceptSettings()); } }
        public ICommand CancelSettingsCommand { get { return new RelayCommand(x => OnUserCancelSettings()); } }
        
        private string m_rootDirectory;

        public OptionsMenuViewModel()
        {
            RootDirectory = Properties.Settings.Default.RootDirectory;
        }

        private void OnUserRequestOpenRootDirectory()
        {
            var ofd = new CommonOpenFileDialog();
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
            }
        }

        private void OnUserAcceptSettings()
        {
            Properties.Settings.Default.RootDirectory = RootDirectory;
            Properties.Settings.Default.Save();
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
                if (window.Title == "OptionsMenu")
                    window.Close();
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
