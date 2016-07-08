using JStudio.J3D;
using OpenTK;
using System;
using System.Collections.Generic;

namespace WindEditor
{
    [Flags]
    public enum ActorFlags
    {
        None = 0,
        Selected = 1,
    }

    public enum MapLayer
    {
        Default,
        Layer0,
        Layer1,
        Layer2,
        Layer3,
        Layer4,
        Layer5,
        Layer6,
        Layer7,
        Layer8,
        Layer9,
        LayerA,
        LayerB,
    }

    public class WActorNode : WDOMNode, IRenderable
    {
        public string FourCC { get; protected set; }
        public MapLayer Layer { get; set; }

        public List<IPropertyValue> Properties { get; protected set; }
        public ActorFlags Flags { get; set; }

        private SimpleObjRenderer m_objRender;
        private J3D m_actorMesh;

        public WActorNode(string fourCC)
        {
            Properties = new List<IPropertyValue>();
            FourCC = fourCC;
        }

        public void PostFinishedLoad()
        {
            if (FourCC == "ACTR" || FourCC == "SCOB" || FourCC == "TGOB")
            {
                IPropertyValue propVal = Properties.Find(x => x.Name == "Name");
                if (propVal != null)
                {
                    TStringPropertyValue stringProperty = (TStringPropertyValue)propVal;
                    m_actorMesh = WResourceManager.LoadActorByName((string)stringProperty.GetValue());
                }

            }

            if (m_actorMesh == null)
            {
                Obj objRef = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj");
                m_objRender = new SimpleObjRenderer(objRef);
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
            if (m_actorMesh != null)
                return m_actorMesh.Raycast(ray, out closestDistance, true);
            else
                return WMath.RayIntersectsAABB(ray, m_objRender.GetAABB().Min, m_objRender.GetAABB().Max, out closestDistance);
        }

        #region IRenderable
        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddOpaqueMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            Matrix4 trs = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);

            if (m_actorMesh != null)
                m_actorMesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
            else
                m_objRender.Render(view.ViewMatrix, view.ProjMatrix, trs);
        }

        Vector3 IRenderable.GetPosition()
        {
            Vector3 modelOffset = Vector3.Zero;
            if (m_actorMesh != null)
                modelOffset += m_actorMesh.BoundingBox.Center;

            return Transform.Position + modelOffset;
        }

        float IRenderable.GetBoundingRadius()
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
