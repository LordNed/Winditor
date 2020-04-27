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

            this.WhenActivated(d => d(
                this.Bind(ViewModel, vm => vm.Value, v => v.iStack.ItemsSource, 
                vmToViewConverterOverride: new ObservableIntCollectionToIEnumerableConverter(),
                viewToVMConverterOverride: new ObservableIntCollectionToIEnumerableConverter())
            ));
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
                ObservableCollection<IntWrapper> list = from as ObservableCollection<IntWrapper>;
                if (list == null)
                {
                    result = new List<IntWrapper>() { new IntWrapper(0) };
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
