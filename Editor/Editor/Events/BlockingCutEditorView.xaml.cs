using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
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
    /// Interaction logic for BlockingNodeEditorView.xaml
    /// </summary>
    public partial class BlockingCutEditorView : UserControl, IViewFor<BlockingCutEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(BlockingCutEditorViewModel), typeof(BlockingCutEditorView), new PropertyMetadata(null));

        public BlockingCutEditorViewModel ViewModel
        {
            get => (BlockingCutEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (BlockingCutEditorViewModel)value;
        }
        #endregion

        public BlockingCutEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d => {
                DataContext = ViewModel; //this.Bind(ViewModel, vm => vm.Value.ParentActor.ParentEvent.Actors, v => v.ActorNameCombo1.ItemsSource).DisposeWith(d);
            });
        }

        private void CutNameCombo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
