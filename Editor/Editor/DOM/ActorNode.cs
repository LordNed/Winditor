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
        public string FourCC { get; internal set; }
        public MapLayer Layer { get; set; }

        public List<IPropertyValue> Properties { get; }
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

        public override AABox GetBoundingBox()
        {
            AABox modelABB = m_objRender != null ? m_objRender.GetAABB() : new AABox(-Vector3.One * 50, Vector3.One * 50);
            modelABB.ScaleBy(Transform.LocalScale);

            return new AABox(modelABB.Min + Transform.Position, modelABB.Max + Transform.Position);
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
            return Transform.Position;
        }

        float IRenderable.GetBoundingRadius()
        {
            Vector3 lScale = Transform.LocalScale;
            float largestMax = lScale[0];
            for (int i = 1; i < 3; i++)
                if (lScale[i] > largestMax)
                    largestMax = lScale[i];

            return largestMax * 5f; // Undersize it for now.
        }
        #endregion
    }
}
