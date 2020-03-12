using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.ViewModel
{
    public class WDetailsCategoryRowViewModel : INotifyPropertyChanged
    {
        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        public string CategoryName
        {
            get { return m_CategoryName; }
            set
            {
                if (value != m_CategoryName)
                {
                    m_CategoryName = value;
                    OnPropertyChanged("CategoryName");
                }
            }
        }

        public BindingList<WDetailSingleRowViewModel> PropertyRows
        {
            get { return m_PropertyRows; }
            set
            {
                if (value != m_PropertyRows)
                {
                    m_PropertyRows = value;
                    OnPropertyChanged("PropertyRows");
                }
            }
        }

        private string m_CategoryName;
        private BindingList<WDetailSingleRowViewModel> m_PropertyRows;

        public WDetailsCategoryRowViewModel()
        {
            m_PropertyRows = new BindingList<WDetailSingleRowViewModel>();
        }

        public WDetailsCategoryRowViewModel(string name)
        {
            CategoryName = name.Replace("_", "__"); // Escape underscores so they're not interpreted access keys.

            m_PropertyRows = new BindingList<WDetailSingleRowViewModel>();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
