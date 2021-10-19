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

        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                if (value != IsExpanded)
                {
                    m_IsExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        public bool IsVisible
        {
            get { return m_IsVisible; }
            set
            {
                if (value != IsVisible)
                {
                    m_IsVisible = value;
                    OnPropertyChanged("IsVisible");
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
        private bool m_IsExpanded;
        private bool m_IsVisible;
        private BindingList<WDetailSingleRowViewModel> m_PropertyRows;

        public WDetailsCategoryRowViewModel()
        {
            m_PropertyRows = new BindingList<WDetailSingleRowViewModel>();
        }

        public WDetailsCategoryRowViewModel(string name)
        {
            CategoryName = name.Replace("_", "__"); // Escape underscores so they're not interpreted access keys.
            IsExpanded = true;
            IsVisible = true;

            m_PropertyRows = new BindingList<WDetailSingleRowViewModel>();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
