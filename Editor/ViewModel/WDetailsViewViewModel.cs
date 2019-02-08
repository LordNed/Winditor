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
        public ReflectionTestClass test;

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

        Dictionary<string, WDetailsCategoryRowViewModel> m_Categories;

        public WDetailsViewViewModel()
        {
            m_Categories = new Dictionary<string, WDetailsCategoryRowViewModel>();
            test = new ReflectionTestClass();

            PropertyInfo[] props = test.GetType().GetProperties();

            foreach (PropertyInfo p in props)
            {
                CustomAttributeData cad = p.CustomAttributes.ToList()[0];
                string category_name = (string)cad.ConstructorArguments.ToArray()[0].Value;

                if (!Categories.ContainsKey(category_name))
                {
                    Categories[category_name] = new WDetailsCategoryRowViewModel(category_name);
                }

                Categories[category_name].PropertyRows.Add(new WDetailSingleRowViewModel(p.Name));

                if (p.Name == "Name")
                {
                    Categories[category_name].PropertyRows[0].PropertyControl = new System.Windows.Controls.TextBox();
                    System.Windows.Controls.TextBox argh = (System.Windows.Controls.TextBox)Categories[category_name].PropertyRows[0].PropertyControl;

                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(p.Name);
                    binding.Source = test;
                    binding.Mode = System.Windows.Data.BindingMode.TwoWay;
                    binding.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;

                    argh.SetBinding(System.Windows.Controls.TextBox.TextProperty, binding);
                }
            }

            test.Name = "hi ned";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ReflectionTestClass : INotifyPropertyChanged
    {
        // WPF
        public event PropertyChangedEventHandler PropertyChanged;

        [WProperty("Category A", "Name")]
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value != m_Name)
                {
                    m_Name = value;
                    OnPropertyChanged("Name");
                    Console.WriteLine(value);
                }
            }
        }

        [WProperty("Category A", "Year of Birth")]
        public int Year { get; set; }

        [WProperty("Category B", "Family")]
        public List<string> Family { get; set; }

        private string m_Name;
        private int m_Year;
        private List<string> m_Family;

        public ReflectionTestClass()
        {
            Name = "Earl";
            Year = 2019;

            Family = new List<string>();

            Family.Add("Albert");
            Family.Add("Greta");
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
