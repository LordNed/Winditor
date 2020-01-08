using JStudio.J3D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;
using WindEditor.Collision;

namespace WindEditor
{
    public class NodeTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SerializableDOMNode)
            {
                SerializableDOMNode srlNode = value as SerializableDOMNode;

                if (srlNode.FourCC.ToString().Contains("ACT") ||
                    srlNode.FourCC.ToString().Contains("SCO") || srlNode.FourCC.ToString().Contains("TRE"))
                    return true;

                switch (srlNode.FourCC)
                {
                    case FourCC.CAMR:
                    case FourCC.DOOR:
                    case FourCC.TGDR:
                    case FourCC.TGOB:
                    case FourCC.TGSC:
                    case FourCC.RPPN:
                    case FourCC.PPNT:
                    case FourCC.PLYR:
                    case FourCC.RARO:
                    case FourCC.RCAM:
                    case FourCC.SOND:
                    case FourCC.SHIP:
                    case FourCC.LGTV:
                    case FourCC.LGHT:
                    case FourCC.AROB:
                        return true;
                }

                return false;
            }
            else if (value is SerializableDOMNode || value is WDOMGroupNode)
            {
                WDOMGroupNode grpNode = value as WDOMGroupNode;

                if (grpNode.FourCC.ToString().Contains("ACT") ||
                    grpNode.FourCC.ToString().Contains("SCO") || grpNode.FourCC.ToString().Contains("TRE"))
                    return true;

                switch (grpNode.FourCC)
                {
                    case FourCC.DOOR:
                    case FourCC.TGDR:
                    case FourCC.TGOB:
                    case FourCC.TGSC:
                    case FourCC.RPPN:
                    case FourCC.PPNT:
                    case FourCC.PLYR:
                    case FourCC.RARO:
                    case FourCC.SOND:
                    case FourCC.SHIP:
                    case FourCC.LGTV:
                    case FourCC.LGHT:
                    case FourCC.AROB:
                        return true;
                }

                return false;
            }
            else if (value is WDOMLayeredGroupNode || value is CategoryDOMNode || value is J3DNode || value is WCollisionMesh)
                return true;
            else if (value is WRoom || value is WStage)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return typeof(VisibleDOMNode);
            else
                return typeof(WDOMNode);
        }
    }

    [HideCategoriesAttribute()]
    public partial class VisibleDOMNode : IRenderable
	{
		protected SimpleObjRenderer m_objRender;
        protected List<J3D> m_actorMeshes;
		public TevColorOverride ColorOverrides { get; protected set; }

		public override void OnConstruction()
		{
			base.OnConstruction();

			ColorOverrides = new TevColorOverride();
            m_actorMeshes = new List<J3D>();
            PropertyChanged += VisibleDOMNode_PropertyChanged;
            IsRendered = true;
		}

        public override void PostLoad()
		{
			base.PostLoad();

            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1, 1, 1, 1));
        }

		public override FAABox GetBoundingBox()
		{
			FAABox modelABB = m_objRender != null ? m_objRender.GetAABB() : m_actorMeshes[0].BoundingBox;
			modelABB.ScaleBy(Transform.LocalScale);

			return new FAABox(modelABB.Min + Transform.Position, modelABB.Max + Transform.Position);
		}

		public bool Raycast(FRay ray, out float closestDistance)
		{
			// Convert the ray to local space of this node since all of our raycasts are local.
			FRay localRay = WMath.TransformRay(ray, Transform.Position, Transform.LocalScale, Transform.Rotation.Inverted());
            closestDistance = float.MaxValue;
			bool bHit = false;

			if (m_actorMeshes.Count > 0)
			{
                foreach (var actor_mesh in m_actorMeshes)
                {
                    bHit = actor_mesh.Raycast(localRay, out closestDistance, true);

                    if (bHit)
                        break;
                }
			}
			else
			{
				bHit = WMath.RayIntersectsAABB(localRay, m_objRender.GetAABB().Min, m_objRender.GetAABB().Max, out closestDistance);

                if (bHit)
                {
                    // Convert the hit point back to world space...
                    Vector3 localHitPoint = localRay.Origin + (localRay.Direction * closestDistance);
                    localHitPoint = Vector3.Transform(localHitPoint + Transform.Position, Transform.Rotation);

                    // Now get the distance from the original ray origin and the new worldspace hit point.
                    closestDistance = (localHitPoint - ray.Origin).Length;
                }
            }

			return bHit;
		}

        protected virtual void VisibleDOMNode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
                PostLoad();
        }

		#region IRenderable
		virtual public void AddToRenderer(WSceneView view)
		{
			view.AddOpaqueMesh(this);
		}

		virtual public void Draw(WSceneView view)
		{
			Matrix4 trs = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);

			if (m_actorMeshes.Count > 0)
			{
                foreach (var actor_mesh in m_actorMeshes)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (ColorOverrides.ColorsEnabled[i])
                            actor_mesh.SetTevColorOverride(i, ColorOverrides.Colors[i]);

                        if (ColorOverrides.ConstColorsEnabled[i])
                            actor_mesh.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
                    }

                    if (IsSelected)
                        actor_mesh.Tick(1 / (float)60);

                    actor_mesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
                }
			}
			else if (m_objRender != null)
				m_objRender.Render(view.ViewMatrix, view.ProjMatrix, trs);
		}

		virtual public Vector3 GetPosition()
		{
			Vector3 modelOffset = Vector3.Zero;
			if (m_actorMeshes.Count > 0)
				modelOffset += m_actorMeshes[0].BoundingBox.Center;

			return Transform.Position + modelOffset;
		}

		virtual public float GetBoundingRadius()
		{
			Vector3 lScale = Transform.LocalScale;
			float largestMax = lScale[0];
			for (int i = 1; i < 3; i++)
				if (lScale[i] > largestMax)
					largestMax = lScale[i];

			float boundingSphere = 86f; // Default Editor Cube
			if (m_actorMeshes.Count > 0)
				boundingSphere = m_actorMeshes[0].BoundingSphere.Radius;
			return largestMax * boundingSphere;
		}
		#endregion
	}
}
