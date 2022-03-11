using OpenTK;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Windows;
using System.IO;
using WindEditor.ViewModel.CustomEvents;

namespace WindEditor.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public WWindEditor WindEditor { get { return m_editor; } }

        //Close Command
        public ICommand ExitApplicationCommand { get { return new RelayCommand(x => OnRequestCloseApplication()); } }

        private WWindEditor m_editor;
        private GLControl m_glControl;
        private bool m_editorIsShuttingDown;

        private VmDataEvent vmDataEvent;
        private EventHandler<WindEditorEventArgs> vmEvenHandler;

        public MainWindowViewModel()
        {
            //Make sure the static class settings are invoked
            WSettingsManager.LoadSettings();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) => WriteUnhandledExceptionToCrashLog(e.ExceptionObject);
            App.Current.MainWindow.Closing += OnMainWindowClosed;

            // CustomEvent between vm both directions.
            vmDataEvent = VmDataEvent._VmInstance;
            vmEvenHandler = new EventHandler<WindEditorEventArgs>(OnVMEvent);
            // Event between VM.
            if (vmDataEvent != null)
                vmDataEvent.Subscribe(vmEvenHandler);
        }

        private void OnVMEvent(object source, WindEditorEventArgs args)
        {
            string name = args.ObjectToTransport as string;
            if (!String.IsNullOrEmpty(name) && name == "KeyMenu") 
            {
                //send back event with WindEditor instance
                if (vmDataEvent != null)
                    vmDataEvent.Raise(this, new WindEditorEventArgs(m_editor));
            }
        }

        private static void WriteUnhandledExceptionToCrashLog(object exceptionObject)
        {
            string crashLogPath = "./CrashLog.txt";
            Exception exception = exceptionObject as Exception;
            if (exception == null)
            {
                return;
            }
            using (StreamWriter writer = new StreamWriter(crashLogPath, append: true))
            {
                writer.WriteLine("----------------------------------------");
                writer.WriteLine("Winditor crashed on: " + DateTime.Now.ToString());
                writer.WriteLine();

                while (exception != null)
                {
                    writer.WriteLine(exception.GetType().FullName);
                    writer.WriteLine(exception.Message);
                    writer.WriteLine(exception.StackTrace);
                    writer.WriteLine();

                    exception = exception.InnerException;
                }
            }
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
            if (cliArguments.Length > 1)
            {
                if (System.IO.Directory.Exists(cliArguments[1]))
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

        private void OnRequestCloseApplication()
        {
            App.Current.MainWindow.Close();
        }

        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
