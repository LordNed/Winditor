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
using WindEditor.Editor;
using WindEditor.ViewModel;
using System.Reflection;
using System.Windows.Markup.Primitives;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for WAdvancedBindingListControl.xaml
    /// </summary>
    public abstract partial class WAdvancedBindingListControl : UserControl
    {
        public abstract List<WDetailSingleRowViewModel> GenerateBoundFields();

        public abstract void OnEntryComboSelectionChanged();
        public abstract void OnAddButtonClicked();
        public abstract void OnRemoveButtonClicked();

        private void entry_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnEntryComboSelectionChanged();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            OnAddButtonClicked();
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            OnRemoveButtonClicked();
        }
    }

    public class WAdvancedBindingListControl<T> : WAdvancedBindingListControl
    {
        private List<WDetailSingleRowViewModel> mBoundFields;

        public AdvancedBindingList<T> BoundList
        {
            get { return (AdvancedBindingList<T>)GetValue(BoundListProperty); }
            set { SetValue(BoundListProperty, value); }
        }

        public static readonly DependencyProperty BoundListProperty = DependencyProperty.Register(
            "BoundList", typeof(AdvancedBindingList<T>), typeof(WAdvancedBindingListControl<T>), new PropertyMetadata(null));

        public WAdvancedBindingListControl()
        {
            InitializeComponent();

            mBoundFields = new List<WDetailSingleRowViewModel>();
        }

        public override void OnEntryComboSelectionChanged()
        {
            UpdateBindings();
        }

        public override void OnAddButtonClicked()
        {
            BoundList.AddNew();
            entry_combo.Items.Refresh();

            entry_combo.SelectedItem = BoundList.Last();
        }

        public override void OnRemoveButtonClicked()
        {
            if (BoundList.Count > 0)
            {
                BoundList.RemoveAt(entry_combo.SelectedIndex);
                entry_combo.Items.Refresh();

                if (BoundList.Count != 0)
                    entry_combo.SelectedItem = BoundList.Last();
            }
        }

        public override List<WDetailSingleRowViewModel> GenerateBoundFields()
        {
            mBoundFields = WDetailsViewViewModel.GeneratePropertyRows(typeof(T));

            return mBoundFields;
        }

        public void UpdateBindings()
        {
            foreach (var c in mBoundFields)
            {
                Control c_as_control = c.PropertyControl as Control;

                MarkupObject b = MarkupWriter.GetMarkupObjectFor(c_as_control);
                if (b != null)
                {
                    foreach (MarkupProperty mp in b.Properties)
                    {
                        if (mp.DependencyProperty != null)
                        {
                            BindingExpression bi = c_as_control.GetBindingExpression(mp.DependencyProperty);

                            if (bi == null)
                                continue;

                            Binding new_bi = new Binding()
                            {
                                Source = entry_combo.SelectedItem,
                                Mode = bi.ParentBinding.Mode,
                                UpdateSourceTrigger = bi.ParentBinding.UpdateSourceTrigger,
                                Path = bi.ParentBinding.Path
                            };

                            c_as_control.SetBinding(mp.DependencyProperty, new_bi);
                        }
                    }
                }
            }
        }
    }
}
