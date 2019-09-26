using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WindEditor.ViewModel;
using WindEditor.View;
using WindEditor.Collision;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace WindEditor.Editor.Modes
{
    public class CollisionMode : IEditorMode
    {
        private TreeView m_test_tree;
        private DockPanel m_ModeControlsDock;
        private WDetailsViewViewModel m_DetailsViewModel;
        private WCollisionMesh m_CollisionMesh;

        public DockPanel ModeControlsDock
        {
            get { return m_ModeControlsDock; }
            set
            {
                if (value != m_ModeControlsDock)
                {
                    m_ModeControlsDock = value;
                    OnPropertyChanged("ModeControlsDock");
                }
            }
        }
        public WDetailsViewViewModel DetailsViewModel
        {
            get { return m_DetailsViewModel; }
            set
            {
                if (value != m_DetailsViewModel)
                {
                    m_DetailsViewModel = value;
                    OnPropertyChanged("DetailsViewModel");
                }
            }
        }

        public WCollisionMesh ActiveCollisionMesh
        {
            get { return m_CollisionMesh; }
            set
            {
                if (value != m_CollisionMesh)
                {
                    m_CollisionMesh = value;
                    OnPropertyChanged("ActiveCollisionMesh");
                }
            }
        }

        public Selection EditorSelection { get; set; }
        public WWorld World { get; }

        public event EventHandler<GenerateUndoEventArgs> GenerateUndoEvent;

        public CollisionMode(WWorld world)
        {
            World = world;

            EditorSelection = new Selection(this);
            EditorSelection.OnSelectionChanged += OnSelectionChanged;

            DetailsViewModel = new WDetailsViewViewModel();

            ModeControlsDock = CreateUI();
        }

        private DockPanel CreateUI()
        {
            DockPanel collision_dock_panel = new DockPanel()
            {
                LastChildFill = true
            };

            StackPanel collision_stack_panel = new StackPanel()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
            };

            RowDefinition tree_row = new RowDefinition();
            RowDefinition splitter_row = new RowDefinition();
            RowDefinition properties_row = new RowDefinition();

            tree_row.Height = System.Windows.GridLength.Auto;
            tree_row.MaxHeight = 500;
            tree_row.MinHeight = 10;
            splitter_row.Height = System.Windows.GridLength.Auto;
            properties_row.Height = System.Windows.GridLength.Auto;
            properties_row.MinHeight = 80;

            Grid col_grid = new Grid();
            col_grid.RowDefinitions.Add(tree_row);
            col_grid.RowDefinitions.Add(splitter_row);
            col_grid.RowDefinitions.Add(properties_row);

            System.Windows.HierarchicalDataTemplate template = new System.Windows.HierarchicalDataTemplate(typeof(CollisionGroupNode));
            template.ItemsSource = new Binding("Children");

            System.Windows.FrameworkElementFactory tb = new System.Windows.FrameworkElementFactory(typeof(TextBlock));
            tb.SetBinding(TextBlock.TextProperty, new Binding("Name"));
            tb.SetValue(TextBlock.ForegroundProperty, Brushes.Black);

            template.VisualTree = tb;

            m_test_tree = new TreeView()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                ItemTemplate = template
            };
            m_test_tree.SelectedItemChanged += M_test_tree_SelectedItemChanged;

            Grid.SetRow(m_test_tree, 0);

            GridSplitter splitter = new GridSplitter()
            {
                Height = 5,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
            };
            Grid.SetRow(splitter, 1);

            WDetailsView actor_details = new WDetailsView()
            {
                Name = "Details",
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                DataContext = DetailsViewModel
            };

            GroupBox actor_prop_box = new GroupBox()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                Header = "Properties",
                Content = actor_details,
            };

            Grid.SetRow(actor_prop_box, 2);

            col_grid.Children.Add(m_test_tree);
            col_grid.Children.Add(splitter);
            col_grid.Children.Add(actor_prop_box);

            DockPanel.SetDock(collision_stack_panel, Dock.Top);

            collision_stack_panel.Children.Add(col_grid);
            collision_dock_panel.Children.Add(collision_stack_panel);

            return collision_dock_panel;
        }

        private void M_test_tree_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            throw new NotImplementedException();
        }

        public void OnBecomeActive()
        {
            WScene focused = World.Map.SceneList[0];
            List<WCollisionMesh> meshes = focused.GetChildrenOfType<WCollisionMesh>();
            ActiveCollisionMesh = meshes[0];

            m_test_tree.ItemsSource = new ObservableCollection<CollisionGroupNode>() { ActiveCollisionMesh.RootNode };
        }

        public void Update(WSceneView view)
        {
            //throw new NotImplementedException();
        }

        public void FilterSceneForRenderer(WSceneView view, WWorld world)
        {
            foreach (WScene scene in world.Map.SceneList)
            {
                foreach (var renderable in scene.GetChildrenOfType<IRenderable>())
                    renderable.AddToRenderer(view);
            }
        }

        public void BroadcastUndoEventGenerated(WUndoCommand command)
        {
            GenerateUndoEvent?.Invoke(this, new GenerateUndoEventArgs(command));
        }

        private void OnSelectionChanged()
        {
            if (EditorSelection.PrimarySelectedObject != null)
                DetailsViewModel.ReflectObject(EditorSelection.PrimarySelectedObject);
        }

        #region INotifyPropertyChanged Support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CollisionMode()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
