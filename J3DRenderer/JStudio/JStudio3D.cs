using GameFormatReader.Common;
using OpenTK;
using System.ComponentModel;
using OpenTK.Graphics.OpenGL;
using WindEditor;
using System.IO;
using J3DRenderer.ShaderGen;

namespace J3DRenderer.JStudio
{
    public partial class JStudio3D : INotifyPropertyChanged
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

        private int m_totalFileSize;

        // Hack
        private Matrix4 m_viewMatrix;
        private Matrix4 m_projMatrix;
        private Matrix4 m_modelMatrix;

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
        }

        private void LoadTagDataFromFile(EndianBinaryReader reader, int tagCount)
        {
            for(int i = 0; i < tagCount; i++)
            {
                long tagStart = reader.BaseStream.Position;

                string tagName = reader.ReadString(4);
                int tagSize = reader.ReadInt32();

                switch(tagName)
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
                        break;
                    // DRAW (Skeletal Animation Data) - Stores which matrices (?) are weighted, and which are used directly
                    case "DRW1":
                        break;
                    // JOINTS - Stores the skeletal joints (position, rotation, scale, etc...)
                    case "JNT1":
                        JNT1Tag = new JNT1();
                        JNT1Tag.LoadJNT1FromStream(reader, tagStart);
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
                    System.Console.WriteLine("Skipping generating Shader for Unreferenced Material: {0}", material);
                    continue;
                }
                material.Shader = TEVShaderGenerator.GenerateShader(material, MAT3Tag);
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

            RenderMeshRecursive(INF1Tag.HierarchyRoot);
        }

        private void RenderMeshRecursive(HierarchyNode curNode)
        {
            switch(curNode.Type)
            {
                case HierarchyDataType.Material:
                    BindMaterialByIndex(curNode.Value);
                    break;

                case HierarchyDataType.Batch:
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

            for(int i = 0; i < 8; i++)
            {
                int idx = material.TextureIndexes[i];
                if (idx < 0)
                    continue;

                int glTextureIndex = GL.GetUniformLocation(shader.Program, string.Format("Texture{0}", i));
                GL.Uniform1(glTextureIndex, i);
                TEX1Tag.Textures[idx].Bind(i);
            }

            if (shader.UniformTexMtx >= 0)
            {
                for (int i = 0; i < material.TexMatrixIndexes.Length; i++)
                {
                    int idx = material.TexMatrixIndexes[i];
                    if (idx < 0)
                        continue;

                    Matrix4 matrix = MAT3Tag.TexMatrixInfos[idx].Matrix;
                    GL.UniformMatrix4(GL.GetUniformLocation(shader.Program, string.Format("TexMtx[{0}]", i)), false, ref matrix);
                }
            }

            var color0Amb = MAT3Tag.AmbientColors[material.AmbientColorIndexes[0]];
            var color0Mat = MAT3Tag.MaterialColors[material.MaterialColorIndexes[0]];
            var color1Amb = MAT3Tag.AmbientColors[material.AmbientColorIndexes[1]];
            var color1Mat = MAT3Tag.MaterialColors[material.MaterialColorIndexes[1]];

            if (shader.UniformColor0Amb >= 0) GL.Uniform4(shader.UniformColor0Amb, color0Amb.R, color0Amb.G, color0Amb.B, color0Amb.A);
            if (shader.UniformColor0Mat >= 0) GL.Uniform4(shader.UniformColor0Mat, color0Mat.R, color0Mat.G, color0Mat.B, color0Mat.A);
            if (shader.UniformColor1Amb >= 0) GL.Uniform4(shader.UniformColor1Amb, color1Amb.R, color1Amb.G, color1Amb.B, color1Amb.A);
            if (shader.UniformColor1Mat >= 0) GL.Uniform4(shader.UniformColor1Mat, color1Mat.R, color1Mat.G, color1Mat.B, color1Mat.A);
        }

        private void RenderBatchByIndex(ushort index)
        {
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            SHP1.Shape shape = SHP1Tag.Shapes[SHP1Tag.ShapeRemapTable[index]];
            shape.Bind();
            shape.Draw();
            shape.Unbind();
        }


        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
