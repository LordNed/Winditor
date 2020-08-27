using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WindEditor.Events;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for CutReferenceControl.xaml
    /// </summary>
    public partial class CutReferenceControl : UserControl
    {
        public ObservableCollection<Staff> StaffCollectionReference
        {
            get { return (ObservableCollection<Staff>)GetValue(StaffCollectionReferenceProperty); }
            set { SetValue(StaffCollectionReferenceProperty, value); }
        }

        public static readonly DependencyProperty StaffCollectionReferenceProperty = DependencyProperty.Register(
            "StaffCollectionReference", typeof(ObservableCollection<Staff>), typeof(CutReferenceControl), new PropertyMetadata(null));

        public Cut CutReference
        {
            get { return (Cut)GetValue(CutReferenceProperty); }
            set { SetValue(CutReferenceProperty, value); }
        }

        public static readonly DependencyProperty CutReferenceProperty = DependencyProperty.Register(
            "CutReference", typeof(Cut), typeof(CutReferenceControl), new PropertyMetadata(null));

        public Staff StaffReference
        {
            get { return (Staff)GetValue(StaffReferenceProperty); }
            set { SetValue(StaffReferenceProperty, value); }
        }

        public static readonly DependencyProperty StaffReferenceProperty = DependencyProperty.Register(
            "StaffReference", typeof(Staff), typeof(CutReferenceControl), new PropertyMetadata(null));

        public CutReferenceControl()
        {
            InitializeComponent();
        }

        public void SetReferences()
        {
            if (CutReference != null)
            {
                StaffReference = CutReference.ParentActor;
                StaffReference.Cuts.CollectionChanged += Cuts_CollectionChanged;
            }
        }

        private void Cuts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (cut_combo.Items.Contains(CutReference))
            {
                cut_combo.SelectedItem = CutReference;
            }
        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            CutReference = null;
            StaffReference = null;
        }

        private void cut_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.RemovedItems.Count > 0)
            {
                CutReference = (Cut)e.AddedItems[0];
            }
        }

        private void actor_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.RemovedItems.Count > 0)
            {
                Staff last_staff = (Staff)e.RemovedItems[0];
                last_staff.Cuts.CollectionChanged -= Cuts_CollectionChanged;

                StaffReference.Cuts.CollectionChanged += Cuts_CollectionChanged;
            }
        }
    }
}
