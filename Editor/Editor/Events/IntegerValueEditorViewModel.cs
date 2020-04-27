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
    public class IntegerValueEditorViewModel : ValueEditorViewModel<ObservableCollection<IntWrapper>>
    {
        static IntegerValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new IntegerValueEditorView(), typeof(IViewFor<IntegerValueEditorViewModel>));
        }

        public IntegerValueEditorViewModel()
        {
            Value = new ObservableCollection<IntWrapper>();
        }
    }
}
