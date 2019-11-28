using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WindEditor.Minitors;

namespace WindEditor
{
	public partial class tag_md_cb
	{
        private MessageReference m_MessageReference;

        [WProperty("Obj Paper", "Message ID", true, "The ID of the message to be displayed when the actor is interacted with.")]
        public MessageReference MessageReference
        {
            get { return m_MessageReference; }
            set
            {
                if (value != m_MessageReference)
                {
                    m_MessageReference = value;
                    OnPropertyChanged("MessageReference");
                }
            }
        }

        public override void PostLoad()
		{
			base.PostLoad();

            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f), true);

            MessageReference = new MessageReference((ushort)MessageID);
		}

		public override void PreSave()
		{
            MessageID = MessageReference.MessageID;
        }

        public override void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }
    }
}
