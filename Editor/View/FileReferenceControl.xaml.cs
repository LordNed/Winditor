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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for FileReferenceControl.xaml
    /// </summary>

    public partial class FileReferenceControl : UserControl
    {
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public bool IsFilePicker { get; set; }

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
            "FileName", typeof(string), typeof(FileReferenceControl), new PropertyMetadata(""));

        public string FieldName { get; set; }

        public FileReferenceControl()
        {
            InitializeComponent();
        }

        private void FileSelectorButton_Click(object sender, RoutedEventArgs e)
        {
            // Set up the directory picker
            var ofd = new CommonOpenFileDialog();
            ofd.Title = "Choose Directory";
            ofd.IsFolderPicker = !IsFilePicker;
            ofd.AddToMostRecentlyUsedList = false;
            ofd.AllowNonFileSystemItems = false;
            ofd.EnsureFileExists = true;
            ofd.EnsurePathExists = true;
            ofd.EnsureReadOnly = false;
            ofd.EnsureValidNames = true;
            ofd.Multiselect = false;
            ofd.ShowPlacesList = true;

            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (ofd.ShowDialog(window) == CommonFileDialogResult.Ok)
            {
                FileReference reference = new FileReference
                {
                    FilePath = ofd.FileName
                };

                WSettingsManager.GetSettings().SetProperty(FieldName, reference);
                FileName = ofd.FileName;
            }
        }
    }
}
