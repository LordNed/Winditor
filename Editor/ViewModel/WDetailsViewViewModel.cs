using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace WindEditor.ViewModel
{
    public class WDetailsViewViewModel : INotifyPropertyChanged
    {
        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, WDetailsCategoryRowViewModel> Categories
        {
            get { return m_Categories; }
            set
            {
                if (value != m_Categories)
                {
                    m_Categories = value;
                    OnPropertyChanged("Categories");
                }
            }
        }

        public int GridColumnWidth_0
        {
            get { return m_GridColumnWidth_0; }
            set
            {
                if (value != m_GridColumnWidth_0)
                {
                    m_GridColumnWidth_0 = value;
                    OnPropertyChanged("GridColumnWidth_0");
                }
            }
        }

        public int GridColumnWidth_2
        {
            get { return m_GridColumnWidth_2; }
            set
            {
                if (value != m_GridColumnWidth_2)
                {
                    m_GridColumnWidth_2 = value;
                    OnPropertyChanged("GridColumnWidth_2");
                }
            }
        }

        private Dictionary<string, WDetailsCategoryRowViewModel> m_Categories;
        private int m_GridColumnWidth_0;
        private int m_GridColumnWidth_2;

        public WDetailsViewViewModel()
        {
            m_Categories = new Dictionary<string, WDetailsCategoryRowViewModel>();
            m_GridColumnWidth_0 = 50;
            m_GridColumnWidth_0 = 50;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class WProperty : System.Attribute
    {
        private string Category;
        private string DisplayName;

        public WProperty(string category, string name)
        {
            Category = category;
            DisplayName = name;
        }
    }
}
