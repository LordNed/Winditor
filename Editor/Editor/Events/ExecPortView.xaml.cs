using ReactiveUI;
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
using NodeNetwork.Views;
using NodeNetwork.ViewModels;
using System.Reactive.Disposables;

namespace WindEditor.Events
{
    /// <summary>
    /// Interaction logic for ExecPortView.xaml
    /// </summary>
    public partial class ExecPortView : UserControl, IViewFor<ExecPortViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(ExecPortViewModel), typeof(ExecPortView), new PropertyMetadata(null));

        public ExecPortViewModel ViewModel
        {
            get => (ExecPortViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ExecPortViewModel)value;
        }
        #endregion

        #region Template Resource Keys
        public const string ExecutionPortTemplateKey = "ExecutionPortTemplate";
        public const string IntegerPortTemplateKey = "IntegerPortTemplate";
        public const string StringPortTemplateKey = "StringPortTemplate";
        #endregion

        public ExecPortView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.PortView.ViewModel).DisposeWith(d);

                this.OneWayBind(ViewModel, vm => vm.PortType, v => v.PortView.Template, GetTemplateFromPortType)
                    .DisposeWith(d);

                this.OneWayBind(ViewModel, vm => vm.IsMirrored, v => v.PortView.RenderTransform,
                    isMirrored => new ScaleTransform(isMirrored ? -1.0 : 1.0, 1.0))
                    .DisposeWith(d);
            });
        }

        public ControlTemplate GetTemplateFromPortType(PortType type)
        {
            switch (type)
            {
                case PortType.Execution: return (ControlTemplate)Resources[ExecutionPortTemplateKey];
                case PortType.Integer: return (ControlTemplate)Resources[IntegerPortTemplateKey];
                case PortType.String: return (ControlTemplate)Resources[StringPortTemplateKey];
                default: throw new Exception("Unsupported port type");
            }
        }
    }
}
