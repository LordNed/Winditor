using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;

namespace WindEditor.Events
{
    public class FloatWrapper : INotifyPropertyChanged
    {
        private float m_Value;

        public float FloatValue
        {
            get { return m_Value; }
            set
            {
                if (value != m_Value)
                {
                    m_Value = value;
                    OnPropertyChanged("FloatValue");
                }
            }
        }

        public FloatWrapper(float val)
        {
            FloatValue = val;
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

    public class FloatSubstance : BaseSubstance
    {
        static int test = 0;
        private ObservableCollection<FloatWrapper> m_Data;

        public ObservableCollection<FloatWrapper> FloatData
        {
            get { return m_Data; }
            set
            {
                if (value != m_Data)
                {
                    m_Data = value;
                    OnPropertyChanged("FloatData");
                }
            }
        }

        public FloatSubstance(EndianBinaryReader reader) : base(reader)
        {
            m_Data = new ObservableCollection<FloatWrapper>();
        }

        public override void ReadValue(SubstanceData data)
        {
            m_Data = new ObservableCollection<FloatWrapper>(data.GetFloatData(m_ElementIndex, m_ElementCount));
        }

        public override void WriteValue(SubstanceData data)
        {
            m_ElementIndex = data.AddFloatData(FloatData.ToArray());
            m_ElementCount = FloatData.Count;
        }

        public override void AddSubstanceEditor(NodeViewModel view_model)
        {
            ValueNodeOutputViewModel<ObservableCollection<FloatWrapper>> int_output = new ValueNodeOutputViewModel<ObservableCollection<FloatWrapper>>();
            int_output.Editor = new FloatValueEditorViewModel() { Value = m_Data };

            if (m_Data == null)
            {
                int i = 0;
            }

            view_model.Outputs.Edit(x => x.Add(int_output));

            int_output.Editor.PropertyChanged += Editor_PropertyChanged;

            test++;
            Console.WriteLine(test);
        }

        private void Editor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            FloatValueEditorViewModel v = sender as FloatValueEditorViewModel;
            m_Data = v.Value;
        }
    }
}
