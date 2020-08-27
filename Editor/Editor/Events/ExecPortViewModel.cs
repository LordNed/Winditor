using NodeNetwork.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Events
{
    public enum PortType
    {
        Execution, Integer, String
    }

    public class ExecPortViewModel : PortViewModel
    {
        public ExecPortViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new ExecPortView(), typeof(IViewFor<ExecPortViewModel>));
        }

        #region PortType
        public PortType PortType
        {
            get => _portType;
            set => this.RaiseAndSetIfChanged(ref _portType, value);
        }
        private PortType _portType;
        #endregion
    }
}
