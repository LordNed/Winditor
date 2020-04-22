using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;
using JStudio.J3D;

namespace WindEditor
{
    public partial class bridge
    {
        protected J3D m_plankMesh;

        public override void PostLoad()
        {
            base.PostLoad();
            UpdateModel();
        }

        public override void PreSave()
        {

        }

        protected int ReadTypeBit(byte mask)
        {
            int bitShift = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    bitShift = i;
                    break;
                }
            }

            int bitfieldVal = TypeBitfield == 0xFF ? 0 : TypeBitfield;
            return ((bitfieldVal & mask) >> bitShift);
        }

        protected void WriteTypeBit(byte mask, int value)
        {
            int bitShift = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    bitShift = i;
                    break;
                }
            }

            int bitfieldVal = TypeBitfield == 0xFF ? 0 : TypeBitfield;
            TypeBitfield = (bitfieldVal & ~mask | (value << bitShift & mask));
        }

        public enum PlankTypeEnum
        {
            Plain_Wood = 0,
            Reinforced_Wood = 1,
        }

        [WProperty("Plank Bridge", "Plank Type", true, "", SourceScene.Room)]
        public PlankTypeEnum PlankType
        {
            get
            {
                return (PlankTypeEnum)ReadTypeBit(0x01);
            }

            set
            {
                WriteTypeBit(0x01, (int)value);
                OnPropertyChanged("PlankType");
                UpdateModel();
            }
        }

        [WProperty("Plank Bridge", "Is Half Bridge", true, "", SourceScene.Room)]
        public bool IsHalfBridge
        {
            get
            {
                return ReadTypeBit(0x02) != 0;
            }

            set
            {
                WriteTypeBit(0x02, value ? 1 : 0);
                OnPropertyChanged("IsHalfBridge");
            }
        }

        [WProperty("Plank Bridge", "Ropeless", true, "If this is checked, the bridge will not have ropes holding it up.\nIts plank type will also be forced to be Reinforced, regardless of the \"Plank Type\" param.", SourceScene.Room)]
        public bool Ropeless
        {
            get
            {
                return ReadTypeBit(0x04) != 0;
            }

            set
            {
                WriteTypeBit(0x04, value ? 1 : 0);
                OnPropertyChanged("Ropeless");
                UpdateModel();
            }
        }

        public enum RopeTypeEnum
        {
            Cuttable_Ropes = 0,
            Indestructible_Ropes = 1,
        }

        [WProperty("Plank Bridge", "Rope Type", true, "", SourceScene.Room)]
        public RopeTypeEnum RopeType
        {
            get
            {
                return (RopeTypeEnum)ReadTypeBit(0x08);
            }

            set
            {
                WriteTypeBit(0x08, (int)value);
                OnPropertyChanged("RopeType");
            }
        }

        private void UpdateModel()
        {
            List<J3D> meshes = null;
            if (PlankType == PlankTypeEnum.Reinforced_Wood || Ropeless)
                meshes = WResourceManager.LoadActorResource("Reinforced Bridge Plank");
            else
                meshes = WResourceManager.LoadActorResource("Plain Bridge Plank");

            if (meshes != null && meshes.Count > 0)
                m_plankMesh = meshes[0];
        }

        public override void Draw(WSceneView view)
        {
            base.Draw(view); // Draw the default editor cube to represent the bridge entity itself so that can still be clicked.

            if (m_plankMesh == null || Path == null)
                return;
            var points = Path.GetPoints();
            if (points.Count < 2)
                return;

            for (int i = 0; i < 4; i++)
            {
                if (ColorOverrides.ColorsEnabled[i])
                    m_plankMesh.SetTevColorOverride(i, ColorOverrides.Colors[i]);

                if (ColorOverrides.ConstColorsEnabled[i])
                    m_plankMesh.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
            }

            if (IsSelected)
                m_plankMesh.Tick(1 / (float)60);

            Vector3 p1 = points[0].Transform.Position;
            Vector3 p2 = points[1].Transform.Position;
            Vector3 bridge_delta = p2 - p1;

            float spacing_multiplier = 47f;
            if (bridge_delta.Length > 1300.0)
                spacing_multiplier += 3f;
            int num_planks = (int)(bridge_delta.Length / (1.5 * spacing_multiplier));
            Vector3 plank_delta = bridge_delta / (num_planks - 1);

            float y_rot = (float)Math.Atan2(bridge_delta.X, bridge_delta.Z);
            Quaternion yRot = Quaternion.FromAxisAngle(Vector3.UnitY, y_rot);
            Vector3 plank_delta_xz = Vector3.Transform(plank_delta, yRot.Inverted());
            float x_rot = (float)-Math.Atan2(plank_delta_xz.Y, plank_delta_xz.Z);
            Quaternion xRot = Quaternion.FromAxisAngle(Vector3.UnitX, x_rot);
            Quaternion plankRotation = yRot * xRot;

            Vector3 plankScale = new Vector3(1.0f, 1.0f, 1.5f);

            for (int plank_i = 0; plank_i < num_planks; plank_i++)
            {
                Vector3 plankPosition = p1 + (plank_delta * plank_i);

                Matrix4 trs = Matrix4.CreateScale(plankScale) * Matrix4.CreateFromQuaternion(plankRotation) * Matrix4.CreateTranslation(plankPosition);

                m_plankMesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
            }
        }
        public override float GetBoundingRadius()
        {
            float radius = base.GetBoundingRadius();

            if (m_plankMesh == null || Path == null)
                return radius;
            var points = Path.GetPoints();
            if (points.Count < 2)
                return radius;

            for (int pathPointIndex = 0; pathPointIndex < 2; pathPointIndex++)
            {
                if (pathPointIndex >= points.Count)
                    break;
                PathPoint_v2 point = points[pathPointIndex];

                float dist = (Transform.Position - point.Transform.Position).Length;
                if (dist > radius)
                    radius = dist;
            }

            return radius;
        }
    }
}
