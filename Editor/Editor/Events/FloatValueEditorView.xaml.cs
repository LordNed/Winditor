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
    /// Interaction logic for FloatValueEditorView.xaml
    /// </summary>
    public partial class FloatValueEditorView : UserControl, IViewFor<FloatValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(FloatValueEditorViewModel), typeof(FloatValueEditorView), new PropertyMetadata(null));

        public FloatValueEditorViewModel ViewModel
        {
            get => (FloatValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (FloatValueEditorViewModel)value;
        }
        #endregion

        public FloatValueEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d => d(
                this.Bind(ViewModel, vm => vm.Value, v => v.iStack.ItemsSource,
                vmToViewConverterOverride: new ObservableFloatCollectionToIEnumerableConverter(),
                viewToVMConverterOverride: new ObservableFloatCollectionToIEnumerableConverter())
            ));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Value.Add(new FloatWrapper(0.0f));
            ViewModel.Value = new ObservableCollection<FloatWrapper>(ViewModel.Value.ToArray());
        }
    }

    public class ObservableFloatCollectionToIEnumerableConverter : IBindingTypeConverter
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
                ObservableCollection<FloatWrapper> list = from as ObservableCollection<FloatWrapper>;
                if (list == null)
                {
                    result = new List<FloatWrapper>() { new FloatWrapper(0.0f) };
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
