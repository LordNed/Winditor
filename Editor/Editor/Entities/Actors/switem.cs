using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class switem
	{
		public override void PostLoad()
		{
			UpdateModel();
			base.PostLoad();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			m_actorMeshes.Clear();
			m_objRender = null;
			switch (SpawnedItem)
			{
                case DroppedItemID.Heart:
                    m_actorMeshes = WResourceManager.LoadActorResource("Heart Pickup");
                    break;
                case DroppedItemID.Green_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Green Rupee");
                    break;
                case DroppedItemID.Blue_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue Rupee");
                    break;
                case DroppedItemID.Yellow_Rupee:
                case DroppedItemID.Yellow_Rupee_Joke_Message:
                    m_actorMeshes = WResourceManager.LoadActorResource("Yellow Rupee");
                    break;
                case DroppedItemID.Red_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Red Rupee");
                    break;
                case DroppedItemID.Purple_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Purple Rupee");
                    break;
                case DroppedItemID.Orange_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Orange Rupee");
                    break;
                case DroppedItemID.Piece_of_Heart:
                    m_actorMeshes = WResourceManager.LoadActorResource("Piece of Heart");
                    break;
                case DroppedItemID.Heart_Container:
                    m_actorMeshes = WResourceManager.LoadActorResource("Heart Container");
                    break;
                case DroppedItemID.Silver_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Silver Rupee");
                    break;
                case DroppedItemID.Small_Key:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Key");
                    break;
                case DroppedItemID.Fairy:
                    m_actorMeshes = WResourceManager.LoadActorResource("Recovery Fairy");
                    break;
                case DroppedItemID.Small_Magic_Jar:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Magic Jar");
                    break;
                case DroppedItemID.Large_Magic_Jar:
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Magic Jar");
                    break;
                case DroppedItemID.Bombs_5:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case DroppedItemID.Bombs_10:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case DroppedItemID.Bombs_20:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case DroppedItemID.Bombs_30:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case DroppedItemID.Arrows_10:
                    m_actorMeshes = WResourceManager.LoadActorResource("Arrows Pickup");
                    break;
                case DroppedItemID.Arrows_20:
                    m_actorMeshes = WResourceManager.LoadActorResource("Arrows Pickup");
                    break;
                case DroppedItemID.Arrows_30:
                    m_actorMeshes = WResourceManager.LoadActorResource("Arrows Pickup");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
		}
	}
}
