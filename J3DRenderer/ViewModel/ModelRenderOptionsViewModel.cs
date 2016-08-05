using System.ComponentModel;

namespace J3DRenderer
{
    public class ModelRenderOptionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowPivot
        {
            get { return m_showPivot; }
            set
            {
                m_showPivot = value;
                OnPropertyChanged("ShowPivot");
            }
        }

        public bool ShowGrid
        {
            get { return m_showGrid; }
            set
            {
                m_showGrid = value;
                OnPropertyChanged("ShowGrid");
            }
        }

        public bool ShowBounds
        {
            get { return m_showBounds; }
            set
            {
                m_showBounds = value;
                OnPropertyChanged("ShowBounds");
            }
        }

        public bool ShowBones
        {
            get { return m_showBones; }
            set
            {
                m_showBones = value;
                OnPropertyChanged("ShowBones");
            }
        }

        private bool m_showPivot;
        private bool m_showGrid;
        private bool m_showBounds;
        private bool m_showBones;

        public ModelRenderOptionsViewModel()
        {
            ShowPivot = Properties.Settings.Default.ShowPivot;
            ShowGrid = Properties.Settings.Default.ShowGrid;
            ShowBounds = Properties.Settings.Default.ShowBounds;
            ShowBones = Properties.Settings.Default.ShowBones;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.ShowPivot = ShowPivot;
            Properties.Settings.Default.ShowGrid = ShowGrid;
            Properties.Settings.Default.ShowBounds = ShowBounds;
            Properties.Settings.Default.ShowBones = ShowBones;
            Properties.Settings.Default.Save();
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
