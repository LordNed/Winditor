using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class item
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
            switch (ItemID)
            {
                case ItemID.Heart:
                    m_actorMeshes = WResourceManager.LoadActorResource("Heart Pickup");
                    break;
                case ItemID.Green_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Green Rupee");
                    break;
                case ItemID.Blue_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue Rupee");
                    break;
                case ItemID.Yellow_Rupee:
                case ItemID.Yellow_Rupee_Joke_Message:
                    m_actorMeshes = WResourceManager.LoadActorResource("Yellow Rupee");
                    break;
                case ItemID.Red_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Red Rupee");
                    break;
                case ItemID.Purple_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Purple Rupee");
                    break;
                case ItemID.Orange_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Orange Rupee");
                    break;
                case ItemID.Piece_of_Heart:
                case ItemID.Piece_of_Heart_Alternate_Message:
                    m_actorMeshes = WResourceManager.LoadActorResource("Piece of Heart");
                    break;
                case ItemID.Heart_Container:
                    m_actorMeshes = WResourceManager.LoadActorResource("Heart Container");
                    break;
                case ItemID.Silver_Rupee:
                    m_actorMeshes = WResourceManager.LoadActorResource("Silver Rupee");
                    break;
                case ItemID.Small_Key:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Key");
                    break;
                case ItemID.Fairy:
                    m_actorMeshes = WResourceManager.LoadActorResource("Recovery Fairy");
                    break;
                case ItemID.Small_Magic_Jar:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Magic Jar");
                    break;
                case ItemID.Large_Magic_Jar:
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Magic Jar");
                    break;
                case ItemID.Bombs_5:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case ItemID.Bombs_10:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case ItemID.Bombs_20:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case ItemID.Bombs_30:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Pickup");
                    break;
                case ItemID.Arrows_10:
                    m_actorMeshes = WResourceManager.LoadActorResource("Arrows Pickup");
                    break;
                case ItemID.Arrows_20:
                    m_actorMeshes = WResourceManager.LoadActorResource("Arrows Pickup");
                    break;
                case ItemID.Arrows_30:
                    m_actorMeshes = WResourceManager.LoadActorResource("Arrows Pickup");
                    break;
                case ItemID.Knights_Crest:
                    m_actorMeshes = WResourceManager.LoadActorResource("Knights Crest");
                    break;
                case ItemID.Joy_Pendant:
                    m_actorMeshes = WResourceManager.LoadActorResource("Joy Pendant");
                    break;
                case ItemID.Recovered_Heros_Sword:
                    m_actorMeshes = WResourceManager.LoadActorResource("Recovered Hero's Sword");
                    break;
                case ItemID.Golden_Feather:
                    m_actorMeshes = WResourceManager.LoadActorResource("Golden Feather");
                    break;
                case ItemID.Boko_Baba_Seed:
                    m_actorMeshes = WResourceManager.LoadActorResource("Boko Baba Seed");
                    break;
                case ItemID.Skull_Necklace:
                    m_actorMeshes = WResourceManager.LoadActorResource("Skull Necklace");
                    break;
                case ItemID.Green_Chu_Jelly:
                    m_actorMeshes = WResourceManager.LoadActorResource("Green Chu Blob");
                    break;
                case ItemID.Blue_Chu_Jelly:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue Chu Blob");
                    break;
                case ItemID.Red_Chu_Jelly:
                    m_actorMeshes = WResourceManager.LoadActorResource("Red Chu Blob");
                    break;
                case ItemID.Bombs:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bombs Ability");
                    break;
                case ItemID.Big_Key:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Key");
                    break;
                case ItemID.Town_Flower:
                    m_actorMeshes = WResourceManager.LoadActorResource("Town Flower");
                    break;
                case ItemID.Sea_Flower:
                    m_actorMeshes = WResourceManager.LoadActorResource("Sea Flower");
                    break;
                case ItemID.Exotic_Flower:
                    m_actorMeshes = WResourceManager.LoadActorResource("Exotic Flower");
                    break;
                case ItemID.Heros_Flag:
                    m_actorMeshes = WResourceManager.LoadActorResource("Hero's Flag");
                    break;
                case ItemID.Big_Catch_Flag:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Catch Flag");
                    break;
                case ItemID.Big_Sale_Flag:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Sale Flag");
                    break;
                case ItemID.Pinwheel:
                    m_actorMeshes = WResourceManager.LoadActorResource("Pinwheel");
                    break;
                case ItemID.Sickle_Moon_Flag:
                    m_actorMeshes = WResourceManager.LoadActorResource("Sickle Moon Flag");
                    break;
                case ItemID.Skull_Tower_Idol:
                    m_actorMeshes = WResourceManager.LoadActorResource("Skull Tower Idol");
                    break;
                case ItemID.Fountain_Idol:
                    m_actorMeshes = WResourceManager.LoadActorResource("Fountain Idol");
                    break;
                case ItemID.Postman_Statue:
                    m_actorMeshes = WResourceManager.LoadActorResource("Postman Statue");
                    break;
                case ItemID.Shop_Guru_Statue:
                    m_actorMeshes = WResourceManager.LoadActorResource("Shop Guru Statue");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
    }
}
