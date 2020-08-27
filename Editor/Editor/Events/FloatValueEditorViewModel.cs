using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Events
{
    public class FloatValueEditorViewModel : ValueEditorViewModel<ObservableCollection<PrimitiveBinding<float>>>
    {
        static FloatValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new FloatValueEditorView(), typeof(IViewFor<FloatValueEditorViewModel>));
        }

        public FloatValueEditorViewModel()
        {
            Value = new ObservableCollection<PrimitiveBinding<float>>();
        }
    }
}
