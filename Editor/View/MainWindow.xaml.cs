using WindEditor.ViewModel;
using OpenTK;
using OpenTK.Graphics;
using System.Windows;
using System.Windows.Forms.Integration;
using System.IO;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Linq;
using WindEditor.Editor.Modes;
using System.Globalization;
using System.Threading;

namespace WindEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel m_viewModel;

        public MainWindow()
        {
            // Prevents issues with reading/writing floats on European systems.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("", false);

            InitializeComponent();
            WindowsFormsHost.EnableWindowsFormsInterop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorFormat cF = new ColorFormat(8);
            GraphicsMode gm = new GraphicsMode(cF, 24, 8, 1);
            glControlHost.Child = new GLControl(gm);
            glControlHost.SizeChanged += GlControlHost_SizeChanged;
            glControlHost.PreviewKeyDown += GlControlHost_PreviewKeyDown;
            glControlHost.PreviewKeyUp += GlControlHost_PreviewKeyUp;
            glControlHost.Child.MouseDown += GlControlHost_MouseDown;
            glControlHost.Child.MouseUp += GlControlHost_MouseUp;
            glControlHost.Child.MouseWheel += GlControlHost_MouseWheel;

            m_viewModel = (MainWindowViewModel)DataContext;
            m_viewModel.OnMainEditorWindowLoaded((GLControl)glControlHost.Child);

            m_viewModel.WindEditor.InitMinitorModules();
            List<Control> tools_items = new List<Control>(m_viewModel.WindEditor.GetRegisteredEditorMenus());

            tools_items.Add(new Separator());
            tools_items.Add(new MenuItem() { Header = "Options", Command = m_viewModel.SetDataRootCommand });

            ToolsMenu.ItemsSource = tools_items;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if(droppedFilePaths.Length > 0)
                {
                    // Only check the first thing dropped in, we except them to drag/drop a map in, not a set of scenes.
                    if(Directory.Exists(droppedFilePaths[0]))
                    {
                        m_viewModel.WindEditor.LoadProject(droppedFilePaths[0], droppedFilePaths[0]);
                    }
                }
            }
        }

        private void GlControlHost_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WInput.SetMouseState(WinFormToWPFMouseButton(e), false);
        }

        private void GlControlHost_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WInput.SetMouseState(WinFormToWPFMouseButton(e), true);
        }

        private void GlControlHost_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WInput.SetMouseScrollDelta(e.Delta);
        }

        private void GlControlHost_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            WInput.SetKeyboardState(e.Key, false);
        }

        private void GlControlHost_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            WInput.SetKeyboardState(e.Key, true);
        }

        private void GlControlHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double dpi_factor = System.Windows.PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
            int newWidth = (int)(e.NewSize.Width * dpi_factor);
            int newHeight = (int)(e.NewSize.Height * dpi_factor);
            m_viewModel.WindEditor.OnViewportResized(newWidth, newHeight);
        }

        private static System.Windows.Input.MouseButton WinFormToWPFMouseButton(System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Input.MouseButton btn = System.Windows.Input.MouseButton.Left;
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    btn = System.Windows.Input.MouseButton.Left;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    btn = System.Windows.Input.MouseButton.Middle;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    btn = System.Windows.Input.MouseButton.Right;
                    break;
                case System.Windows.Forms.MouseButtons.XButton1:
                    btn = System.Windows.Input.MouseButton.XButton1;
                    break;
                case System.Windows.Forms.MouseButtons.XButton2:
                    btn = System.Windows.Input.MouseButton.XButton2;
                    break;
            }

            return btn;
        }

		/// <summary>
		/// We can't use PropertyBinding on the PropertyDefinitions, but we can get a callback when the object changes
		/// and then get the properties off of them manually, insert them, and then forcibly update it again... :D
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PropertyGrid_SelectedObjectChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			PropertyGrid grid = sender as PropertyGrid;
			if(grid != null)
			{
				grid.PropertyDefinitions.Clear();
				WDOMNode node = grid.SelectedObject as WDOMNode;
				if(node != null)
				{
					foreach(var property in node.VisibleProperties)
					{
						grid.PropertyDefinitions.Add(property);
					}

					grid.Update();
				}
			}
		}

        private bool m_ignoreSelectionChange;
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_ignoreSelectionChange || e.NewValue == null || m_viewModel.WindEditor.MainWorld.CurrentMode.GetType() != typeof(ActorMode))
                return;

            ActorMode mode = m_viewModel.WindEditor.MainWorld.CurrentMode as ActorMode;
            var selection = mode.EditorSelection;

            m_ignoreSelectionChange = true;
            selection.ClearSelection();
            selection.AddToSelection(e.NewValue as WDOMNode);
            m_ignoreSelectionChange = false;
        }

        private void TreeView_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = e.OriginalSource as MenuItem;
            if (menuItem.IsChecked)
            {
                foreach (var item in MenuItemExtensions.ElementToGroupNames)
                {
                    if (item.Key != menuItem && item.Value == MenuItemExtensions.GetGroupName(menuItem))
                    {
                        item.Key.IsChecked = false;
                    }
                }
            }
            else // it's not possible for the user to deselect an item
            {
                menuItem.IsChecked = true;
            }

            switch (menuItem.Name)
            {
                case "default":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Default;
                    break;
                case "layer_0":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer0;
                    break;
                case "layer_1":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer1;
                    break;
                case "layer_2":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer2;
                    break;
                case "layer_3":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer3;
                    break;
                case "layer_4":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer4;
                    break;
                case "layer_5":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer5;
                    break;
                case "layer_6":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer6;
                    break;
                case "layer_7":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer7;
                    break;
                case "layer_8":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer8;
                    break;
                case "layer_9":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.Layer9;
                    break;
                case "layer_a":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.LayerA;
                    break;
                case "layer_b":
                    m_viewModel.WindEditor.ActiveLayer = MapLayer.LayerB;
                    break;
            }
        }
    }

    public class MenuItemExtensions : DependencyObject
    {
        public static Dictionary<MenuItem, String> ElementToGroupNames = new Dictionary<MenuItem, String>();

        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.RegisterAttached("GroupName",
                                         typeof(String),
                                         typeof(MenuItemExtensions),
                                         new PropertyMetadata(String.Empty, OnGroupNameChanged));

        public static void SetGroupName(MenuItem element, String value)
        {
            element.SetValue(GroupNameProperty, value);
        }

        public static String GetGroupName(MenuItem element)
        {
            return element.GetValue(GroupNameProperty).ToString();
        }

        private static void OnGroupNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Add an entry to the group name collection
            var menuItem = d as MenuItem;

            if (menuItem != null)
            {
                String newGroupName = e.NewValue.ToString();
                String oldGroupName = e.OldValue.ToString();
                if (String.IsNullOrEmpty(newGroupName))
                {
                    //Removing the toggle button from grouping
                    RemoveCheckboxFromGrouping(menuItem);
                }
                else
                {
                    //Switching to a new group
                    if (newGroupName != oldGroupName)
                    {
                        if (!String.IsNullOrEmpty(oldGroupName))
                        {
                            //Remove the old group mapping
                            RemoveCheckboxFromGrouping(menuItem);
                        }
                        ElementToGroupNames.Add(menuItem, e.NewValue.ToString());
                        menuItem.Checked += MenuItemChecked;
                    }
                }
            }
        }

        private static void RemoveCheckboxFromGrouping(MenuItem checkBox)
        {
            ElementToGroupNames.Remove(checkBox);
            checkBox.Checked -= MenuItemChecked;
        }

        static void MenuItemChecked(object sender, RoutedEventArgs e)
        {
            var menuItem = e.OriginalSource as MenuItem;
            foreach (var item in ElementToGroupNames)
            {
                if (item.Key != menuItem && item.Value == GetGroupName(menuItem))
                {
                    item.Key.IsChecked = false;
                }
            }
        }
    }
}
