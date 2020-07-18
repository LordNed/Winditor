using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNetwork.ViewModels;
using ReactiveUI;
using DynamicData;
using System.Collections.ObjectModel;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;

namespace WindEditor.Events
{
    public class SubstanceNodeViewModel : NodeViewModel
    {
        private Substance m_Substance;

        public Substance Substance { get { return m_Substance; } set { m_Substance = value; } }

        static SubstanceNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<SubstanceNodeViewModel>));
        }

        public SubstanceNodeViewModel(Substance sub)
        {
            Substance = sub;

            Name = Substance.Name;

            switch (Substance)
            {
                case Substance<ObservableCollection<PrimitiveBinding<float>>> float_sub:
                    ValueNodeOutputViewModel<ObservableCollection<PrimitiveBinding<float>>> float_output = new ValueNodeOutputViewModel<ObservableCollection<PrimitiveBinding<float>>>();
                    float_output.Editor = new FloatValueEditorViewModel() { Value = float_sub.Data };

                    Outputs.Edit(x => x.Add(float_output));
                    break;
                case Substance<ObservableCollection<PrimitiveBinding<int>>> int_sub:
                    ValueNodeOutputViewModel<ObservableCollection<PrimitiveBinding<int>>> int_output = new ValueNodeOutputViewModel<ObservableCollection<PrimitiveBinding<int>>>();
                    int_output.Editor = new IntegerValueEditorViewModel() { Value = int_sub.Data };

                    Outputs.Edit(x => x.Add(int_output));
                    break;
                case Substance<ObservableCollection<BindingVector3>> vec_sub:
                    ValueNodeOutputViewModel<ObservableCollection<BindingVector3>> vec_output = new ValueNodeOutputViewModel<ObservableCollection<BindingVector3>>();
                    vec_output.Editor = new VectorValueEditorViewModel() { Value = vec_sub.Data };

                    Outputs.Edit(x => x.Add(vec_output));
                    break;
                case Substance<PrimitiveBinding<string>> string_sub:
                    ValueNodeOutputViewModel<PrimitiveBinding<string>> string_output = new ValueNodeOutputViewModel<PrimitiveBinding<string>>();
                    string_output.Editor = new StringValueEditorViewModel() { Value = string_sub.Data };

                    Outputs.Edit(x => x.Add(string_output));
                    break;
                default:
                    Outputs.Edit(x => x.Add(new NodeOutputViewModel()));
                    break;
            }
        }
    }
}
