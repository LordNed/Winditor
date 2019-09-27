using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;

namespace WindEditor.Editor.Modes
{
    public class GenerateUndoEventArgs
    {
        public GenerateUndoEventArgs(WUndoCommand com) { Command = com; }
        public WUndoCommand Command { get;}
    }

    public interface IEditorMode : IDisposable, INotifyPropertyChanged
    {
        DockPanel ModeControlsDock { get; set; }
        //Selection<T> EditorSelection { get; set; }
        WWorld World { get; }

        void OnBecomeActive();

        void Update(WSceneView view);

        event EventHandler<GenerateUndoEventArgs> GenerateUndoEvent;
        void BroadcastUndoEventGenerated(WUndoCommand command);

        void FilterSceneForRenderer(WSceneView view, WWorld world);

        void ClearSelection();
    }
}
