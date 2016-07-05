using GameFormatReader.Common;
using JStudio.J3D.Animation;
using JStudio.J3D.ShaderGen;
using JStudio.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using WArchiveTools;
using WindEditor;

namespace JStudio.J3D
{
    public class TevColorOverride
    {
        private WLinearColor[] m_colors;
        private WLinearColor[] m_kColors;
        private bool[] m_colorsEnabled;
        private bool[] m_kColorsEnabled;

        public TevColorOverride()
        {
            m_colors = new WLinearColor[4];
            m_kColors = new WLinearColor[4];
            m_colorsEnabled = new bool[4];
            m_kColorsEnabled = new bool[4];
        }

        public void SetTevColorOverride(int index, WLinearColor overrideColor)
        {
            if (index < 0 || index >= 4)
                throw new ArgumentOutOfRangeException("index", "index must be between 0 and 3");

            m_colors[index] = overrideColor;
            m_colorsEnabled[index] = true;
        }

        public void SetTevkColorOverride(int index, WLinearColor overrideColor)
        {
            if (index < 0 || index >= 4)
                throw new ArgumentOutOfRangeException("index", "index must be between 0 and 3");

            m_kColors[index] = overrideColor;
            m_kColorsEnabled[index] = true;
        }

        public PSBlock SetPSBlockForMaterial(Material srcMaterial, ref PSBlock psBlock)
        {
            psBlock.Color0 = m_colorsEnabled[0] ? m_colors[0] : srcMaterial.TevColorIndexes[0];
            psBlock.Color1 = m_colorsEnabled[1] ? m_colors[1] : srcMaterial.TevColorIndexes[1];
            psBlock.Color2 = m_colorsEnabled[2] ? m_colors[2] : srcMaterial.TevColorIndexes[2];
            psBlock.Color3 = m_colorsEnabled[3] ? m_colors[3] : srcMaterial.TevColorIndexes[3];

            psBlock.kColor0 = m_kColorsEnabled[0] ? m_kColors[0] : srcMaterial.TevKonstColorIndexes[0];
            psBlock.kColor1 = m_kColorsEnabled[1] ? m_kColors[1] : srcMaterial.TevKonstColorIndexes[1];
            psBlock.kColor2 = m_kColorsEnabled[2] ? m_kColors[2] : srcMaterial.TevKonstColorIndexes[2];
            psBlock.kColor3 = m_kColorsEnabled[3] ? m_kColors[3] : srcMaterial.TevKonstColorIndexes[3];
            return psBlock;
        }
    }

    public partial class J3D : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Magic { get; protected set; }
        public string StudioType { get; protected set; }
        public string TotalFileSize { get { return string.Format("{0} bytes", m_totalFileSize); } }
        public FAABox BoundingBox { get; protected set; }
        public FSphere BoundingSphere { get; protected set; }

        public INF1 INF1Tag { get; protected set; }
        public VTX1 VTX1Tag { get; protected set; }
        public MAT3 MAT3Tag { get; protected set; }
        public SHP1 SHP1Tag { get; protected set; }
        public JNT1 JNT1Tag { get; protected set; }
        public TEX1 TEX1Tag { get; private set; }
        public EVP1 EVP1Tag { get; private set; }
        public DRW1 DRW1Tag { get; private set; }

        public List<BCK> BoneAnimations { get { return m_boneAnimations; } }
        public List<BTK> MaterialAnimations { get { return m_materialAnimations; } }
        public BCK CurrentBoneAnimation
        {
            get { return m_currentBoneAnimation; }
            set { SetBoneAnimation(value.Name); }
        }

        private int m_totalFileSize;

        // Hack
        private Matrix4 m_viewMatrix;
        private Matrix4 m_projMatrix;
        private Matrix4 m_modelMatrix;
        private Material m_currentBoundMat;

        private GXLight[] m_hardwareLights = new GXLight[8];
        private TevColorOverride m_tevColorOverrides;
        private int m_hardwareLightBuffer;

        private Dictionary<string, Texture> m_textureOverrides;
        private List<BCK> m_boneAnimations;
        private List<BTK> m_materialAnimations;
        private BCK m_currentBoneAnimation;
        private BTK m_currentMaterialAnimation;
        private bool m_skinningInvalid;

