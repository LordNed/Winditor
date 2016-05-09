using OpenTK;
using System.ComponentModel;

namespace WindEditor.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WindEditor WindEditor { get { return m_editor; } }

        private WindEditor m_editor;
        private GLControl m_glControl;


        public MainWindowViewModel()
        {
        }

        internal void OnMainEditorWindowLoaded(GLControl glControl)
        {
            m_glControl = glControl;

            // Delay the creation of the editor until the UI is created, so that we can fire off GL commands immediately in the editor.
            m_editor = new WindEditor();
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("WindEditor"));

            // Set up the Editor Tick Loop
            System.Windows.Forms.Timer editorTickTimer = new System.Windows.Forms.Timer();
            editorTickTimer.Interval = 16; //ms
            editorTickTimer.Tick += (o, args) =>
            {
                DoApplicationTick();
            };
            editorTickTimer.Enabled = true;
        }

        private void DoApplicationTick()
        {
            // Poll the mouse at a high resolution
            System.Drawing.Point mousePos = m_glControl.PointToClient(System.Windows.Forms.Cursor.Position);

            mousePos.X = WMath.Clamp(mousePos.X, 0, m_glControl.Width);
            mousePos.Y = WMath.Clamp(mousePos.Y, 0, m_glControl.Height);
            WInput.SetMousePosition(new Vector2(mousePos.X, mousePos.Y));

            m_editor.ProcessTick();
            WInput.Internal_UpdateInputState();

            m_glControl.SwapBuffers();
        }
    }
}
