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
        public bool IsFileSaver { get; set; }
        public string FileExtension { get; set; }

        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
            "FileName", typeof(string), typeof(FileReferenceControl), new PropertyMetadata(""));

        public string FieldName { get; set; }

        public FileReferenceControl()
        {
            InitializeComponent();
        }

        private void FileSelectorButton_Click(object sender, RoutedEventArgs e)
        {
            // Set up the file/directory picker
            CommonFileDialog fd;
            if (IsFileSaver)
            {
                fd = new CommonSaveFileDialog();
                if (FileExtension != null)
                {
                    CommonSaveFileDialog sfd = fd as CommonSaveFileDialog;
                    sfd.DefaultExtension = FileExtension;
                    sfd.AlwaysAppendDefaultExtension = true;
                    sfd.Filters.Add(new CommonFileDialogFilter(FileExtension, "." + FileExtension));
                }
            }
            else
            {
                fd = new CommonOpenFileDialog();
                CommonOpenFileDialog ofd = fd as CommonOpenFileDialog;
                ofd.IsFolderPicker = !IsFilePicker;
                ofd.AllowNonFileSystemItems = false;
                ofd.Multiselect = false;
            }
            fd.Title = IsFilePicker ? "Choose File" : "Choose Directory";
            fd.AddToMostRecentlyUsedList = false;
            fd.EnsureFileExists = true;
            fd.EnsurePathExists = true;
            fd.EnsureReadOnly = false;
            fd.EnsureValidNames = true;
            fd.ShowPlacesList = true;

            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (fd.ShowDialog(window) == CommonFileDialogResult.Ok)
            {
                FileReference reference = new FileReference
                {
                    FilePath = fd.FileName
                };

                WSettingsManager.GetSettings().SetProperty(FieldName, reference);
                FileName = fd.FileName;
            }
        }
    }
}
