using System;
using System.Collections.Generic;
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
using ReactiveUI;

namespace WindEditor.Events
{
    /// <summary>
    /// Interaction logic for StringValueEditorView.xaml
    /// </summary>
    public partial class StringValueEditorView : UserControl, IViewFor<StringValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(StringValueEditorViewModel), typeof(StringValueEditorView), new PropertyMetadata(null));

        public StringValueEditorViewModel ViewModel
        {
            get => (StringValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (StringValueEditorViewModel)value;
        }
        #endregion

        public StringValueEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d => {
                DataContext = ViewModel; //this.Bind(ViewModel, vm => vm.Value.ParentActor.ParentEvent.Actors, v => v.ActorNameCombo1.ItemsSource).DisposeWith(d);
            });
        }
    }
}
