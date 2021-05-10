using JStudio.J3D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;
using WindEditor.Collision;
using System.Linq;
using System.Reflection;

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

        protected bool DisableRotationAndScaleForRaycasting = false;
        protected Vector3 VisualScaleMultiplier = Vector3.One;
        protected virtual Vector3 VisualScale
        {
            get { return  Vector3.Multiply(Transform.LocalScale, VisualScaleMultiplier); }
        }

        // These lists will act as a cache of which switch values this object is currently using.
        protected List<int> UsedInSwitches = null;
        protected List<int> UsedOutSwitches = null;

		public override void OnConstruction()
		{
			base.OnConstruction();

			ColorOverrides = new TevColorOverride();
            m_actorMeshes = new List<J3D>();
            PropertyChanged += VisibleDOMNode_PropertyChanged;
            IsRendered = true;

            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1, 1, 1, 1));
        }

        public override void PostLoad()
		{
			base.PostLoad();

            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1, 1, 1, 1));
        }

		public override FAABox GetBoundingBox()
		{
			FAABox modelABB = m_objRender != null ? m_objRender.GetAABB() : m_actorMeshes[0].BoundingBox;
			modelABB.ScaleBy(VisualScale);

			return new FAABox(modelABB.Min + Transform.Position, modelABB.Max + Transform.Position);
		}

		public bool Raycast(FRay ray, out float closestDistance)
		{
            // Convert the ray to local space of this node since all of our raycasts are local.
            FRay localRay;
            if (DisableRotationAndScaleForRaycasting)
                localRay = WMath.TransformRay(ray, Transform.Position, Vector3.One, Quaternion.Identity);
            else
                localRay = WMath.TransformRay(ray, Transform.Position, VisualScale, Transform.Rotation.Inverted());
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
			else if (m_objRender != null)
			{
                if (m_objRender.FaceCullingEnabled && m_objRender.GetAABB().Contains(localRay.Origin))
                {
                    // If the camera is inside an OBJ render that has backface culling on, the actor won't actually be visible, so don't select it.
                    return false;
                }

                bHit = WMath.RayIntersectsAABB(localRay, m_objRender.GetAABB().Min, m_objRender.GetAABB().Max, out closestDistance);

                if (bHit)
                {
                    // Convert the hit point back to world space...
                    Vector3 localHitPoint = localRay.Origin + (localRay.Direction * closestDistance);
                    Vector3 globalHitPoint = Transform.Position + Vector3.Transform(localHitPoint, Transform.Rotation);

                    // Now get the distance from the original ray origin and the new worldspace hit point.
                    closestDistance = (globalHitPoint - ray.Origin).Length;
                }
            }

			return bHit;
		}

        public List<int> GetUsedInSwitches()
        {
            if (UsedInSwitches == null)
                CalculateUsedSwitches();
            return UsedInSwitches;
        }

        public List<int> GetUsedOutSwitches()
        {
            if (UsedOutSwitches == null)
                CalculateUsedSwitches();
            return UsedOutSwitches;
        }

        public virtual void CalculateUsedSwitches()
        {
            List<int> inSwitches = new List<int>();
            List<int> outSwitches = new List<int>();

            PropertyInfo[] obj_properties = this.GetType().GetProperties();
            foreach (PropertyInfo prop in obj_properties)
            {
                // We want to ignore all properties that are not marked with the WProperty attribute.
                CustomAttributeData[] custom_attributes = prop.CustomAttributes.ToArray();
                CustomAttributeData wproperty_attribute = custom_attributes.FirstOrDefault(x => x.AttributeType.Name == "WProperty");
                if (wproperty_attribute == null)
                    continue;

                // TODO: Once it's possible to disable properties, disabled ones shouldn't be counted here (e.g. for Warp Pots).

                string property_name = (string)wproperty_attribute.ConstructorArguments[1].Value;
                if (prop.PropertyType != typeof(int))
                    continue;
                if (!property_name.Contains("Switch"))
                    continue;
                if (property_name.Contains("Num Switches"))
                    continue;

                int switchValue = (int)prop.GetValue(this, null);
                if (switchValue == 255)
                    continue;

                // TODO: Properly check whether the switch is in/out/both, this is just a hack.
                if (property_name.Contains("Switch to Set"))
                {
                    outSwitches.Add(switchValue);
                } else if (property_name.Contains("Switch to Check"))
                {
                    inSwitches.Add(switchValue);
                } else
                {
                    inSwitches.Add(switchValue);
                    outSwitches.Add(switchValue);
                }
            }

            UsedInSwitches = inSwitches;
            UsedOutSwitches = outSwitches;
        }

        protected virtual void VisibleDOMNode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Clear the cache of which switches this object uses any time any of its properties are changed so the list is recalculated.
            // Most properties don't affect the switch list, but properties don't change that often so erring on the safe side is fine.
            UsedInSwitches = null;
            UsedOutSwitches = null;

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
            Matrix4 trs = Matrix4.CreateScale(VisualScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);

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
			Vector3 lScale = VisualScale;
			float largestMax = lScale[0];
			for (int i = 1; i < 3; i++)
				if (lScale[i] > largestMax)
					largestMax = lScale[i];

			float boundingSphere = 86f; // Default Editor Cube
			if (m_actorMeshes.Count > 0)
				boundingSphere = m_actorMeshes[0].BoundingSphere.Radius;
            else if (m_objRender != null)
                boundingSphere = m_objRender.GetAABB().Extents.Length;
            return largestMax * boundingSphere;
		}
		#endregion
	}
}
