using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WindEditor.Serialization;

namespace WindEditor
{
    public class WActorEditor : IDisposable
    {
		public Selection EditorSelection { get; protected set; }
        public ICommand CutSelectionCommand { get { return new RelayCommand(x => CutSelection(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand CopySelectionCommand { get { return new RelayCommand(x => CopySelection(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand PasteSelectionCommand { get { return new RelayCommand(x => PasteSelection(), (x) => false ); } }
        public ICommand DeleteSelectionCommand { get { return new RelayCommand(x => DeleteSelection(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand SelectAllCommand { get { return new RelayCommand(x => SelectAll(), (x) => true); } }
        public ICommand SelectNoneCommand { get { return new RelayCommand(x => SelectNone(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand CreateEntityCommand { get { return new RelayCommand(EntityFourCC => CreateEntity(EntityFourCC as string)); } }

        private WWorld m_world;
        private WTransformGizmo m_transformGizmo;

        // To detect redundant calls
        private bool m_hasBeenDisposed = false;


        public WActorEditor(WWorld world)
        {
            m_world = world;
            m_transformGizmo = new WTransformGizmo(m_world);

			EditorSelection = new Selection(m_world, this);
			EditorSelection.OnSelectionChanged += OnSelectionChanged;
        }

		private void OnSelectionChanged()
		{
			// This will get invoked when an Undo happens which allows the gizmo to fix itself.
			UpdateGizmoTransform();
		}

        public void UpdateGizmoTransform()
        {
			OpenTK.Vector3 position = OpenTK.Vector3.Zero;
			OpenTK.Quaternion localRotation = OpenTK.Quaternion.Identity;

			foreach(var entity in EditorSelection.SelectedObjects)
            {
				position += entity.Transform.Position;
            }

			if(EditorSelection.SelectedObjects.Count > 0)
			{
				position /= EditorSelection.SelectedObjects.Count;
				localRotation = EditorSelection.SelectedObjects[0].Transform.LocalRotation;
			}

			m_transformGizmo.SetPosition(position);
			m_transformGizmo.SetLocalRotation(localRotation);
        }

        public void CreateEntity(string fourCC)
        {
			throw new System.NotImplementedException();

			// ToDo: This can spawn specific classes the same way that the actor loader does.
        }

        public void UpdateForSceneView(WSceneView view)
        {
            // Update our Selection Gizmo first, so we can check if it is currently transforming when we check to see
            // if the user's selection has changed.
            UpdateSelectionGizmo(view);

            // Check to see if they've left clicked and are changing their selection.
            CheckForObjectSelectionChange(view);

            // Add our gizmo to the renderer this frame.
            ((IRenderable)m_transformGizmo).AddToRenderer(view);
        }

        private void CheckForObjectSelectionChange(WSceneView view)
        {
            // If we have a gizmo and we're transforming it, don't check for selection change.
            if (m_transformGizmo != null && m_transformGizmo.IsTransforming)
                return;
            if (WInput.GetMouseButtonDown(0) && !WInput.GetMouseButton(1))
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                var addedActor = Raycast(mouseRay);

                // Check the behaviour of this click to determine appropriate selection modification behaviour.
                // Click w/o Modifiers = Clear Selection, add result to selection
                // Click /w Ctrl = Toggle Selection State
                // Click /w Shift = Add to Selection
                bool ctrlPressed = WInput.GetKey(Key.LeftCtrl) || WInput.GetKey(Key.RightCtrl);
                bool shiftPressed = WInput.GetKey(Key.LeftShift) || WInput.GetKey(Key.RightShift);

                if (!ctrlPressed & !shiftPressed)
                {
					EditorSelection.ClearSelection();
					if (addedActor != null)
						EditorSelection.AddToSelection(addedActor);
                }
                else if (addedActor != null && (ctrlPressed && !shiftPressed))
                {
					if (addedActor.IsSelected)
						EditorSelection.RemoveFromSelection(addedActor);
					else
						EditorSelection.AddToSelection(addedActor);
                }
                else if (addedActor != null && shiftPressed)
                {
					if(!EditorSelection.SelectedObjects.Contains(addedActor))
						EditorSelection.AddToSelection(addedActor);
                }

				UpdateGizmoTransform();
            }
        }

        private void UpdateSelectionGizmo(WSceneView view)
        {
            if (!m_transformGizmo.Enabled && EditorSelection.SelectedObjects.Count > 0)
            {
                // Show the Transform Gizmo.
                m_transformGizmo.Enabled = true;
				UpdateGizmoTransform();
                // m_transformGizmo.SetPosition(m_selectionList[0].Transform.Position);
                // m_transformGizmo.SetLocalRotation(m_selectionList[0].Transform.Rotation);
            }
            else if (m_transformGizmo.Enabled && EditorSelection.SelectedObjects.Count == 0)
            {
                // Hide the Transform Gizmo.
                m_transformGizmo.Enabled = false;
            }

            if (!m_transformGizmo.Enabled)
                return;

            if (WInput.GetKeyDown(Key.Q) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.None);
            }
            if (WInput.GetKeyDown(Key.W) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Translation);
            }
            if (WInput.GetKeyDown(Key.E) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Rotation);
            }
            if (WInput.GetKeyDown(Key.R) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Scale);
            }

            if (WInput.GetKeyDown(Key.OemOpenBrackets))
            {
                m_transformGizmo.DecrementSize();
            }

            if (WInput.GetKeyDown(Key.OemCloseBrackets))
            {
                m_transformGizmo.IncrementSize();
            }

            if(WInput.GetKeyDown(Key.OemTilde))
            {
                if (m_transformGizmo.TransformSpace == FTransformSpace.World)
                    m_transformGizmo.SetTransformSpace(FTransformSpace.Local);
                else
                    m_transformGizmo.SetTransformSpace(FTransformSpace.World);

                UpdateGizmoTransform();
            }

            if (WInput.GetMouseButtonDown(0))
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                if (m_transformGizmo.CheckSelectedAxes(mouseRay))
                {                            
                    m_transformGizmo.StartTransform();
                }
            }

            if (WInput.GetMouseButtonUp(0))
            {
                if(m_transformGizmo.IsTransforming)
                {
                    // When we end let go of the gizmo, we want to make one last action which specifies that it is done,
                    // so that the next gizmo move doesn't merge with the previous.
                    WUndoCommand undoAction = CreateUndoActionForGizmo(true);
                    if (undoAction != null)
                        m_world.UndoStack.Push(undoAction);

                    m_transformGizmo.EndTransform();
                }
            }

            if (m_transformGizmo.IsTransforming)
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                if (m_transformGizmo.TransformFromInput(mouseRay, view))
                {
                    WUndoCommand undoAction = CreateUndoActionForGizmo(false);
                    if(undoAction != null)
                        m_world.UndoStack.Push(undoAction);
                }
            }

            m_transformGizmo.UpdateForSceneView(view);
        }

        private WDOMNode Raycast(FRay ray)
        {
            if (m_world.Map == null)
                return null;

            WDOMNode closestResult = null;
            float closestDistance = float.MaxValue;

            foreach (var scene in m_world.Map.SceneList)
            {
                var allActors = scene.GetChildrenOfType<VisibleDOMNode>();

                foreach (VisibleDOMNode actorNode in allActors)
                {
                    float intersectDistance;
                    bool hitActor = actorNode.Raycast(ray, out intersectDistance);
                    if (hitActor)
                    {
                        if (intersectDistance >= 0 && intersectDistance < closestDistance)
                        {
                            closestDistance = intersectDistance;
                            closestResult = actorNode;
                        }
                    }
                }
            }

            return closestResult;
        }

        private WUndoCommand CreateUndoActionForGizmo(bool isDone)
        {
            WUndoCommand undoAction = null;

			var actors = EditorSelection.SelectedObjects;
            switch (m_transformGizmo.Mode)
            {
                case FTransformMode.Translation:
                    undoAction = new WTranslateActorAction(actors, this, m_transformGizmo.DeltaTranslation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Rotation:
                    undoAction = new WRotateActorAction(actors, this, m_transformGizmo.DeltaRotation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Scale:
                    undoAction = new WScaleActorAction(actors, this, m_transformGizmo.DeltaScale, isDone);
                    Console.WriteLine(m_transformGizmo.DeltaScale);
                    break;
                default:
                    break;
            }

            return undoAction;
        }

        private void CutSelection()
        {
            if (EditorSelection.SelectedObjects.Count == 0)
                return;

            CopySelection();
            DeleteSelection();
        }

        private void CopySelection()
        {
			throw new NotImplementedException();
        }

        private void PasteSelection()
        {
            throw new NotImplementedException();
        }

        private void DeleteSelection()
        {
			throw new System.NotImplementedException();
            // foreach (var item in m_selectionList)
            // {
            //     item.Parent.RemoveChild(item);
            // }
			// 
            // ModifySelection(SelectionType.Add, new WActorNode[] { null });
        }

        private void SelectAll()
        {
			throw new System.NotImplementedException();
            // if (m_world.Map == null || m_world.Map.FocusedScene == null)
            //     return;
			// 
            // var allActorsInSelectedScene = m_world.Map.FocusedScene.GetChildrenOfType<WActorNode>();
            // ModifySelection(SelectionType.Add, allActorsInSelectedScene.ToArray());
        }

        private void SelectNone()
        {
			throw new System.NotImplementedException();
            //ClearSelection();
            //ModifySelection(SelectionType.Add, new WActorNode[] { null });
        }

        #region IDisposable Support
        ~WActorEditor()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        protected virtual void Dispose(bool manualDispose)
        {
            if (!m_hasBeenDisposed)
            {
                if (manualDispose)
                {
                    // Dispose managed state (managed objects).
                    m_transformGizmo.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.

                m_hasBeenDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
