using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using SuperBMDLib.Materials.Enums;

namespace SuperBMDLib.Materials
{
    public struct IndirectTevStage : IEquatable<IndirectTevStage>
    {
        public TevStageId TevStage;
        public IndirectFormat IndTexFormat;
        public IndirectBias IndTexBiasSel;
        public IndirectMatrix IndTexMtxId;
        public IndirectWrap IndTexWrapS;
        public IndirectWrap IndTexWrapT;
        public bool AddPrev;
        public bool UtcLod;
        public IndirectAlpha AlphaSel;

        public IndirectTevStage(TevStageId stageId, IndirectFormat format, IndirectBias bias, IndirectMatrix matrixId, IndirectWrap wrapS, IndirectWrap wrapT, bool addPrev, bool utcLod, IndirectAlpha alphaSel)
        {
            TevStage = stageId;
            IndTexFormat = format;
            IndTexBiasSel = bias;
            IndTexMtxId = matrixId;
            IndTexWrapS = wrapS;
            IndTexWrapT = wrapT;
            AddPrev = addPrev;
            UtcLod = utcLod;
            AlphaSel = alphaSel;
        }

        public IndirectTevStage(EndianBinaryReader reader)
        {
            TevStage = (TevStageId)reader.ReadByte();
            IndTexFormat = (IndirectFormat)reader.ReadByte();
            IndTexBiasSel = (IndirectBias)reader.ReadByte();
            IndTexMtxId = (IndirectMatrix)reader.ReadByte();
            IndTexWrapS = (IndirectWrap)reader.ReadByte();
            IndTexWrapT = (IndirectWrap)reader.ReadByte();
            AddPrev = reader.ReadBoolean();
            UtcLod = reader.ReadBoolean();
            AlphaSel = (IndirectAlpha)reader.ReadByte();
            reader.Skip(3);
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write((byte)TevStage);
            writer.Write((byte)IndTexFormat);
            writer.Write((byte)IndTexBiasSel);
            writer.Write((byte)IndTexMtxId);
            writer.Write((byte)IndTexWrapS);
            writer.Write((byte)IndTexWrapT);
            writer.Write(AddPrev);
            writer.Write(UtcLod);
            writer.Write((byte)AlphaSel);

            writer.Write(new byte[] { 0xFF, 0xFF, 0xFF });
        }

        public static bool operator ==(IndirectTevStage left, IndirectTevStage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IndirectTevStage left, IndirectTevStage right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = (int)TevStage;
            hash ^= (int)IndTexFormat << 4;
            hash ^= (int)IndTexBiasSel << 3;
            hash ^= (int)IndTexMtxId << 4;
            hash ^= (int)IndTexWrapS << 5;
            hash ^= (int)IndTexWrapT << 2;
            hash ^= Convert.ToInt32(AddPrev);
            hash ^= Convert.ToInt32(UtcLod);
            hash ^= (int)AlphaSel << 7;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IndirectTevStage))
                return false;
            else
                return Equals((IndirectTevStage)obj);
        }

        public bool Equals(IndirectTevStage other)
        {
            return TevStage == other.TevStage &&
                IndTexFormat == other.IndTexFormat &&
                IndTexBiasSel == other.IndTexBiasSel &&
                IndTexMtxId == other.IndTexMtxId &&
                IndTexWrapS == other.IndTexWrapS &&
                IndTexWrapT == other.IndTexWrapT &&
                AddPrev == other.AddPrev &&
                UtcLod == other.UtcLod &&
                AlphaSel == other.AlphaSel;
        }
    }
}
