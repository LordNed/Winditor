using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WindEditor.ViewModel;
using WindEditor.View;
using System.ComponentModel;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for OptionsMenu.xaml
    /// </summary>
    public partial class KeyOptionsMenu : Window
    {
        private KeysMenuViewModel m_ViewModel;
        private EventHandler<EventArgs> closeEventHandler;

        public KeyOptionsMenu()
        {
            InitializeComponent();
            m_ViewModel = new KeysMenuViewModel();
            DataContext = m_ViewModel;

            if (closeEventHandler == null) 
            {
                closeEventHandler = new EventHandler<EventArgs>(CloseWindow);
            }

            m_ViewModel.CloseWindowEvent.SubscribeCloseEvent(closeEventHandler);
        }

        private void CloseWindow(object obj, EventArgs e)
        {
            m_ViewModel.CloseWindowEvent.UnSubscribeCloseEvent(closeEventHandler);
            this.Close();
        }

        private bool accelerationClicked = false;

        private void Acceleration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (accelerationClicked) 
            {
                accelerationClicked = false;
                TextBox thisTextbox = sender as TextBox;
                if (thisTextbox != null)
                {
                    thisTextbox.Background = new SolidColorBrush(Color.FromRgb(222, 222, 222));
                }
            }
            else 
            {
                accelerationClicked = true;
                TextBox thisTextbox = sender as TextBox;
                if (thisTextbox != null)
                {
                    thisTextbox.Background = new SolidColorBrush(Color.FromRgb(155, 157, 239));
                }
            }
        }

        private void Acceleration_KeyDown(object sender, KeyEventArgs e)
        {
            if (accelerationClicked) 
            {
                TextBox thisTextbox = sender as TextBox;
                if(thisTextbox != null) 
                {
                    thisTextbox.Text = e.Key.ToString();
                    thisTextbox.Background = new SolidColorBrush(Color.FromRgb(222, 222, 222));
                    accelerationClicked = false;
                }
            }
        }
    }
}
