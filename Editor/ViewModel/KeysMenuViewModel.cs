using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WindEditor.Editor.Managers;
using WindEditor.ViewModel.CustomEvents;

namespace WindEditor.ViewModel
{
    public enum KeyInputProfile
    {
        NormalInputProfile,
        SimpleInputProfile,
        NotAnProfile
    }

    public partial class KeysMenuViewModel : INotifyPropertyChanged
    {
        // Setting data 
        private KeyInputProfile keyInputProfileActive = KeyInputProfile.NormalInputProfile;
        private Key accelerationKey = Key.LeftShift;

        // Event for window closing (ViewModel --> View)
        private CloseWindowEvent<EventArgs> closeWindowEvent;
        public CloseWindowEvent<EventArgs> CloseWindowEvent
        {
            get { return closeWindowEvent; }
        }

        private VmDataEvent vmDataEvent;
        private EventHandler<WindEditorEventArgs> vmEvenHandler;

        // For Input/Key profile bindings
        private WWindEditor windEditorInstance;
        private InputProfileManager inputProfileManager;

        public KeysMenuViewModel()
        {
            // ICommands
            CancelSettingsCommand = new RelayCommand(x => CloseKeysMenuWindow());
            AcceptSettingsCommand = new RelayCommand(x => OnUserAcceptSettings());

            // Closing Window/Application
            closeWindowEvent = new CloseWindowEvent<EventArgs>();
            App.Current.MainWindow.Closing += OnMainWindowClosed;

            // CustomEvent between vm (both directions)
            vmDataEvent = VmDataEvent._VmInstance;
            vmEvenHandler = new EventHandler<WindEditorEventArgs>(OnVMEvent);
            // Event between VM.
            if (vmDataEvent != null)
                vmDataEvent.Subscribe(vmEvenHandler);

            if(windEditorInstance == null)
            vmDataEvent.Raise(this, new WindEditorEventArgs("KeyMenu"));

            GetInputProfile();
        }

        public KeyInputProfile InputProfile 
        {
            get
            {
                return keyInputProfileActive;
            }
            set
            {
                if (!(KeyInputProfile.NotAnProfile == value))
                {
                    keyInputProfileActive = value;
                    OnPropertyChanged("InputProfile");
                }
            }
        }

        public string AccelerationKey
        {
            get
            {
                return accelerationKey.ToString();
            }
            set
            {
                Key key;
                bool isConverted = Enum.TryParse(value, out key);
                if (isConverted) 
                {
                    accelerationKey = key;
                    OnPropertyChanged("AccelerationKey");
                }
            }
        }

        public ICommand AcceptSettingsCommand
        {
            get; private set;
        }

        public ICommand CancelSettingsCommand
        {
            get; private set;
        }

        private void OnUserAcceptSettings()
        {
            SetInputProfile();
            inputProfileManager.SaveSettings();

            CloseKeysMenuWindow();
        }

        private void OnVMEvent(object source, WindEditorEventArgs args)
        {
            WWindEditor editor = args.ObjectToTransport as WWindEditor;
            if (editor != null)
            {
                windEditorInstance = editor;
                inputProfileManager = windEditorInstance.InputProfileManager;
            }
        }

        private void CloseKeysMenuWindow()
        {
            if(closeWindowEvent != null)
            closeWindowEvent.OnEventRaised(null, new EventArgs());
        }

        /// <summary>
        /// Garbage collection
        /// </summary>
        private void OnMainWindowClosed(object sender, CancelEventArgs e)
        {
            if (vmDataEvent != null)
                vmDataEvent.UnSubscribe(vmEvenHandler);

            windEditorInstance = null;
            inputProfileManager = null;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
