using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Drawing;
using JStudio.OpenGL;
using System.Drawing.Imaging;

namespace WindEditor.Minitors.Text
{
    public class BGTextboxRenderer : IDisposable
    {
        private bool disposedValue;

        private List<Bitmap> m_BackgroundImages;

        private int m_BG_Program;
        private int m_BG_UniformProjMtx;
        private int m_BG_UniformTexture;

        private int m_BG_Texture;

        private int m_BG_VBO;
        private int m_BG_VAO;
        private uint[] m_BG_Indices = new uint[] { 0, 1, 2, 3, 4, 5 };
        private float[] m_BG_Vertices = new float[]
        {
            0, 0, 0,        0, 0,
            640, 0, 0,      1, 0,
            640, 448, 0,      1, 1,

            640, 448, 0,      1, 1,
            0, 448, 0,      0, 1,
            0, 0, 0,    0, 0
        };

        public BGTextboxRenderer()
        {
            LoadBackgroundImages();

            LoadBGBuffers();
            LoadBGShaders();
        }

        public void Render(Matrix4 ProjectionMatrix, Message CurrentMessage)
        {
            Bitmap BG_Tex = m_BackgroundImages[4];
            BitmapData BG_Tex_Data = BG_Tex.LockBits(new Rectangle(0, 0, BG_Tex.Width, BG_Tex.Height), ImageLockMode.ReadOnly, BG_Tex.PixelFormat);

            // Background first!
            GL.UseProgram(m_BG_Program);

            GL.UniformMatrix4(m_BG_UniformProjMtx, false, ref ProjectionMatrix);

            // Texture
            GL.Uniform1(m_BG_UniformTexture, 0);
            GL.BindTexture(TextureTarget.Texture2D, m_BG_Texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, BG_Tex.Width, BG_Tex.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, BG_Tex_Data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,  (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            BG_Tex.UnlockBits(BG_Tex_Data);

            // Vertex buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_BG_VBO);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 20, 0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 20, 12);

            // Attribute buffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_BG_VAO);
            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
        }

        private void LoadBGShaders()
        {
            int BGVertShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(BGVertShader, File.ReadAllText("resources/shaders/TextboxBackground.vert"));
            GL.CompileShader(BGVertShader);

            int BGFragShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(BGFragShader, File.ReadAllText("resources/shaders/TextboxBackground.frag"));
            GL.CompileShader(BGFragShader);

            m_BG_Program = GL.CreateProgram();
            GL.AttachShader(m_BG_Program, BGVertShader);
            GL.AttachShader(m_BG_Program, BGFragShader);
            GL.LinkProgram(m_BG_Program);

            m_BG_UniformProjMtx = GL.GetUniformLocation(m_BG_Program, "uProjMtx");
            m_BG_UniformTexture = GL.GetUniformLocation(m_BG_Program, "uTexture");
        }

        private void LoadBGBuffers()
        {
            m_BG_VBO = GL.GenBuffer();
            m_BG_VAO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, m_BG_VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(4 * m_BG_Vertices.Length), m_BG_Vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_BG_VAO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * m_BG_Indices.Length), m_BG_Indices, BufferUsageHint.StaticDraw);

            m_BG_Texture = GL.GenTexture();
        }

        private void LoadBackgroundImages()
        {
            m_BackgroundImages = new List<Bitmap>();

            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_dialog.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_dialog.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_sign.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_dialog.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_stone.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_parchment.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_dialog.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_dialog.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_dialog.png"));
            m_BackgroundImages.Add(new Bitmap("resources/textures/bg_dialog.png"));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                GL.DeleteBuffer(m_BG_VBO);
                GL.DeleteBuffer(m_BG_VAO);
                GL.DeleteProgram(m_BG_Program);
                GL.DeleteTexture(m_BG_Texture);

                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BGTextboxRenderer()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