        public void LoadFromStream(EndianBinaryReader reader)
        {
            // Read the J3D Header
            Magic = new string(reader.ReadChars(4));
            StudioType = new string(reader.ReadChars(4));
            m_totalFileSize = reader.ReadInt32();
            int tagCount = reader.ReadInt32();

            // Skip over an unused tag ("SVR3") which is consistent in all models.
            reader.Skip(16);

            LoadTagDataFromFile(reader, tagCount);

            // Rendering Stuff
            m_hardwareLightBuffer = GL.GenBuffer();
            m_textureOverrides = new Dictionary<string, Texture>();
            m_tevColorOverrides = new TevColorOverride();
            m_boneAnimations = new List<BCK>();
            m_materialAnimations = new List<BTK>();

            // Mark this as true when we first load so it moves non-animated pieces into the right area.
            m_skinningInvalid = true;
        }

        public void SetHardwareLight(int index, GXLight light)
        {
            if (index < 0 || index >= 8)
                throw new ArgumentOutOfRangeException("index", "index must be >= 0 or < 8. Maximum of 8 hardware lights supported!");

            m_hardwareLights[index] = light;

            // Fill the buffer with data at the chosen binding point
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, (int)ShaderUniformBlockIds.LightBlock, m_hardwareLightBuffer);
            GL.BufferData(BufferTarget.UniformBuffer, (IntPtr)(GXLight.SizeInBytes * 8), m_hardwareLights, BufferUsageHint.DynamicDraw);
        }

        public void SetTevColorOverride(int index, WLinearColor overrideColor)
        {
            m_tevColorOverrides.SetTevColorOverride(index, overrideColor);
        }

        public void SetTevkColorOverride(int index, WLinearColor overrideColor)
        {
            m_tevColorOverrides.SetTevkColorOverride(index, overrideColor);
        }

        public void LoadBoneAnimation(string bckFile)
        {
            string animName = Path.GetFileNameWithoutExtension(bckFile);
            BCK bck = new BCK(animName);

            using (var reader = FileUtilities.LoadFile(bckFile))
                bck.LoadFromStream(reader);

            m_boneAnimations.Add(bck);

            OnPropertyChanged("BoneAnimations");
        }

        public void LoadMaterialAnim(string btkFile)
        {
            string animName = Path.GetFileNameWithoutExtension(btkFile);
            BTK btk = new BTK(animName);

            using (var reader = FileUtilities.LoadFile(btkFile))
                btk.LoadFromStream(reader);

            m_materialAnimations.Add(btk);

            OnPropertyChanged("MaterialAnimations");
        }

        public void SetBoneAnimation(string animName)
        {
            BCK anim = m_boneAnimations.Find(x => x.Name == animName);
            if (anim == null)
            {
                Console.WriteLine("Failed to play animation {0}, animation not loaded!", animName);
            }

            if (m_currentBoneAnimation != null)
                m_currentBoneAnimation.Stop();

            if (anim != null)
            {
                m_currentBoneAnimation = anim;
                m_currentBoneAnimation.Start();
            }

            OnPropertyChanged("CurrentBoneAnimation");
        }

        public void SetMaterialAnimation(string animName)
        {
            BTK anim = m_materialAnimations.Find(x => x.Name == animName);
            if (anim == null)
            {
                Console.WriteLine("Failed to play animation {0}, animation not loaded!", animName);
            }

            if (m_currentMaterialAnimation != null)
                m_currentMaterialAnimation.Stop();

            if (anim != null)
            {
                m_currentMaterialAnimation = anim;
                m_currentMaterialAnimation.Start();
            }

            OnPropertyChanged("CurrentMaterialAnimation");
        }

        /// <summary>
        /// This is used to emulate a feature used in The Legend of Zelda: The Wind Waker. In WW all characters appear to share
        /// their Toon texture lookup image. The J3D files include a texture in their files which is a black/white checkerboard
        /// that is a placeholder for the one the game overrides. Unfortunately, if we use the black/white checkerboard then lighting
        /// gets broken, so this function is provided to optionally override a texture name with a specific texture, such as the
        /// texture name "ZBtoonEX" used by WW.
        /// </summary>
        /// <param name="textureName">Name of the texture to override. All textures with this name will be overriden.</param>
        /// <param name="filePath">Path to the texture to use.</param>
        public void SetTextureOverride(string textureName, string filePath)
        {
            BinaryTextureImage btiData = new BinaryTextureImage();
            btiData.LoadImageFromDisk(filePath);
            m_textureOverrides[textureName] = new Texture(textureName, btiData);
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
                    // INFO - Vertex Count, Scene Hierarchy
                    case "INF1":
                        INF1Tag = new INF1();
                        INF1Tag.LoadINF1FromStream(reader, tagStart);
                        break;
                    // VERTEX - Stores vertex arrays for pos/normal/color0/tex0 etc.
                    // Contains VertexAttributes which describe how the data is stored/laid out.
                    case "VTX1":
                        VTX1Tag = new VTX1();
                        VTX1Tag.LoadVTX1FromStream(reader, tagStart, tagSize);
                        break;
                    // ENVELOPES - Defines vertex weights for skinning
                    case "EVP1":
                        EVP1Tag = new EVP1();
                        EVP1Tag.LoadEVP1FromStream(reader, tagStart);
                        break;
                    // DRAW (Skeletal Animation Data) - Stores which matrices (?) are weighted, and which are used directly
                    case "DRW1":
                        DRW1Tag = new DRW1();
                        DRW1Tag.LoadDRW1FromStream(reader, tagStart);
                        break;
                    // JOINTS - Stores the skeletal joints (position, rotation, scale, etc...)
                    case "JNT1":
                        JNT1Tag = new JNT1();
                        JNT1Tag.LoadJNT1FromStream(reader, tagStart);
                        JNT1Tag.CalculateParentJointsForSkeleton(INF1Tag.HierarchyRoot);
                        break;
                    // SHAPE - Face/Triangle information for model.
                    case "SHP1":
                        SHP1Tag = new SHP1();
                        SHP1Tag.ReadSHP1FromStream(reader, tagStart, VTX1Tag.VertexData);
                        break;
                    // MATERIAL - Stores materials (which describes how textures, etc. are drawn)
                    case "MAT3":
                        MAT3Tag = new MAT3();
                        MAT3Tag.LoadMAT3FromStream(reader, tagStart, tagSize);
                        break;
                    // TEXTURES - Stores binary texture images.
                    case "TEX1":
                        TEX1Tag = new TEX1();
                        TEX1Tag.LoadTEX1FromStream(reader, tagStart);
                        break;
                    // MODEL - Seems to be bypass commands for Materials and invokes GX registers directly.
                    case "MDL3":
                        break;
                }

                // Skip the stream reader to the start of the next tag since it gets moved around during loading.
                reader.BaseStream.Position = tagStart + tagSize;
            }

            // To generate shaders we need to know which vertex attributes need to be enabled for the shader. However,
            // the shader has no knowledge in our book as to what attributes are enabled. Theoretically we could enable
            // them on the fly as something requested it, but that'd involve more code that I don't want to do right now.
            // To resolve, we iterate once through the hierarchy to see which mesh is called after a material and bind the
            // vertex descriptions.
            Material dummyMat = null;
            AssignVertexAttributesToMaterialsRecursive(INF1Tag.HierarchyRoot, ref dummyMat);

            // Now that the vertex attributes are assigned to the materials, generate a shader from the data.
            foreach (var material in MAT3Tag.MaterialList)
            {
                if (material.VtxDesc == null)
                {
                    Console.WriteLine("Skipping generating Shader for Unreferenced Material: {0}", material);
                    continue;
                }
                material.Shader = TEVShaderGenerator.GenerateShader(material, MAT3Tag);

                // Bind the Light Block uniform to the shader
                GL.BindBufferBase(BufferRangeTarget.UniformBuffer, (int)ShaderUniformBlockIds.LightBlock, m_hardwareLightBuffer);
                GL.UniformBlockBinding(material.Shader.Program, material.Shader.UniformLightBlock, (int)ShaderUniformBlockIds.LightBlock);

                // Bind the Pixel Shader uniform to the shader
                GL.BindBufferBase(BufferRangeTarget.UniformBuffer, (int)ShaderUniformBlockIds.PixelShaderBlock, material.Shader.PSBlockUBO);
                GL.UniformBlockBinding(material.Shader.Program, material.Shader.UniformPSBlock, (int)ShaderUniformBlockIds.PixelShaderBlock);
            }

            // Iterate through the shapes and calculate a bounding box which encompasses all of them.
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (var shape in SHP1Tag.Shapes)
            {
                Vector3 sMin = shape.BoundingBox.Min;
                Vector3 sMax = shape.BoundingBox.Max;

                if (sMin.X < min.X)
                    min.X = sMin.X;
                if (sMax.X > max.X)
                    max.X = sMax.X;

                if (sMin.Y < min.Y)
                    min.Y = sMin.Y;
                if (sMax.Y > max.Y)
                    max.Y = sMax.Y;

                if (sMin.Z < min.Z)
                    min.Z = sMin.Z;
                if (sMax.Z > max.Z)
                    max.Z = sMax.Z;
            }

            BoundingBox = new FAABox(min, max);
            BoundingSphere = new FSphere(BoundingBox.Center, BoundingBox.Max.Length);
        }

        private void AssignVertexAttributesToMaterialsRecursive(HierarchyNode curNode, ref Material curMaterial)
        {
            switch (curNode.Type)
            {
                case HierarchyDataType.Material: curMaterial = MAT3Tag.MaterialList[MAT3Tag.MaterialRemapTable[curNode.Value]]; break;
                case HierarchyDataType.Batch: curMaterial.VtxDesc = SHP1Tag.Shapes[SHP1Tag.ShapeRemapTable[curNode.Value]].VertexDescription; break;
            }

            foreach (var child in curNode.Children)
                AssignVertexAttributesToMaterialsRecursive(child, ref curMaterial);
        }

        public void Tick(float deltaTime)
        {
            foreach (var boneAnim in m_boneAnimations)
                boneAnim.Tick(deltaTime);

            foreach (var matAnim in m_materialAnimations)
                matAnim.Tick(deltaTime);

            if (m_currentBoneAnimation != null)
                m_currentBoneAnimation.ApplyAnimationToPose(JNT1Tag.AnimatedJoints);

            if (m_currentMaterialAnimation != null)
                m_currentMaterialAnimation.ApplyAnimationToMaterials(MAT3Tag);
        }

        public void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix, Matrix4 modelMatrix)
        {
            m_viewMatrix = viewMatrix;
            m_projMatrix = projectionMatrix;
            m_modelMatrix = modelMatrix;

            IList<SkeletonJoint> boneList = (m_currentBoneAnimation != null) ? JNT1Tag.AnimatedJoints : JNT1Tag.BindJoints;

            Matrix4[] boneTransforms = new Matrix4[boneList.Count];
            ApplyBonePositionsToAnimationTransforms(boneList, boneTransforms);

            // Assume that all bone animations constantly invalidate the skinning.
            if (m_currentBoneAnimation != null)
                m_skinningInvalid = true;

            // We'll only transform the position and normal vertices if skinning has been invalidated.
            if (m_skinningInvalid)
            {
                foreach (var shape in SHP1Tag.Shapes)
                {
                    var transformedVerts = new List<Vector3>(shape.VertexData.Position);
                    //List<WLinearColor> colorOverride = new List<WLinearColor>();
                    List<Vector3> transformedNormals = new List<Vector3>();

                    for (int i = 0; i < shape.VertexData.Position.Count; i++)
                    {
                        // This is relative to the vertex's original packet's matrix table.  
                        ushort posMtxIndex = (ushort)(shape.VertexData.PositionMatrixIndexes[i]);

                        // We need to calculate which packet data table that is.
                        int originalPacketIndex = 0;
                        for (int p = 0; p < shape.MatrixDataTable.Count; p++)
                        {
                            if (i >= shape.MatrixDataTable[p].FirstRelevantVertexIndex && i < shape.MatrixDataTable[p].LastRelevantVertexIndex)
                            {
                                originalPacketIndex = p; break;
                            }
                        }

                        // Now that we know which packet this vertex belongs to, we can get the index from it.
                        // If the Matrix Table index is 0xFFFF then it means "use previous", and we have to
                        // continue backwards until it is no longer 0xFFFF.
                        ushort matrixTableIndex;
                        do
                        {
                            matrixTableIndex = shape.MatrixDataTable[originalPacketIndex].MatrixTable[posMtxIndex];
                            originalPacketIndex--;
                        } while (matrixTableIndex == 0xFFFF);

                        bool isPartiallyWeighted = DRW1Tag.IsWeighted[matrixTableIndex];
                        ushort indexFromDRW1 = DRW1Tag.Indexes[matrixTableIndex];

                        Matrix4 finalMatrix = Matrix4.Zero;
                        if (isPartiallyWeighted)
                        {
                            EVP1.Envelope envelope = EVP1Tag.Envelopes[indexFromDRW1];
                            for (int b = 0; b < envelope.NumBones; b++)
                            {
                                Matrix4 sm1 = EVP1Tag.InverseBindPose[envelope.BoneIndexes[b]];
                                Matrix4 sm2 = boneTransforms[envelope.BoneIndexes[b]];

                                finalMatrix = finalMatrix + Matrix4.Mult(Matrix4.Mult(sm1, sm2), envelope.BoneWeights[b]);
                            }
                        }
                        else
                        {
                            // If the vertex is not weighted then we use a 1:1 movement with the bone matrix.
                            finalMatrix = boneTransforms[indexFromDRW1];
                        }

                        Vector3 transformedVertPos = Vector3.Transform(transformedVerts[i], finalMatrix);
                        transformedVerts[i] = transformedVertPos;

                        if (shape.VertexData.Normal.Count > 0)
                        {
                            Vector3 transformedNormal = Vector3.TransformNormal(shape.VertexData.Normal[i], finalMatrix);
                            transformedNormals.Add(transformedNormal);
                        }

                        //colorOverride.Add(isPartiallyWeighted ? WLinearColor.Black : WLinearColor.White);
                    }

                    // Re-upload to the GPU.
                    shape.OverrideVertPos = transformedVerts;
                    //shape.VertexData.Color0 = colorOverride;
                    if (transformedNormals.Count > 0)
                        shape.OverrideNormals = transformedNormals;
                    shape.UploadBuffersToGPU(true);
                }

                m_skinningInvalid = false;
            }

            //if (WInput.GetKeyDown(System.Windows.Input.Key.O))
            //    m_shapeIndex--;
            //if (WInput.GetKeyUp(System.Windows.Input.Key.P))
            //    m_shapeIndex++;

            m_shapeIndex = WMath.Clamp(m_shapeIndex, 0, SHP1Tag.ShapeCount - 1);

            RenderMeshRecursive(INF1Tag.HierarchyRoot);

            // We're going to restore some semblance of state after rendering ourselves, as models often modify weird and arbitrary GX values.
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.Enable(EnableCap.Dither);
        }

        private void ApplyBonePositionsToAnimationTransforms(IList<SkeletonJoint> boneList, Matrix4[] boneTransforms)
        {
            for (int i = 0; i < boneList.Count; i++)
            {
                SkeletonJoint curJoint, origJoint;
                curJoint = origJoint = boneList[i];

                Matrix4 cumulativeTransform = Matrix4.Identity;
                while (true)
                {
                    Matrix4 jointMatrix = Matrix4.CreateScale(curJoint.Scale) * Matrix4.CreateFromQuaternion(curJoint.Rotation) * Matrix4.CreateTranslation(curJoint.Translation);
                    cumulativeTransform *= jointMatrix;
                    if (curJoint.Parent == null)
                        break;

                    curJoint = curJoint.Parent;
                }

                boneTransforms[i] = cumulativeTransform;

                if (origJoint.Parent != null)
                {
                    Vector3 curPos = cumulativeTransform.ExtractTranslation();
                    Vector3 parentPos = boneTransforms[boneList.IndexOf(origJoint.Parent)].ExtractTranslation();

                    //m_lineBatcher.DrawLine(curPos, parentPos, WLinearColor.Red, 1, 0);
                }
            }
        }

        private int m_shapeIndex;

        private void RenderMeshRecursive(HierarchyNode curNode)
        {
            switch (curNode.Type)
            {
                case HierarchyDataType.Material:
                    BindMaterialByIndex(curNode.Value);
                    break;

                case HierarchyDataType.Batch:
                    //if (curNode.Value != m_shapeIndex) break;
                    RenderBatchByIndex(curNode.Value);
                    break;
            }

            foreach (var child in curNode.Children)
                RenderMeshRecursive(child);
        }

        private void BindMaterialByIndex(ushort index)
        {
            Material material = MAT3Tag.MaterialList[MAT3Tag.MaterialRemapTable[index]];
            material.Bind();
            m_currentBoundMat = material;

            Shader shader = material.Shader;

            GL.UniformMatrix4(shader.UniformModelMtx, false, ref m_modelMatrix);
            GL.UniformMatrix4(shader.UniformViewMtx, false, ref m_viewMatrix);
            GL.UniformMatrix4(shader.UniformProjMtx, false, ref m_projMatrix);

            for (int i = 0; i < 8; i++)
            {
                int idx = material.TextureIndexes[i];
                if (idx < 0) continue;

                //int glTextureIndex = GL.GetUniformLocation(shader.Program, string.Format("Texture[{0}]", i));
                Texture tex = TEX1Tag.Textures[MAT3Tag.TextureRemapTable[idx]];

                // Before we bind the texture, we need to check if this particular texture has been overriden.
                // This allows textures to be replaced on a per-name basis with another file. Used in cases of
                // broken/incorrect texture included by default in models, ie: The Wind Waker toon textures.
                if (m_textureOverrides.ContainsKey(tex.Name))
                    tex = m_textureOverrides[tex.Name];

                GL.Uniform1(shader.UniformTextureSamplers[i], i); // Everything dies without this, don't forget this bit.
                tex.Bind(i);
            }

            if (shader.UniformTexMtx >= 0)
            {
                for (int i = 0; i < material.TexMatrixIndexes.Length; i++)
                {
                    Matrix4 matrix = material.TexMatrixIndexes[i].TexMtx;
                    string matrixString = string.Format("TexMtx[{0}]", i);
                    int matrixUniformLoc = GL.GetUniformLocation(shader.Program, matrixString);
                    matrix.Transpose();

                    GL.UniformMatrix4(matrixUniformLoc, false, ref matrix);
                }
            }

            var color0Amb = material.AmbientColorIndexes[0];
            var color0Mat = material.MaterialColorIndexes[0];
            var color1Amb = material.AmbientColorIndexes[1];
            var color1Mat = material.MaterialColorIndexes[1];

            if (shader.UniformColor0Amb >= 0) GL.Uniform4(shader.UniformColor0Amb, color0Amb.R, color0Amb.G, color0Amb.B, color0Amb.A);
            if (shader.UniformColor0Mat >= 0) GL.Uniform4(shader.UniformColor0Mat, color0Mat.R, color0Mat.G, color0Mat.B, color0Mat.A);
            if (shader.UniformColor1Amb >= 0) GL.Uniform4(shader.UniformColor1Amb, color1Amb.R, color1Amb.G, color1Amb.B, color1Amb.A);
            if (shader.UniformColor1Mat >= 0) GL.Uniform4(shader.UniformColor1Mat, color1Mat.R, color1Mat.G, color1Mat.B, color1Mat.A);

            // Set the OpenGL State
            GXToOpenGL.SetBlendState(material.BlendModeIndex);
            GXToOpenGL.SetCullState(material.CullModeIndex);
            GXToOpenGL.SetDepthState(material.ZModeIndex);
            GXToOpenGL.SetDitherEnabled(material.DitherIndex);

            //if (WInput.GetKey(System.Windows.Input.Key.U))
            //    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            //else
            //    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);


            // Update the data in the PS Block
            PSBlock psData = new PSBlock();
            m_tevColorOverrides.SetPSBlockForMaterial(material, ref psData);
            UpdateTextureDimensionsForPSBlock(ref psData, material);
            UpdateFogForPSBlock(ref psData, material);

            // Upload the PS Block to the GPU
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, (int)ShaderUniformBlockIds.PixelShaderBlock, shader.PSBlockUBO);
            GL.BufferData<PSBlock>(BufferTarget.UniformBuffer, (IntPtr)(Marshal.SizeOf(psData)), ref psData, BufferUsageHint.DynamicDraw);
        }

        private void UpdateFogForPSBlock(ref PSBlock psData, Material mat)
        {
            psData.FogColor = mat.FogModeIndex.Color;
        }

        private void UpdateTextureDimensionsForPSBlock(ref PSBlock psData, Material mat)
        {
            for (int i = 0; i < 8; i++)
            {
                if (mat.TextureIndexes[i] < 0)
                    continue;

                Texture texture = TEX1Tag.Textures[MAT3Tag.TextureRemapTable[i]];
                Vector4 texDimensions = new Vector4(texture.CompressedData.Width, texture.CompressedData.Height, 0, 0);

                switch (i)
                {
                    case 0: psData.TexDimension0 = texDimensions; break;
                    case 1: psData.TexDimension1 = texDimensions; break;
                    case 2: psData.TexDimension2 = texDimensions; break;
                    case 3: psData.TexDimension3 = texDimensions; break;
                    case 4: psData.TexDimension4 = texDimensions; break;
                    case 5: psData.TexDimension5 = texDimensions; break;
                    case 6: psData.TexDimension6 = texDimensions; break;
                    case 7: psData.TexDimension7 = texDimensions; break;
                }
            }
        }


        private void RenderBatchByIndex(ushort index)
        {
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw);

            SHP1.Shape shape = SHP1Tag.Shapes[SHP1Tag.ShapeRemapTable[index]];
            shape.Bind();
            shape.Draw();
            shape.Unbind();
        }

        private void DrawBoundsForJoints(bool boundingBox, bool boundingSphere)
        {
            Matrix4[] boneTransforms = new Matrix4[JNT1Tag.BindJoints.Count];
            for (int i = 0; i < JNT1Tag.BindJoints.Count; i++)
            {
                SkeletonJoint curJoint, origJoint;
                curJoint = origJoint = JNT1Tag.BindJoints[i];

                Matrix4 cumulativeTransform = Matrix4.Identity;
                while (true)
                {
                    Matrix4 jointMatrix = Matrix4.CreateScale(curJoint.Scale) * Matrix4.CreateFromQuaternion(curJoint.Rotation) * Matrix4.CreateTranslation(curJoint.Translation);
                    cumulativeTransform *= jointMatrix;
                    if (curJoint.Parent == null)
                        break;

                    curJoint = curJoint.Parent;
                }

                boneTransforms[i] = cumulativeTransform;
                Vector3 curPos = cumulativeTransform.ExtractTranslation();
                Quaternion curRot = cumulativeTransform.ExtractRotation();

                WLinearColor jointColor = origJoint.Unknown1 == 0 ? WLinearColor.Yellow : WLinearColor.Blue;
                if (boundingSphere)
                {
                    //m_lineBatcher.DrawSphere(curPos, origJoint.BoundingSphereDiameter, 8, jointColor, 0f, 0f);
                }
                if (boundingBox)
                {
                    Vector3 extents = (origJoint.BoundingBox.Max - origJoint.BoundingBox.Min) / 2;
                    //m_lineBatcher.DrawBox(curPos, extents, curRot, jointColor, 0f, 0f);
                }
            }
        }

        private void DrawBoundsForShapes(bool boundingBox, bool boundingSphere)
        {
            foreach (var shape in SHP1Tag.Shapes)
            {
                Vector3 center = (shape.BoundingBox.Max - shape.BoundingBox.Min) / 2;
                if (boundingSphere)
                {
                    //m_lineBatcher.DrawSphere(center, shape.BoundingSphereDiameter, 8, WLinearColor.White, 0f, 0f);
                }
                if (boundingBox)
                {
                    Vector3 extents = (shape.BoundingBox.Max - shape.BoundingBox.Min) / 2;
                    //m_lineBatcher.DrawBox(center, extents, Quaternion.Identity, WLinearColor.White, 0f, 0f);
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
