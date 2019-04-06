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

namespace WindEditor.Editors.Text
{
    /// <summary>
    /// Interaction logic for TextEditorWindow.xaml
    /// </summary>
    public partial class TextEditorWindow : Window
    {
        public TextEditorWindow()
        {
            InitializeComponent();

            string[] five_codes = Enum.GetNames(typeof(FiveByteCode));
            string[] colors = Enum.GetNames(typeof(TextColor));
            string[] seven_codes = Enum.GetNames(typeof(SevenByteCode));

            foreach (string s in five_codes)
            {
                TagListView.Items.Add(s);
            }

            foreach (string s in colors)
            {
                TagListView.Items.Add(s);
            }

            foreach (string s in seven_codes)
            {
                TagListView.Items.Add(s + "=0");
            }
        }

        private void TagListView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    ListBox listbox = sender as ListBox;
                    if (listbox == null)
                        return;

                    ProcessTagSelection(listbox);
                    break;
                case Key.Escape:
                    TagSuggestionPopup.IsOpen = false;
                    break;
            }
        }

        private void TagListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            ListBox listbox = sender as ListBox;
            if (listbox == null)
                return;

            ProcessTagSelection(listbox);
        }

        private void ProcessTagSelection(ListBox list_box)
        {
            TagSuggestionPopup.IsOpen = false;

            string tag_name = list_box.SelectedItem.ToString() + "]";
            int i = MainTextBox.CaretIndex;

            MainTextBox.Text = MainTextBox.Text.Insert(i, tag_name);
            MainTextBox.CaretIndex = i + tag_name.Length;

            MainTextBox.Focus();
        }

        private void MainTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if ((textbox == null) || (textbox.CaretIndex == 0))
                return;

            if (e.Key != Key.OemOpenBrackets || e.DeadCharProcessedKey == Key.LeftShift || e.DeadCharProcessedKey == Key.RightShift)
                return;

            ShowTagsPopup(textbox.GetRectFromCharacterIndex(textbox.CaretIndex, true));
        }

        private void ShowTagsPopup(Rect placementRect)
        {
            TagSuggestionPopup.PlacementTarget = MainTextBox;
            TagSuggestionPopup.PlacementRectangle = placementRect;
            TagSuggestionPopup.IsOpen = true;

            TagListView.SelectedIndex = 0;
            TagListView.Focus();
        }
    }
}
