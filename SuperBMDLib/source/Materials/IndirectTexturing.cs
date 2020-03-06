using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using SuperBMDLib.Materials.Enums;

namespace SuperBMDLib.Materials
{
    public class IndirectTexturing
    {
        /// <summary>
        /// Determines if an indirect texture lookup is to take place
        /// </summary>
        public bool HasLookup;
        /// <summary>
        /// The number of indirect texturing stages to use
        /// </summary>
        public byte IndTexStageNum;

        public IndirectTevOrder[] TevOrders;

        /// <summary>
        /// The dynamic 2x3 matrices to use when transforming the texture coordinates
        /// </summary>
        public IndirectTexMatrix[] Matrices;
        /// <summary>
        /// U and V scales to use when transforming the texture coordinates
        /// </summary>
        public IndirectTexScale[] Scales;
        /// <summary>
        /// Instructions for setting up the specified TEV stage for lookup operations
        /// </summary>
        public IndirectTevStage[] TevStages;

        public IndirectTexturing()
        {
            HasLookup = false;
            IndTexStageNum = 0;

            TevOrders = new IndirectTevOrder[4];
            for (int i = 0; i < 4; i++)
                TevOrders[i] = new IndirectTevOrder(TexCoordId.Null, TexMapId.Null);

            Matrices = new IndirectTexMatrix[3];
            for (int i = 0; i < 3; i++)
                Matrices[i] = new IndirectTexMatrix(new OpenTK.Matrix2x3(0.5f, 0.0f, 0.0f, 0.0f, 0.5f, 0.0f), 1);

            Scales = new IndirectTexScale[4];
            for (int i = 0; i < 4; i++)
                Scales[i] = new IndirectTexScale(IndirectScale.ITS_1, IndirectScale.ITS_1);

            TevStages = new IndirectTevStage[16];
            for (int i = 0; i < 3; i++)
                TevStages[i] = new IndirectTevStage(
                    TevStageId.TevStage0,
                    IndirectFormat.ITF_8,
                    IndirectBias.ITB_S,
                    IndirectMatrix.ITM_OFF,
                    IndirectWrap.ITW_OFF,
                    IndirectWrap.ITW_OFF,
                    false,
                    false,
                    IndirectAlpha.ITBA_OFF
                    );
        }

        public IndirectTexturing(EndianBinaryReader reader)
        {
            HasLookup = reader.ReadBoolean();
            IndTexStageNum = reader.ReadByte();
            reader.SkipInt16();

            TevOrders = new IndirectTevOrder[8];
            for (int i = 0; i < 4; i++)
                TevOrders[i] = new IndirectTevOrder(reader);

            Matrices = new IndirectTexMatrix[3];
            for (int i = 0; i < 3; i++)
                Matrices[i] = new IndirectTexMatrix(reader);

            Scales = new IndirectTexScale[4];
            for (int i = 0; i < 4; i++)
                Scales[i] = new IndirectTexScale(reader);

            TevStages = new IndirectTevStage[16];
            for (int i = 0; i < 16; i++)
                TevStages[i] = new IndirectTevStage(reader);
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(HasLookup);
            writer.Write(IndTexStageNum);

            writer.Write((short)-1);

            for (int i = 0; i < 4; i++)
                TevOrders[i].Write(writer);

            for (int i = 0; i < 3; i++)
                Matrices[i].Write(writer);

            for (int i = 0; i < 4; i++)
                Scales[i].Write(writer);

            for (int i = 0; i < 16; i++)
                TevStages[i].Write(writer);
        }


    }
}
