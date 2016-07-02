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

        public WActorNode(string fourCC)
        {
            Properties = new List<IPropertyValue>();
            FourCC = fourCC;

            Obj objRef = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj");
            m_objRender = new SimpleObjRenderer(objRef);

            //var obj = new Obj();
            //obj.Load("resources/editor/EditorCube.obj");
            //m_objRender = new SimpleObjRenderer(obj);
        }

        public override AABox GetBoundingBox()
        {
            AABox modelABB = m_objRender.GetAABB();
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
