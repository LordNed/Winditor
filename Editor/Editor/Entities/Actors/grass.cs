using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace WindEditor
{
    public partial class grass
    {
        #region Constants
        private static readonly int[] kFoliageSpawnPatternGroupIndexes = {
            0,
            0,
            1,
            2,
            3,
            4,
            5,
            6,
        };
        private static readonly int[] kFoliageSpawnPatternNumModels = {
            1,
            7,
            21,
            3,
            7,
            17,
            7,
            5,
        };

        private static readonly Vector3[][] kGrassSpawnOffsets = new Vector3[][] {
            new Vector3[] {
                new Vector3(0, 0, 0),
                new Vector3(3, 0, -50),
                new Vector3(-2, 0, 50),
                new Vector3(50, 0, 27),
                new Vector3(52, 0, -25),
                new Vector3(-50, 0, 22),
                new Vector3(-50, 0, -29),
            },
            new Vector3[] {
                new Vector3(-18, 0, 76),
                new Vector3(-15, 0, 26),
                new Vector3(133, 0, 0),
                new Vector3(80, 0, 23),
                new Vector3(86, 0, -83),
                new Vector3(33, 0, -56),
                new Vector3(83, 0, -27),
                new Vector3(-120, 0, -26),
                new Vector3(-18, 0, -65),
                new Vector3(-20, 0, -21),
                new Vector3(-73, 0, 1),
                new Vector3(-67, 0, -102),
                new Vector3(-21, 0, 126),
                new Vector3(-120, 0, -78),
                new Vector3(-70, 0, -49),
                new Vector3(32, 0, 103),
                new Vector3(34, 0, 51),
                new Vector3(-72, 0, 98),
                new Vector3(-68, 0, 47),
                new Vector3(33, 0, -5),
                new Vector3(135, 0, -53),
            },
            new Vector3[] {
                new Vector3(-75, 0, -50),
                new Vector3(75, 0, -25),
                new Vector3(14, 0, 106),
            },
            new Vector3[] {
                new Vector3(-24, 0, -28),
                new Vector3(27, 0, -28),
                new Vector3(-21, 0, 33),
                new Vector3(-18, 0, -34),
                new Vector3(44, 0, -4),
                new Vector3(41, 0, 10),
                new Vector3(24, 0, 39),
            },
            new Vector3[] {
                new Vector3(-55, 0, -22),
                new Vector3(-28, 0, -50),
                new Vector3(-77, 0, 11),
                new Vector3(55, 0, -44),
                new Vector3(83, 0, -71),
                new Vector3(11, 0, -48),
                new Vector3(97, 0, -34),
                new Vector3(-74, 0, -57),
                new Vector3(31, 0, 58),
                new Vector3(59, 0, 30),
                new Vector3(13, 0, 23),
                new Vector3(-12, 0, 54),
                new Vector3(55, 0, 97),
                new Vector3(10, 0, 92),
                new Vector3(33, 0, -10),
                new Vector3(-99, 0, -27),
                new Vector3(40, 0, -87),
            },
            new Vector3[] {
                new Vector3(0, 0, 3),
                new Vector3(-26, 0, -29),
                new Vector3(7, 0, -25),
                new Vector3(31, 0, -5),
                new Vector3(-7, 0, 40),
                new Vector3(-35, 0, 15),
                new Vector3(23, 0, 32),
            },
            new Vector3[] {
                new Vector3(-40, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(80, 0, 0),
                new Vector3(-80, 0, 0),
                new Vector3(40, 0, 0),
            }
        };
        #endregion

        public override void PostLoad()
        {
            UpdateModel();
        }

        public override void PreSave()
        {

        }

        private void UpdateModel()
        {
            WDOMNode cur_object = this;
            while (cur_object.Parent != null)
            {
                cur_object = cur_object.Parent;
            }
            WRoom currRoom = cur_object as WRoom;
            string currStageName = World.Map.MapName;

            string model_path;
            switch (Type)
            {
                case TypeEnum.Grass:
                    if (currStageName.StartsWith("kin") || currStageName == "Xboss1")
                    {
                        // Forbidden Woods grass
                        model_path = "resources/models/foliage/grass_Vmori.obj";
                    }
                    else
                    {
                        model_path = "resources/models/foliage/grass.obj";
                    }
                    break;
                case TypeEnum.Tree:
                    model_path = "resources/models/foliage/tree.obj";
                    break;
                case TypeEnum.White_Flower:
                    model_path = "resources/models/foliage/flower.obj";
                    break;
                case TypeEnum.Pink_Flower:
                    if (currStageName == "sea" && currRoom.RoomIndex == 33)
                    {
                        // Private Oasis flowers
                        model_path = "resources/models/foliage/pflower_oasis.obj";
                    }
                    else
                    {
                        model_path = "resources/models/foliage/pflower_pink.obj";
                    }
                    break;
                default:
                    model_path = "resources/editor/EditorCube.obj";
                    break;
            }

            m_objRender = WResourceManager.LoadObjResource(model_path, new OpenTK.Vector4(1, 1, 1, 1), true, false, TextureWrapMode.MirroredRepeat);
        }

        public override void Draw(WSceneView view)
        {
            var bbox = GetBoundingBox();
            m_world.DebugDrawBox(bbox.Center, (bbox.Max - bbox.Min) / 2, Transform.Rotation.ToSinglePrecision(), (Flags & NodeFlags.Selected) == NodeFlags.Selected ? WLinearColor.White : WLinearColor.Black, 0, 0);

            int spawnPatternIndex = (int)SpawnPattern;
            int groupIndex = kFoliageSpawnPatternGroupIndexes[spawnPatternIndex];
            int numModels = kFoliageSpawnPatternNumModels[spawnPatternIndex];
            Vector3[] offsetsForGroup = kGrassSpawnOffsets[groupIndex];

            for (int i = 0; i < numModels; i++)
            {
                Vector3 modelOffset = offsetsForGroup[i];

                WTransform transform = new WTransform();
                
                if (Type == TypeEnum.Tree)
                {
                    // Only trees rotate, and they only rotate the positions of each tree model around the entity's center, not the individual trees themselves.
                    Quaternion rotation = Quaternion.Identity.FromEulerAngles(new Vector3(0, Transform.Rotation.ToEulerAngles().Y, 0));
                    modelOffset = Vector3.Transform(modelOffset, rotation);
                }

                transform.Position = Transform.Position + modelOffset;

                Matrix4 trs = Matrix4.CreateScale(transform.LocalScale) * Matrix4.CreateFromQuaternion(transform.Rotation.ToSinglePrecision()) * Matrix4.CreateTranslation(transform.Position);

                m_objRender.Render(view.ViewMatrix, view.ProjMatrix, trs);
            }
        }

        public override float GetBoundingRadius()
        {
            return 200f;
        }
    }
}