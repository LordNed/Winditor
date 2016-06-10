using GameFormatReader.Common;
using OpenTK;
using System;
using System.Collections.Generic;

namespace J3DRenderer.JStudio.Animation
{
    /// <summary>
    /// The BTK format represents a material animation that changes the tex coords over time.
    /// </summary>
    class BTK
    {
        struct AnimIndex
        {
            public ushort Count;
            public ushort Index;
            public ushort Unknown0;
        }

        struct AnimComponent
        {
            public AnimIndex Scale;
            public AnimIndex Rotation;
            public AnimIndex Translation;
        }

        public string Magic { get; private set; }
        public string StudioType { get; private set; }
        public LoopType LoopMode { get; private set; }
        public byte AngleMultiplier { get; private set; }
        public short AnimLength { get; private set; }

        public void LoadFromStream(EndianBinaryReader reader)
        {
            // Read the J3D Header
            Magic = new string(reader.ReadChars(4)); // "J3D1"
            StudioType = new string(reader.ReadChars(4)); // btk1

            int fileSize = reader.ReadInt32();
            int tagCount = reader.ReadInt32();

            // Skip over an unused space.
            reader.Skip(16);

            LoadTagDataFromFile(reader, tagCount);
        }

        private void LoadTagDataFromFile(EndianBinaryReader reader, int tagCount)
        {
            for (int i = 0; i < tagCount; i++)
            {
                long tagStart = reader.BaseStream.Position;

                string tagName = reader.ReadString(4);
                int tagSize = reader.ReadInt32();

                switch (tagName)
                {
                    case "TTK1":
                        LoopMode = (LoopType)reader.ReadByte(); // 0 = Play Once. 2 = Loop (Assumed from BCK)
                        AngleMultiplier = reader.ReadByte(); // Multiply Angle Value by pow(2, angleMultiplier) (Assumed from BCK)
                        AnimLength = reader.ReadInt16();
                        short textureAnimEntryCount = (short)(reader.ReadInt16() / 3); // 3 for each material. BTK stores U, V, and W separately, so you need to divide by three.
                        short numScaleFloatEntries = reader.ReadInt16();
                        short numRotationShortEntries = reader.ReadInt16();
                        short numTranslateFloatEntries = reader.ReadInt16();
                        int animDataOffset = reader.ReadInt32();
                        int remapTableOffset = reader.ReadInt32();
                        int stringTableOffset = reader.ReadInt32();
                        int textureIndexTableOffset = reader.ReadInt32();
                        int textureCenterTableOffset = reader.ReadInt32();
                        int scaleDataOffset = reader.ReadInt32();
                        int rotationDataOffset = reader.ReadInt32();
                        int translateDataOffset = reader.ReadInt32();

                        // Read array of scale data
                        float[] scaleData = new float[numScaleFloatEntries];
                        reader.BaseStream.Position = tagStart + scaleDataOffset;
                        for (int j = 0; j < numScaleFloatEntries; j++)
                            scaleData[j] = reader.ReadSingle();

                        // Read array of rotation data (but don't convert it)
                        float[] rotationData = new float[numRotationShortEntries];
                        reader.BaseStream.Position = tagStart + rotationDataOffset;
                        for (int j = 0; j < numRotationShortEntries; j++)
                            rotationData[j] = reader.ReadInt16();

                        // Read array of translation/position data
                        float[] translationData = new float[numTranslateFloatEntries];
                        reader.BaseStream.Position = tagStart + translateDataOffset;
                        for (int j = 0; j < numTranslateFloatEntries; j++)
                            translationData[j] = reader.ReadSingle();

                        // Remap Table (probably matches MAT3's remap table?)
                        short[] remapTable = new short[textureAnimEntryCount];
                        reader.BaseStream.Position = tagStart + remapTableOffset;
                        for (int j = 0; j < textureAnimEntryCount; j++)
                            remapTable[j] = reader.ReadInt16();

                        // String Table which gives us material names.
                        reader.BaseStream.Position = tagStart + stringTableOffset;
                        StringTable stringTable = StringTable.FromStream(reader);

                        // Texture Index table which tells us which texture index of this material to modify (?)
                        byte[] textureIndexTable = new byte[textureAnimEntryCount];
                        reader.BaseStream.Position = tagStart + textureIndexTableOffset;
                        for (int j = 0; j < textureAnimEntryCount; j++)
                            textureIndexTable[j] = reader.ReadByte();

                        // Texture Offsets
                        Vector3[] textureOffsets = new Vector3[textureAnimEntryCount];
                        reader.BaseStream.Position = tagStart + textureCenterTableOffset;
                        for (int j = 0; j < textureAnimEntryCount; j++)
                            textureOffsets[j] = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                        // Read the data for each joint that this animation.
                        //AnimationData = new List<JointAnim>();
                        float rotScale = (float)Math.Pow(2f, AngleMultiplier) * (180 / 32768f);

                        reader.BaseStream.Position = tagStart + animDataOffset;
                        for (int j = 0; j < textureAnimEntryCount; j++)
                        {
                            AnimComponent texU = ReadAnimComponent(reader);
                            AnimComponent texV = ReadAnimComponent(reader);
                            AnimComponent texW = ReadAnimComponent(reader);
                        }
                        break;
                }
            }
        }

        private AnimIndex ReadAnimIndex(EndianBinaryReader stream)
        {
            return new AnimIndex { Count = stream.ReadUInt16(), Index = stream.ReadUInt16(), Unknown0 = stream.ReadUInt16() };
        }

        private AnimComponent ReadAnimComponent(EndianBinaryReader stream)
        {
            return new AnimComponent { Scale = ReadAnimIndex(stream), Rotation = ReadAnimIndex(stream), Translation = ReadAnimIndex(stream) };
        }
    }
}
