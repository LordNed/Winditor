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

    public class WActorNode : WDOMNode
    {
        public List<IPropertyValue> Properties { get; }
        public MapLayer Layer { get; set; }
        public ActorFlags Flags { get; set; }

        private SimpleObjRenderer m_objRender;

        public WActorNode()
        {
            Properties = new List<IPropertyValue>();

            var obj = new Obj();
            obj.Load("resources/editor/EditorCube.obj");
            m_objRender = new SimpleObjRenderer(obj);
        }

        public override void Render(WSceneView view)
        {
            base.Render(view);

            Matrix4 trs = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);

            m_objRender.Render(view.ViewMatrix, view.ProjMatrix, trs);
        }

        public override AABox GetBoundingBox()
        {
            AABox modelABB = m_objRender.GetAABB();
            modelABB.ScaleBy(Transform.LocalScale);

            return new AABox(modelABB.Min + Transform.Position, modelABB.Max + Transform.Position);
        }
    }
}
