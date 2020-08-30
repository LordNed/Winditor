using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WindEditor.Minitors;

namespace WindEditor
{
	public partial class obj_paper
	{
        private MessageReference m_MessageReference;

        [WProperty("obj_paper", "Message ID", true, "The ID of the message to be displayed when the actor is interacted with.")]
        public MessageReference MessageReference
        {
            get { return m_MessageReference; }
            set
            {
                if (value != m_MessageReference)
                {
                    m_MessageReference = value;
                    MessageID = MessageReference.MessageID;
                    OnPropertyChanged("MessageReference");
                }
            }
        }

		public override void PostLoad()
		{
			switch (Type)
            {
                case TypeEnum.Normal_Papers:
                    m_actorMeshes = WResourceManager.LoadActorResource("Simple Papers");
                    break;
                case TypeEnum.Ornate_Papers:
                    m_actorMeshes = WResourceManager.LoadActorResource("Ornate Papers");
                    break;
                case TypeEnum.Stone:
                    m_actorMeshes = WResourceManager.LoadActorResource("Stone Tablet");
                    break;
            }

            MessageReference = new MessageReference((ushort)MessageID);
		}

		public override void PreSave()
		{
            MessageID = MessageReference.MessageID;
		}
	}
}
