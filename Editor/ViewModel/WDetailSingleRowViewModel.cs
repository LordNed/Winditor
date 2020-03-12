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

        public string PropertyToolTip
        {
            get { return m_PropertyToolTip; }
            set
            {
                if (value != m_PropertyToolTip)
                {
                    m_PropertyToolTip = value;
                    OnPropertyChanged("PropertyToolTip");
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
        private string m_PropertyToolTip;
        private object m_PropertyControl;

        public WDetailSingleRowViewModel()
        {
        }

        public WDetailSingleRowViewModel(string name)
        {
            m_PropertyName = name.Replace("_", "__"); // Escape underscores so they're not interpreted access keys.
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
