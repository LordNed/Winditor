using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WindEditor.ViewModel;
using WindEditor.Minitors.EnemyDrop;

namespace WindEditor.Minitors
{
    public class EnemyDropMinitor : IMinitor, INotifyPropertyChanged
    {
        #region IMinitor Interface
        public MenuItem GetMenuItem()
        {
            return new MenuItem()
            {
                Header = "Enemy Drop Editor",
                ToolTip = "Editor for the items that enemies can drop upon defeat.",
                //Command = OpenMinitorCommand,
            };
        }

        public void Tick(float DeltaTime)
        {

        }

        public void InitModule(WDetailsViewViewModel details_view_model)
        {

        }

        public bool RequestCloseModule()
        {
            if (!m_IsDataDirty)
                return true;

            MessageBoxResult result = MessageBox.Show("You have unsaved changes to the enemy item drop data. Save them?", "Unsaved Enemy Drop Changes", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    //OnRequestSaveMessageData();
                    return true;
                case MessageBoxResult.No:
                    return true;
                case MessageBoxResult.Cancel:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ICommand OpenMinitorCommand
        {
            get { return new RelayCommand(x => OnRequestOpenEnemyDropEditor(), x => !string.IsNullOrEmpty(WSettingsManager.GetSettings().RootDirectoryPath)); }
        }

        public string WindowTitle
        {
            get { return m_IsDataDirty ? m_WindowTitle + "*" : m_WindowTitle; }
            set
            {
                if (value != m_WindowTitle)
                {
                    m_WindowTitle = value;
                    OnPropertyChanged("WindowTitle");
                }
            }
        }

        private EnemyDropMinitorWindow m_MinitorWindow;

        private string m_WindowTitle;
        private bool m_IsDataDirty;

        private void OnRequestOpenEnemyDropEditor()
        {
            if (m_MinitorWindow != null)
            {
                m_MinitorWindow.Show();
                m_MinitorWindow.Focus();
                return;
            }

            WindowTitle = "Enemy Item Drop Editor - " + Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "ActorDat", "ActorDat.bin");

            m_MinitorWindow = new EnemyDropMinitorWindow();
            m_MinitorWindow.DataContext = this;
            m_MinitorWindow.Closing += M_MinitorWindow_Closing;

            m_MinitorWindow.Show();
        }
        private void M_MinitorWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            m_MinitorWindow.Hide();
        }

    }
}
