using System.ComponentModel;
using System.Windows.Input;
using WindEditor;

namespace J3DRenderer
{
    public class HighresScreenshotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OnUserRequestScreenshot { get { return new RelayCommand(x => UserRequestedScreenshot = true); } }

        public bool UserRequestedScreenshot { get; set; }

        public float ResolutionMultiplier
        {
            get { return m_resolutionMultiplier; }
            set
            {
                m_resolutionMultiplier = WMath.Clamp(value, MultiplierMinimum, MultiplierMaximum);
                OnPropertyChanged("ResolutionMultiplier");
            }
        }

        public int MultiplierMinimum { get { return 1; } }
        public int MultiplierMaximum { get { return 8; } }

        private float m_resolutionMultiplier = 1f;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
