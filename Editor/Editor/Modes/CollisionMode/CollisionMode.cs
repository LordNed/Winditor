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
using System.Windows.Input;
using System.Windows;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WindEditor.Editor.Modes
{
    public class CollisionMode : IEditorMode
    {
        public ICommand ExportToDAECommand { get { return new RelayCommand(x => ExportToDae()); } }
        public ICommand ImportFromDAECommand { get { return new RelayCommand(x => ImportFromDae()); } }

        private TreeView m_CollisionTree;
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

        public Selection<CollisionTriangle> EditorSelection { get; set; }
        public WWorld World { get; }

        public event EventHandler<GenerateUndoEventArgs> GenerateUndoEvent;

        public CollisionMode(WWorld world)
        {
            World = world;

            EditorSelection = new Selection<CollisionTriangle>(this);
            EditorSelection.OnSelectionChanged += OnSelectionChanged;

            DetailsViewModel = new WDetailsViewViewModel();

            ModeControlsDock = CreateUI();
        }

        private void OnItemMouseDoubleClick(object sender, MouseButtonEventArgs args)
        {
            if (args.ClickCount > 1)
            {
                if (args.OriginalSource is TextBlock)
                {
                    TextBlock orig = args.OriginalSource as TextBlock;

                    if (orig.DataContext != null && orig.DataContext is CollisionGroupNode)
                    {
                        CollisionGroupNode group = orig.DataContext as CollisionGroupNode;

                        ClearSelection();

                        int capacity = group.GatherTriangles(null);
                        List<CollisionTriangle> triangles = new List<CollisionTriangle>(capacity);
                        group.GatherTriangles(triangles);

                        if (triangles.Count > 0)
                        {
                            AddTriangleToSelection(triangles);
                        }
                        else
                        {
                            MessageBox.Show($"The collision node \"{group.Name}\" has no associated geometry.", "Collision Node Error", MessageBoxButton.OK);
                        }

                        args.Handled = true;
                    }
                }
            }
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

            m_CollisionTree = new TreeView()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                ItemTemplate = template
            };
            m_CollisionTree.SelectedItemChanged += M_test_tree_SelectedItemChanged;
            m_CollisionTree.PreviewMouseLeftButtonDown += OnItemMouseDoubleClick;

            Grid.SetRow(m_CollisionTree, 0);

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

            col_grid.Children.Add(m_CollisionTree);
            col_grid.Children.Add(splitter);
            col_grid.Children.Add(actor_prop_box);

            DockPanel.SetDock(collision_stack_panel, Dock.Top);

            collision_stack_panel.Children.Add(col_grid);
            collision_dock_panel.Children.Add(collision_stack_panel);

            return collision_dock_panel;
        }

        private void M_test_tree_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            CollisionGroupNode selected = e.NewValue as CollisionGroupNode;

            /*ClearSelection();

            List<CollisionTriangle> new_selected_tris = selected.GetTrianglesRecursive();

            foreach (CollisionTriangle t in new_selected_tris)
            {
                AddTriangleToSelection(t);
            }

            if (EditorSelection.SelectedObjects.Count > 1)
            {
                EditorSelection.SelectedObjects[0].Properties.PropertyChanged += OnTriPropertyChanged;
            }*/

            ClearSelection();

            m_DetailsViewModel.ReflectObject(selected);

            //OnSelectionChanged();
        }

        private void OnTriPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var new_value = sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null);

            for (int i = 1; i < EditorSelection.SelectedObjects.Count; i++)
            {
                CollisionProperty tri_props = EditorSelection.SelectedObjects[i].Properties;
                tri_props.GetType().GetProperty(e.PropertyName).SetValue(tri_props, new_value, null);
            }
        }

        public void OnBecomeActive()
        {
            UpdateActiveMesh();

            World.Map.PropertyChanged += OnFocusedSceneChanged;
        }

        public void OnBecomeInactive()
        {
            World.Map.PropertyChanged -= OnFocusedSceneChanged;
        }

        private void OnFocusedSceneChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "FocusedScene")
            {
                return;
            }

            ClearSelection();

            UpdateActiveMesh();
        }

        private void UpdateActiveMesh()
        {
            if (ActiveCollisionMesh != null)
                ActiveCollisionMesh.IsRendered = ActiveCollisionMesh.PreviousRenderVisibility;

            if (World.Map.FocusedScene is WStage)
            {
                m_CollisionTree.ItemsSource = null;
                ActiveCollisionMesh = null;
                return;
            }

            List<WCollisionMesh> meshes = World.Map.FocusedScene.GetChildrenOfType<WCollisionMesh>();

            if (meshes.Count > 0)
            {
                ActiveCollisionMesh = meshes[0];
                ActiveCollisionMesh.PreviousRenderVisibility = ActiveCollisionMesh.IsRendered;
                ActiveCollisionMesh.IsRendered = true;

                m_CollisionTree.ItemsSource = new ObservableCollection<CollisionGroupNode>() { ActiveCollisionMesh.RootNode };
            }
        }

        public void Update(WSceneView view)
        {
            if (WInput.GetMouseButton(0) && !WInput.GetMouseButton(1))
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                var addedActor = Raycast(mouseRay);

                // Check the behaviour of this click to determine appropriate selection modification behaviour.
                // Click w/o Modifiers = Clear Selection, add result to selection
                // Click /w Ctrl = Toggle Selection State
                // Click /w Shift = Add to Selection
                bool ctrlPressed = WInput.GetKey(Key.LeftCtrl) || WInput.GetKey(Key.RightCtrl);
                bool shiftPressed = WInput.GetKey(Key.LeftShift) || WInput.GetKey(Key.RightShift);

                if (ctrlPressed && !WInput.GetMouseButtonDown(0))
                    return;

                if (!ctrlPressed & !shiftPressed)
                {
                    ClearSelection();
                    if (addedActor != null)
                        AddTriangleToSelection(addedActor);
                }
                else if (addedActor != null && (ctrlPressed && !shiftPressed))
                {
                    if (addedActor.IsSelected)
                        RemoveTriangleFromSelection(addedActor);
                    else
                        AddTriangleToSelection(addedActor);
                }
                else if (addedActor != null && shiftPressed)
                {
                    if (!EditorSelection.SelectedObjects.Contains(addedActor))
                        AddTriangleToSelection(addedActor);
                }
            }
        }

        public void Shutdown()
        {
            m_CollisionTree.ItemsSource = null;
            m_DetailsViewModel.Categories = new System.Collections.Specialized.OrderedDictionary();
        }

        private void AddTriangleToSelection(CollisionTriangle tri)
        {
            tri.Select();
            int prev_count = EditorSelection.SelectedObjects.Count;
            EditorSelection.AddToSelection(tri);

            if (prev_count == 1)
            {
                EditorSelection.SelectedObjects[0].Properties.PropertyChanged += OnTriPropertyChanged;
            }

            OnSelectionChanged();
        }

        private void AddTriangleToSelection(IEnumerable<CollisionTriangle> tris)
        {
            foreach (CollisionTriangle t in tris)
                t.Select();

            EditorSelection.AddToSelection(tris);
            EditorSelection.SelectedObjects[0].Properties.PropertyChanged += OnTriPropertyChanged;

            OnSelectionChanged();
        }

        private void RemoveTriangleFromSelection(CollisionTriangle tri)
        {
            tri.Deselect();

            if (tri == EditorSelection.SelectedObjects[0])
            {
                EditorSelection.SelectedObjects[0].Properties.PropertyChanged -= OnTriPropertyChanged;
            }

            EditorSelection.SelectedObjects.Remove(tri);

            if (EditorSelection.SelectedObjects.Count > 1)
            {
                EditorSelection.SelectedObjects[0].Properties.PropertyChanged += OnTriPropertyChanged;
            }

            OnSelectionChanged();
        }

        private CollisionTriangle Raycast(FRay ray)
        {
            if (World.Map == null || ActiveCollisionMesh == null)
                return null;

            CollisionTriangle closestResult = null;
            float closestDistance = float.MaxValue;

            foreach (var tri in ActiveCollisionMesh.Triangles)
            {
                float dist = float.MaxValue;

                if (WMath.RayIntersectsTriangle(ray, tri.Vertices[1], tri.Vertices[0], tri.Vertices[2], true, out dist))
                {
                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        closestResult = tri;
                    }
                }
            }

            return closestResult;
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

        public void ClearSelection()
        {
            foreach (CollisionTriangle t in EditorSelection.SelectedObjects)
            {
                t.Deselect();
            }

            if (EditorSelection.SelectedObjects.Count > 1)
            {
                EditorSelection.SelectedObjects[0].Properties.PropertyChanged -= OnTriPropertyChanged;
            }

            EditorSelection.ClearSelection();

            OnSelectionChanged();
        }

        private void OnSelectionChanged()
        {
            if (EditorSelection.PrimarySelectedObject != null)
                DetailsViewModel.ReflectObject(EditorSelection.PrimarySelectedObject.Properties);
            else if (EditorSelection.SelectedObjects.Count > 1)
                DetailsViewModel.ReflectObject(EditorSelection.SelectedObjects[0].Properties);
            else
                DetailsViewModel.Categories = new System.Collections.Specialized.OrderedDictionary();
        }

        public void ExportToDae()
        {
            if (m_CollisionMesh == null)
            {
                MessageBox.Show("You must select a room before you can export its collision.", "No collision mesh selected");
                return;
            }

            var sfd = new CommonSaveFileDialog()
            {
                Title = "Export DAE",
                DefaultExtension = "dae",
                AlwaysAppendDefaultExtension = true,
            };
            sfd.Filters.Add(new CommonFileDialogFilter("Collada", ".dae"));
            if (WSettingsManager.GetSettings().LastCollisionDaePath.FilePath != "")
            {
                sfd.InitialDirectory = WSettingsManager.GetSettings().LastCollisionDaePath.FilePath;
            }

            if (sfd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                m_CollisionMesh.ToDAEFile(sfd.FileName);
                
                WSettingsManager.GetSettings().LastCollisionDaePath.FilePath = Path.GetDirectoryName(sfd.FileName);
                WSettingsManager.SaveSettings();
            }
        }

        public void ImportFromDae()
        {
            if (m_CollisionMesh == null)
            {
                MessageBox.Show("You must select a room before you can import collision for it.", "No collision mesh selected");
                return;
            }

            var ofd = new CommonOpenFileDialog()
            {
                Title = "Import DAE",
                DefaultExtension = "dae",
            };
            ofd.Filters.Add(new CommonFileDialogFilter("Collada", ".dae"));
            if (WSettingsManager.GetSettings().LastCollisionDaePath.FilePath != "")
            {
                ofd.InitialDirectory = WSettingsManager.GetSettings().LastCollisionDaePath.FilePath;
            }

            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                WRoom currRoom = World.Map.FocusedScene as WRoom;
                m_CollisionMesh.FromDAEFile(ofd.FileName, currRoom.RoomIndex);

                m_CollisionTree.ItemsSource = new ObservableCollection<CollisionGroupNode>() { ActiveCollisionMesh.RootNode };

                WSettingsManager.GetSettings().LastCollisionDaePath.FilePath = Path.GetDirectoryName(ofd.FileName);
                WSettingsManager.SaveSettings();
            }
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
