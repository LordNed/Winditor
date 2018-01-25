using JStudio.J3D;
using OpenTK;
using System;
using System.Windows.Data;
using System.Globalization;

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
            else if (value is WDOMLayeredGroupNode)
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

    public partial class VisibleDOMNode : IRenderable
	{
		private SimpleObjRenderer m_objRender;
		private J3D m_actorMesh;
		public TevColorOverride ColorOverrides { get; protected set; }

		public override void OnConstruction()
		{
			base.OnConstruction();

			ColorOverrides = new TevColorOverride();
            PropertyChanged += VisibleDOMNode_PropertyChanged;
            IsRendered = true;
		}

        public override void SetParent(WDOMNode newParent)
        {
            base.SetParent(newParent);

            if (IsRendered == true)
            {
                WDOMNode parent = Parent;

                while (parent != null)
                {
                    parent.IsRendered = true;
                    parent = parent.Parent;
                }
            }
        }

        public override void PostLoad()
		{
			base.PostLoad();

			m_actorMesh = WResourceManager.LoadActorByName(ToString());
			if (m_actorMesh != null)
			{
				// Create and set up some initial lighting options so character's aren't drawn super brightly
				// until we support actually loading room environment lighting and apply it (see below).
				var lightPos = new Vector4(250, 200, 250, 0);
				var mainLight = new JStudio.OpenGL.GXLight(lightPos, -lightPos.Normalized(), new Vector4(1, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
				var secondaryLight = new JStudio.OpenGL.GXLight(lightPos, -lightPos.Normalized(), new Vector4(0, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));

				Quaternion lightRot = Quaternion.FromAxisAngle(Vector3.UnitY, (float)Math.PI / 2f);
				Vector3 newLightPos = Vector3.Transform(new Vector3(-500, 0, 0), lightRot) + new Vector3(0, 50, 0);

				secondaryLight.Position = new Vector4(newLightPos, 0);

				m_actorMesh.SetHardwareLight(0, mainLight);
				m_actorMesh.SetHardwareLight(1, secondaryLight);
				m_actorMesh.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");
				m_actorMesh.SetTextureOverride("ZAtoon", "resources/textures/ZAtoon.png");

				m_objRender = null;
			}

			if (m_actorMesh == null)
			{
				m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj");
			}
		}

		public override FAABox GetBoundingBox()
		{
			FAABox modelABB = m_objRender != null ? m_objRender.GetAABB() : m_actorMesh.BoundingBox;
			modelABB.ScaleBy(Transform.LocalScale);

			return new FAABox(modelABB.Min + Transform.Position, modelABB.Max + Transform.Position);
		}

		public bool Raycast(FRay ray, out float closestDistance)
		{
			// Convert the ray to local space of this node since all of our raycasts are local.
			FRay localRay = WMath.TransformRay(ray, Transform.Position, Transform.LocalScale, Transform.Rotation.Inverted());
			bool bHit = false;
			if (m_actorMesh != null)
			{
				bHit = m_actorMesh.Raycast(localRay, out closestDistance, true);
			}
			else
			{
				bHit = WMath.RayIntersectsAABB(localRay, m_objRender.GetAABB().Min, m_objRender.GetAABB().Max, out closestDistance);
			}

			if (bHit)
			{
				// Convert the hit point back to world space...
				Vector3 localHitPoint = localRay.Origin + (localRay.Direction * closestDistance);
				localHitPoint = Vector3.Transform(localHitPoint + Transform.Position, Transform.Rotation);

				// Now get the distance from the original ray origin and the new worldspace hit point.
				closestDistance = (localHitPoint - ray.Origin).Length;
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
			var bbox = GetBoundingBox();
			m_world.DebugDrawBox(bbox.Center, (bbox.Max - bbox.Min) / 2, Transform.Rotation, (Flags & NodeFlags.Selected) == NodeFlags.Selected ? WLinearColor.White : WLinearColor.Black, 0, 0);

			Matrix4 trs = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);

			if (m_actorMesh != null)
			{
				for (int i = 0; i < 4; i++)
				{
					if (ColorOverrides.ColorsEnabled[i])
						m_actorMesh.SetTevColorOverride(i, ColorOverrides.Colors[i]);

					if (ColorOverrides.ConstColorsEnabled[i])
						m_actorMesh.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
				}

				m_actorMesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
			}
			else
				m_objRender.Render(view.ViewMatrix, view.ProjMatrix, trs);
		}

		virtual public Vector3 GetPosition()
		{
			Vector3 modelOffset = Vector3.Zero;
			if (m_actorMesh != null)
				modelOffset += m_actorMesh.BoundingBox.Center;

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
			if (m_actorMesh != null)
				boundingSphere = m_actorMesh.BoundingSphere.Radius;
			return largestMax * boundingSphere;
		}
		#endregion
	}
}
