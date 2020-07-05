using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.Events
{
    public class BlockingCutEditorViewModel : ValueEditorViewModel<Cut>, INotifyPropertyChanged
    {
        private Staff m_BlockingStaff1;
        private Staff m_BlockingStaff2;
        private Staff m_BlockingStaff3;

        public Staff BlockingStaff1
        {
            get { return m_BlockingStaff1; }
            set
            {
                if (value != m_BlockingStaff1)
                {
                    m_BlockingStaff1 = value;
                    OnPropertyChanged("BlockingStaff1");
                }
            }
        }

        public Staff BlockingStaff2
        {
            get { return m_BlockingStaff2; }
            set
            {
                if (value != m_BlockingStaff2)
                {
                    m_BlockingStaff2 = value;
                    OnPropertyChanged("BlockingStaff2");
                }
            }
        }

        public Staff BlockingStaff3
        {
            get { return m_BlockingStaff3; }
            set
            {
                if (value != m_BlockingStaff3)
                {
                    m_BlockingStaff3 = value;
                    OnPropertyChanged("BlockingStaff3");
                }
            }
        }

        static BlockingCutEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new BlockingCutEditorView(), typeof(IViewFor<BlockingCutEditorViewModel>));
        }

        public BlockingCutEditorViewModel()
        {
            Value = null;
        }

        public BlockingCutEditorViewModel(Cut cut)
        {
            Value = cut;

            if (cut.BlockingCuts[0] != null)
            {
                BlockingStaff1 = cut.BlockingCuts[0].ParentActor;
            }
        }

        #region INotifyPropertyChanged Support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
