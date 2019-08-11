using OpenTK;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Windows;

namespace WindEditor.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WWindEditor WindEditor { get { return m_editor; } }
        public ICommand SetDataRootCommand { get { return new RelayCommand(x => OnUserSetDataRoot()); } }

        private WWindEditor m_editor;
        private GLControl m_glControl;
        private bool m_editorIsShuttingDown;


        public MainWindowViewModel()
        {
            App.Current.MainWindow.Closing += OnMainWindowClosed;
        }

        internal void OnMainEditorWindowLoaded(GLControl glControl)
        {
            m_glControl = glControl;

            // Delay the creation of the editor until the UI is created, so that we can fire off GL commands immediately in the editor.
            m_editor = new WWindEditor();
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("WindEditor"));

            // Set up the Editor Tick Loop
            System.Windows.Forms.Timer editorTickTimer = new System.Windows.Forms.Timer();
            editorTickTimer.Interval = 16; //ms
            editorTickTimer.Tick += (o, args) =>
            {
                DoApplicationTick();
            };
            editorTickTimer.Enabled = true;

            // Check the command line arguments to see if they've passed a folder, if so we'll try to open that.
            // This allows debugging through Visual Studio to open a map automatically.
            var cliArguments = Environment.GetCommandLineArgs();
            if(cliArguments.Length > 1)
            {
                if(System.IO.Directory.Exists(cliArguments[1]))
                {
                    m_editor.LoadProject(cliArguments[1], cliArguments[1]);
                }
            }
        }

        private void DoApplicationTick()
        {
            if (m_editorIsShuttingDown)
                return;

            // Poll the mouse at a high resolution
            System.Drawing.Point mousePos = m_glControl.PointToClient(System.Windows.Forms.Cursor.Position);

            mousePos.X = WMath.Clamp(mousePos.X, 0, m_glControl.Width);
            mousePos.Y = WMath.Clamp(mousePos.Y, 0, m_glControl.Height);
            WInput.SetMousePosition(new Vector2(mousePos.X, mousePos.Y));

            m_editor.ProcessTick();
            WInput.Internal_UpdateInputState();

            m_glControl.SwapBuffers();
        }

        private void OnUserSetDataRoot()
        {
            // Violate dat MVVM.
            WindEditor.View.OptionsMenu optionsMenu = new View.OptionsMenu();
            optionsMenu.ShowDialog();
        }

        private void OnMainWindowClosed(object sender, CancelEventArgs e)
        {
            /*if(someChangesExist)
                if(UserWantsToSave)
                    e.Cancel = true;*/

            /*MessageBoxResult res = MessageBox.Show("test", "App", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
            }*/

            if (!WindEditor.TryCloseMinitors())
            {
                e.Cancel = true;
                return;
            }

            m_editorIsShuttingDown = true;
            m_editor.Shutdown();

            Application.Current.Shutdown();
        }
    }
}
