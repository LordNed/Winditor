using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using WindEditor.View;
using WindEditor.ViewModel;
using WindEditor.Events;

namespace WindEditor.Editor.Modes
{
    public class EventMode : IEditorMode
    {
        private DockPanel m_ModeControlsDock;
        private WDetailsViewViewModel m_DetailsViewModel;
        private ComboBox m_EventCombo;

        private WEventList m_EventList;
        private Event m_SelectedEvent;

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

        public WEventList EventList
        {
            get { return m_EventList; }
            set
            {
                if (value != m_EventList)
                {
                    m_EventList = value;
                    OnPropertyChanged("EventList");
                }
            }
        }

        public Event SelectedEvent
        {
            get { return m_SelectedEvent; }
            set
            {
                if (value != m_SelectedEvent)
                {
                    m_SelectedEvent = value;
                    OnPropertyChanged("SelectedEvent");
                }
            }
        }

        public WWorld World { get; }

        public event EventHandler<GenerateUndoEventArgs> GenerateUndoEvent;

        public EventMode(WWorld world)
        {
            World = world;
            DetailsViewModel = new WDetailsViewViewModel();

            ModeControlsDock = CreateUI();
        }

        /// <summary>
        /// Generates a DockPanel that contains the controls specific to this editor mode.
        /// </summary>
        private DockPanel CreateUI()
        {
            DockPanel event_dock_panel = new DockPanel()
            {
                LastChildFill = true
            };

            StackPanel event_stack_panel = new StackPanel()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
            };

            RowDefinition combo_row = new RowDefinition();
            RowDefinition properties_row = new RowDefinition();

            combo_row.Height = System.Windows.GridLength.Auto;
            combo_row.MaxHeight = 500;
            combo_row.MinHeight = 10;
            properties_row.Height = System.Windows.GridLength.Auto;
            //properties_row.MinHeight = 80;

            Grid ev_grid = new Grid();
            ev_grid.RowDefinitions.Add(combo_row);
            ev_grid.RowDefinitions.Add(properties_row);

            m_EventCombo = new ComboBox()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                DisplayMemberPath = "Name"
            };

            m_EventCombo.SelectionChanged += M_EventCombo_SelectionChanged;

            Grid.SetRow(m_EventCombo, 0);

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

            Grid.SetRow(actor_prop_box, 1);

            ev_grid.Children.Add(m_EventCombo);
            ev_grid.Children.Add(actor_prop_box);

            DockPanel.SetDock(event_stack_panel, Dock.Top);

            event_stack_panel.Children.Add(ev_grid);
            event_dock_panel.Children.Add(event_stack_panel);

            return event_dock_panel;
        }

        private void M_EventCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEvent = EventList.Events[m_EventCombo.SelectedIndex];
            m_DetailsViewModel.ReflectObject(SelectedEvent);
        }

        public void BroadcastUndoEventGenerated(WUndoCommand command)
        {
            //throw new NotImplementedException();
        }

        public void ClearSelection()
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

        public void OnBecomeActive()
        {
            WStage stage = (WStage)World.Map.SceneList.First(x => x.GetType() == typeof(WStage));
            EventList = stage.GetChildrenOfType<WEventList>()[0];

            m_EventCombo.ItemsSource = EventList.Events;
            m_EventCombo.SelectedIndex = 0;
        }

        public void OnBecomeInactive()
        {
            //throw new NotImplementedException();
        }

        public void Update(WSceneView view)
        {
            //throw new NotImplementedException();
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
