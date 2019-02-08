using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.ViewModel
{
    public class WDetailSingleRowViewModel : INotifyPropertyChanged
    {
        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        public string PropertyName
        {
            get { return m_PropertyName; }
            set
            {
                if (value != m_PropertyName)
                {
                    m_PropertyName = value;
                    OnPropertyChanged("PropertyName");
                }
            }
        }

        public object PropertyControl
        {
            get { return m_PropertyControl; }
            set
            {
                if (value != m_PropertyControl)
                {
                    m_PropertyControl = value;
                    OnPropertyChanged("PropertyControl");
                }
            }
        }

        private string m_PropertyName;
        private object m_PropertyControl;

        public WDetailSingleRowViewModel()
        {
        }

        public WDetailSingleRowViewModel(string name)
        {
            m_PropertyName = name;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
