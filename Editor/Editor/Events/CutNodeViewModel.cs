using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace WindEditor.Events
{
    public class CutNodeViewModel : NodeViewModel
    {
        private Cut m_Cut;

        public Cut Cut
        {
            get { return m_Cut; }
            set
            {
                if (m_Cut != value)
                {
                    m_Cut = value;
                }
            }
        }

        static CutNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new CutNodeView(), typeof(IViewFor<CutNodeViewModel>));
        }

        public CutNodeViewModel(Cut cut)
        {
            Cut = cut;
        }
    }
}
