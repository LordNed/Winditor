using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindEditor.Events
{
    /// <summary>
    /// Interaction logic for IntegerValueEditorView.xaml
    /// </summary>
    public partial class IntegerValueEditorView : UserControl, IViewFor<IntegerValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(IntegerValueEditorViewModel), typeof(IntegerValueEditorView), new PropertyMetadata(null));

        public IntegerValueEditorViewModel ViewModel
        {
            get => (IntegerValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (IntegerValueEditorViewModel)value;
        }
        #endregion

        public IntegerValueEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d => {
                DataContext = ViewModel; //this.Bind(ViewModel, vm => vm.Value.ParentActor.ParentEvent.Actors, v => v.ActorNameCombo1.ItemsSource).DisposeWith(d);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Value.Add(new PrimitiveBinding<int>(0));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Value.Count > 1)
            {
                Button t = e.Source as Button;

                ViewModel.Value.RemoveAt((int)t.Tag);
            }
        }
    }

    public class ObservableIntCollectionToIEnumerableConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return 1;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            if (toType == typeof(ObservableCollection<int>))
            {
                result = null;
            }
            else if (toType == typeof(IEnumerable))
            {
                ObservableCollection<PrimitiveBinding<int>> list = from as ObservableCollection<PrimitiveBinding<int>>;
                if (list == null)
                {
                    result = new List<int>() { 0 };
                }
                else
                {
                    result = list.ToList();
                }
            }
            else
            {
                result = null;
                return false;
            }

            return true;
        }
    }
}
