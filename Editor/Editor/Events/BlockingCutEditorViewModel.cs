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

        private Cut m_BlockingCut1;
        private Cut m_BlockingCut2;
        private Cut m_BlockingCut3;

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

        public Cut BlockingCut1
        {
            get { return m_BlockingCut1; }
            set
            {
                if (value != m_BlockingCut1)
                {
                    m_BlockingCut1 = value;
                    OnPropertyChanged("BlockingCut1");

                    if (Value != null)
                    {
                        Value.BlockingCuts[0] = value;
                    }
                }
            }
        }

        public Cut BlockingCut2
        {
            get { return m_BlockingCut2; }
            set
            {
                if (value != m_BlockingCut2)
                {
                    m_BlockingCut2 = value;
                    OnPropertyChanged("BlockingCut2");

                    if (Value != null)
                    {
                        Value.BlockingCuts[0] = value;
                    }
                }
            }
        }

        public Cut BlockingCut3
        {
            get { return m_BlockingCut3; }
            set
            {
                if (value != m_BlockingCut3)
                {
                    m_BlockingCut3 = value;
                    OnPropertyChanged("BlockingCut3");

                    if (Value != null)
                    {
                        Value.BlockingCuts[0] = value;
                    }
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
            ValueChanged.Subscribe(d => {
                OnPropertyChanged("Value");
            });

            Value = cut;

            if (cut == null)
                return;

            if (cut.BlockingCuts[0] != null)
            {
                BlockingCut1 = cut.BlockingCuts[0];
                BlockingStaff1 = cut.BlockingCuts[0].ParentActor;
            }
            if (cut.BlockingCuts[1] != null)
            {
                BlockingCut2 = cut.BlockingCuts[1];
                BlockingStaff2 = cut.BlockingCuts[1].ParentActor;
            }
            if (cut.BlockingCuts[2] != null)
            {
                BlockingCut3 = cut.BlockingCuts[2];
                BlockingStaff3 = cut.BlockingCuts[2].ParentActor;
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
