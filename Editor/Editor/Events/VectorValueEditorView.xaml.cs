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
    /// Interaction logic for VectorValueEditorView.xaml
    /// </summary>
    public partial class VectorValueEditorView : UserControl, IViewFor<VectorValueEditorViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(VectorValueEditorViewModel), typeof(VectorValueEditorView), new PropertyMetadata(null));

        public VectorValueEditorViewModel ViewModel
        {
            get => (VectorValueEditorViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (VectorValueEditorViewModel)value;
        }
        #endregion

        public VectorValueEditorView()
        {
            InitializeComponent();

            this.WhenActivated(d => {
                DataContext = ViewModel; //this.Bind(ViewModel, vm => vm.Value.ParentActor.ParentEvent.Actors, v => v.ActorNameCombo1.ItemsSource).DisposeWith(d);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Value.Add(new BindingVector3(new OpenTK.Vector3()));
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
}
