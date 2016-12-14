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

        public bool ShowBoundingBox
        {
            get { return m_showBoundingBox; }
            set
            {
                m_showBoundingBox = value;
                OnPropertyChanged("ShowBoundingBox");
            }
        }

        public bool ShowBoundingSphere
        {
            get { return m_showBoundingSphere; }
            set
            {
                m_showBoundingSphere = value;
                OnPropertyChanged("ShowBoundingSphere");
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
        private bool m_showBoundingBox;
        private bool m_showBoundingSphere;
        private bool m_showBones;

        public ModelRenderOptionsViewModel()
        {
            ShowPivot = Properties.Settings.Default.ShowPivot;
            ShowGrid = Properties.Settings.Default.ShowGrid;
            ShowBoundingBox = Properties.Settings.Default.ShowBoundingBox;
            ShowBoundingSphere = Properties.Settings.Default.ShowBoundingSphere;
            ShowBones = Properties.Settings.Default.ShowBones;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.ShowPivot = ShowPivot;
            Properties.Settings.Default.ShowGrid = ShowGrid;
            Properties.Settings.Default.ShowBoundingBox = ShowBoundingBox;
            Properties.Settings.Default.ShowBoundingSphere = ShowBoundingSphere;
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
