using GameFormatReader.Common;
using JStudio.J3D.ShaderGen;
using JStudio.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using WindEditor;

namespace JStudio.J3D
{
    public partial class J3D : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Magic { get; protected set; }
        public string StudioType { get; protected set; }
        public string TotalFileSize { get { return string.Format("{0} bytes", m_totalFileSize); } }
        public INF1 INF1Tag { get; protected set; }
        public VTX1 VTX1Tag { get; protected set; }
        public MAT3 MAT3Tag { get; protected set; }
        public SHP1 SHP1Tag { get; protected set; }
        public JNT1 JNT1Tag { get; protected set; }
        public TEX1 TEX1Tag { get; private set; }
        public EVP1 EVP1Tag { get; private set; }
        public DRW1 DRW1Tag { get; private set; }

        private int m_totalFileSize;

        // Hack
        private Matrix4 m_viewMatrix;
        private Matrix4 m_projMatrix;
        private Matrix4 m_modelMatrix;
        private WLineBatcher m_lineBatcher;

        private GXLight[] m_hardwareLights = new GXLight[8];
        private int m_hardwareLightBuffer;

        public void LoadFromStream(EndianBinaryReader reader)
        {
            m_lineBatcher = new WLineBatcher();


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
            }
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


        internal void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix, Matrix4 modelMatrix)
        {
            m_viewMatrix = viewMatrix;
            m_projMatrix = projectionMatrix;
            m_modelMatrix = modelMatrix;


            Matrix4[] boneTransforms = new Matrix4[JNT1Tag.Joints.Count];
            for (int i = 0; i < JNT1Tag.Joints.Count; i++)
            {
                SkeletonJoint curJoint, origJoint;
                curJoint = origJoint = JNT1Tag.Joints[i];

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
                    Vector3 parentPos = boneTransforms[JNT1Tag.Joints.IndexOf(origJoint.Parent)].ExtractTranslation();

                    m_lineBatcher.DrawLine(curPos, parentPos, WLinearColor.Red, 1, 0);
                }
            }

            m_lineBatcher.Render(viewMatrix, projectionMatrix);
            m_lineBatcher.Tick(1 / 60f);

            foreach (var shape in SHP1Tag.Shapes)
            {
                var transformedVerts = new List<Vector3>(shape.VertexData.Position);
                List<WLinearColor> colorOverride = new List<WLinearColor>();
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

                    colorOverride.Add(isPartiallyWeighted ? WLinearColor.Black : WLinearColor.White);
                }

                // Re-upload to the GPU.
                shape.OverrideVertPos = transformedVerts;
                //shape.VertexData.Color0 = colorOverride;
                if (transformedNormals.Count > 0)
                    shape.OverrideNormals = transformedNormals;
                shape.UploadBuffersToGPU();
            }

            if (WInput.GetKeyDown(System.Windows.Input.Key.O))
                m_shapeIndex--;
            if (WInput.GetKeyUp(System.Windows.Input.Key.P))
                m_shapeIndex++;

            m_shapeIndex = WMath.Clamp(m_shapeIndex, 0, SHP1Tag.ShapeCount - 1);

            RenderMeshRecursive(INF1Tag.HierarchyRoot);
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

            Shader shader = material.Shader;

            GL.UniformMatrix4(shader.UniformModelMtx, false, ref m_modelMatrix);
            GL.UniformMatrix4(shader.UniformViewMtx, false, ref m_viewMatrix);
            GL.UniformMatrix4(shader.UniformProjMtx, false, ref m_projMatrix);

            for (int i = 0; i < 8; i++)
            {
                int idx = material.TextureIndexes[i];
                if (idx < 0)
                    continue;

                idx = MAT3Tag.TextureRemapTable[idx];

                int glTextureIndex = GL.GetUniformLocation(shader.Program, string.Format("Texture{0}", i));
                GL.Uniform1(glTextureIndex, i);
                TEX1Tag.Textures[idx].Bind(i);
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

            //int ubi = GL.GetUniformBlockIndex(material.Shader.Program, "LightBlock");
            //GL.UniformBlockBinding(material.Shader.Program, ubi, 0);


            // Set the OpenGL State
            GXToOpenGL.SetBlendState(material.BlendModeIndex);
            GXToOpenGL.SetCullState(material.CullModeIndex);
            GXToOpenGL.SetDepthState(material.ZModeIndex);
            GXToOpenGL.SetDitherEnabled(material.DitherIndex);
        }

        //private int m_lightBufferUniform;
        private int m_psBlockUniform;





        private void RenderBatchByIndex(ushort index)
        {
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw);

            SHP1.Shape shape = SHP1Tag.Shapes[SHP1Tag.ShapeRemapTable[index]];
            shape.Bind();
            shape.Draw();
            shape.Unbind();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
