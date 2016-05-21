using GameFormatReader.Common;
using OpenTK;
using System.ComponentModel;
using OpenTK.Graphics.OpenGL;
using System;
using WindEditor;
using System.IO;

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

        private int m_totalFileSize;

        // Hack
        private Matrix4 m_viewMatrix;
        private Matrix4 m_projMatrix;
        private Matrix4 m_modelMatrix;

        // More hack
        private int[] m_vertexBuffer, m_indexBuffer, m_texcoordBuffer;
        private int m_textureVBO;
        private Shader m_shader;

        public JStudio3D()
        {
            m_shader = new Shader("UnlitTexture");
            m_shader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.vert"), ShaderType.VertexShader);
            m_shader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.frag"), ShaderType.FragmentShader);
            m_shader.LinkShader();
        }

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

                        // Hack
                        m_vertexBuffer = new int[SHP1Tag.ShapeCount];
                        m_indexBuffer = new int[SHP1Tag.ShapeCount];
                        m_texcoordBuffer = new int[SHP1Tag.ShapeCount];
                        m_textureVBO = GL.GenBuffer();

                        for(int s = 0; s < SHP1Tag.ShapeCount; s++)
                        {
                            SHP1.Shape shape = SHP1Tag.Shapes[s];
                            m_vertexBuffer[s] = GL.GenBuffer();
                            m_indexBuffer[s] = GL.GenBuffer();
                            m_texcoordBuffer[s] = GL.GenBuffer();

                            // Positions
                            int vbo = m_vertexBuffer[s];
                            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * shape.VertexData.Position.Count), shape.VertexData.Position.ToArray(), BufferUsageHint.StaticDraw);

                            GL.BindBuffer(BufferTarget.ArrayBuffer, m_texcoordBuffer[s]);
                            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(8 * shape.VertexData.Tex0.Count), shape.VertexData.Tex0.ToArray(), BufferUsageHint.StaticDraw);

                            // Upload Indexes
                            int ebo = m_indexBuffer[s];
                            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
                            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * shape.Indexes.Count), shape.Indexes.ToArray(), BufferUsageHint.StaticDraw);
                        }

                        GL.BindTexture(TextureTarget.Texture2D, m_textureVBO);
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

                        // Black/white checkerboard
                        float[] pixels = new[]
                        {
                            0.0f, 0.0f, 0.0f,   255.0f, 255.0f, 255.0f,
                            255.0f, 255.0f, 255.0f,   0.0f, 0.0f, 0.0f
                        };
                        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 2, 2, 0, PixelFormat.Rgb, PixelType.Float, pixels);

                        //System.Drawing.Imaging.BitmapData bmpData = mat.Diffuse.LockBits(new System.Drawing.Rectangle(0, 0, mat.Diffuse.Width, mat.Diffuse.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, mat.Diffuse.Width, mat.Diffuse.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);
                        //mat.Diffuse.UnlockBits(bmpData);

                        break;
                    // MATERIAL - Stores materials (which describes how textures, etc. are drawn)
                    case "MAT3":
                        MAT3Tag = new MAT3();
                        MAT3Tag.LoadMAT3FromStream(reader, tagStart, tagSize);
                        break;
                    // TEXTURES - Stores binary texture images.
                    case "TEX1":
                        break;
                    // MODEL - Seems to be bypass commands for Materials and invokes GX registers directly.
                    case "MDL3":
                        break;
                }

                reader.BaseStream.Position = tagStart + tagSize;
            }
        }

        internal void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix, Matrix4 modelMatrix)
        {
            m_viewMatrix = viewMatrix;
            m_projMatrix = projectionMatrix;
            m_modelMatrix = modelMatrix;

            RenderMeshRecursive(INF1Tag.Hierarchy);
        }

        public void RenderMeshRecursive(HierarchyNode curNode)
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
        }

        private void RenderBatchByIndex(ushort index)
        {
            //GL.Enable(EnableCap.PrimitiveRestart);
            //GL.PrimitiveRestartIndex(0xFFFF);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            m_shader.Bind();
            GL.UniformMatrix4(m_shader.UniformModelMtx, false, ref m_modelMatrix);
            GL.UniformMatrix4(m_shader.UniformViewMtx, false, ref m_viewMatrix);
            GL.UniformMatrix4(m_shader.UniformProjMtx, false, ref m_projMatrix);

            // Position
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertexBuffer[index]);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);

            // Texcoord
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_texcoordBuffer[index]);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Tex0);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Tex0, 2, VertexAttribPointerType.Float, false, 8, 0);

            // Texture
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, m_textureVBO);

            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_indexBuffer[index]);

            // Draw!
            GL.DrawElements(BeginMode.Triangles, SHP1Tag.Shapes[index].Indexes.Count, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Tex0);
        }


        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
