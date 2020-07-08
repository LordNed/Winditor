using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;

namespace WindEditor.Events
{
    public class VectorValueEditorViewModel : ValueEditorViewModel<ObservableCollection<BindingVector3>>
    {
        static VectorValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new VectorValueEditorView(), typeof(IViewFor<VectorValueEditorViewModel>));
        }

        public VectorValueEditorViewModel()
        {
            Value = new ObservableCollection<BindingVector3>();
        }
    }
}
