using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using NodeNetwork.ViewModels;
using NodeNetwork.Toolkit;
using System.Windows;
using NodeNetwork.Toolkit.ValueNode;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WindEditor.Events
{
    public class IntWrapper : INotifyPropertyChanged
    {
        private int m_Value;

        public int IntValue
        {
            get { return m_Value; }
            set
            {
                if (value != m_Value)
                {
                    m_Value = value;
                    OnPropertyChanged("IntValue");
                }
            }
        }

        public IntWrapper(int val)
        {
            IntValue = val;
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

    public class IntSubstance : BaseSubstance
    {
        private ObservableCollection<IntWrapper> m_Data;

        public ObservableCollection<IntWrapper> IntegerData
        {
            get { return m_Data; }
            set
            {
                if (value != m_Data)
                {
                    m_Data = value;
                    OnPropertyChanged("IntegerData");
                }
            }
        }

        public IntSubstance(EndianBinaryReader reader) : base(reader)
        {
            m_Data = new ObservableCollection<IntWrapper>();
        }

        public override void ReadValue(SubstanceData data)
        {
            m_Data = new ObservableCollection<IntWrapper>(data.GetIntData(m_ElementIndex, m_ElementCount));
        }

        public override void WriteValue(SubstanceData data)
        {
            m_ElementIndex = data.AddIntData(IntegerData.ToArray());
            m_ElementCount = IntegerData.Count;
        }

        public override void AddSubstanceEditor(NodeViewModel view_model)
        {
            ValueNodeOutputViewModel<ObservableCollection<IntWrapper>> int_output = new ValueNodeOutputViewModel<ObservableCollection<IntWrapper>>();
            int_output.Editor = new IntegerValueEditorViewModel() { Value = m_Data };

            view_model.Outputs.Edit(x => x.Add(int_output));

            int_output.Editor.PropertyChanged += Editor_PropertyChanged;
        }

        private void Editor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IntegerValueEditorViewModel v = sender as IntegerValueEditorViewModel;
            m_Data = v.Value;
        }
    }
}
