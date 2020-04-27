using System;
using System.Collections.Generic;
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
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace WindEditor.Events
{
    /// <summary>
    /// Interaction logic for CutNodeView.xaml
    /// </summary>
    public partial class CutNodeView : UserControl, IViewFor<CutNodeViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
                DependencyProperty.Register(nameof(ViewModel), typeof(CutNodeViewModel), typeof(CutNodeView), new PropertyMetadata(null));

        public CutNodeViewModel ViewModel
        {
            get => (CutNodeViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (CutNodeViewModel)value;
        }

        public CutNodeView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.NodeView.ViewModel).DisposeWith(d);
            });
        }
    }
}
