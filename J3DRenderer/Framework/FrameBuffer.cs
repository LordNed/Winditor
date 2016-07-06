using OpenTK.Graphics.OpenGL;
using System;

namespace WindEditor
{
    public class WFrameBuffer : IDisposable
    {
        public int Width { get { return m_frameBufferWidth; } }
        public int Height { get { return m_frameBufferHeight; } }

        private int m_frameBufferIndex;
        private int m_rgbRenderBufferIndex;
        private int m_depthRenderBufferIndex;

        private int m_frameBufferWidth;
        private int m_frameBufferHeight;

        // To detect redundant disposal calls.
        private bool m_hasBeenDisposed = false;

        public WFrameBuffer(int width, int height)
        {
            ClearGLError();

            // Generate a Framebuffer Object (FBO) on the GPU
            GL.GenFramebuffers(1, out m_frameBufferIndex);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, m_frameBufferIndex);

            ResizeBuffer(width, height);

            // Bind our Render Buffer to the FBO we created earlier.
            GLErrorCheck();
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, RenderbufferTarget.Renderbuffer, m_rgbRenderBufferIndex);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, m_depthRenderBufferIndex);
            GLErrorCheck();

            // ?
            //GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GLErrorCheck();

            var fbStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (fbStatus != FramebufferErrorCode.FramebufferComplete)
                Console.WriteLine("Initializing Framebuffer Failed: {0}", fbStatus);
            GLErrorCheck();
        }

        public void Bind()
        {
            GLErrorCheck();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, m_frameBufferIndex);
            GLErrorCheck();
        }

        public byte[] ReadPixels(int x, int y, int width, int height)
        {
            if (x < 0 || y < 0 || (x + width) > m_frameBufferWidth || (y + height) > m_frameBufferHeight)
                throw new ArgumentOutOfRangeException("x, y, width, height", "Specified rectangle is out of range.");
            ClearGLError();

            byte[] buf = new byte[width * height * 4];
            GLErrorCheck();

            GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
            GLErrorCheck();

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            GLErrorCheck();

            GL.ReadPixels(x, y, width, height, PixelFormat.Rgba, PixelType.UnsignedByte, buf);
            GLErrorCheck();
            return buf;
        }

        public void ResizeBuffer(int newWidth, int newHeight)
        {
            ClearGLError();

            Console.WriteLine("WFrameBuffer: Resizing to {0}x{1}", newWidth, newHeight);
            int maxDims;
            GL.GetInteger(GetPName.MaxRenderbufferSize, out maxDims);
            GLErrorCheck();

            // ToDo: 
            // GL.GetRenderbufferParameter(RenderbufferTarget.) Get the RenderbufferParameters after constructing the renderbuffer.


            if (maxDims < newWidth)
                throw new ArgumentException(string.Format("Exceeds max renderbuffer size for this GPU (\"{0}\")!", maxDims), "newWidth");
            if(maxDims < newHeight)
                throw new ArgumentException(string.Format("Exceeds max renderbuffer size for this GPU (\"{0}\")!", maxDims), "newHeight");

            if (m_rgbRenderBufferIndex > 0)
                GL.DeleteRenderbuffers(1, ref m_rgbRenderBufferIndex);

            if (m_depthRenderBufferIndex > 0)
                GL.DeleteRenderbuffers(1, ref m_depthRenderBufferIndex);

            // Generate a RenderBuffer Object 
            GL.GenRenderbuffers(1, out m_rgbRenderBufferIndex);
            GL.GenRenderbuffers(1, out m_depthRenderBufferIndex);
            GLErrorCheck();

            // Allocate memory on the GPU to represent our Renderbuffers.
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, m_rgbRenderBufferIndex);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Rgba8, newWidth, newHeight);
            GLErrorCheck();

            // Tell OpenGL which color attachments we'll use of this framebuffer for rendering.
            DrawBuffersEnum[] attachments = new[] { DrawBuffersEnum.ColorAttachment0 };
            GL.DrawBuffers(attachments.Length, attachments);
            GLErrorCheck();

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, m_depthRenderBufferIndex);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent24, newWidth, newHeight);
            GLErrorCheck();

            m_frameBufferWidth = newWidth;
            m_frameBufferHeight = newHeight;
        }

        public void BlitToBackbuffer()
        {
            ClearGLError();

            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, m_frameBufferIndex);
            GLErrorCheck();

            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
            GLErrorCheck();

            //GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
            //GL.DrawBuffer(DrawBufferMode.ColorAttachment0);

            ClearGLError();
            GL.BlitFramebuffer(0, 0, m_frameBufferWidth, m_frameBufferHeight, 0, 0, m_frameBufferWidth, m_frameBufferHeight, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
            GLErrorCheck();
        }

        private static void GLErrorCheck()
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
                Console.WriteLine("GL Error: {0}", error);
        }

        private static void ClearGLError()
        {
            ErrorCode error;
            do
            {
                error = GL.GetError();
            } while (error != ErrorCode.NoError);
        }

        #region IDisposable Support
        ~WFrameBuffer()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        protected virtual void Dispose(bool manualDispose)
        {
            if (!m_hasBeenDisposed)
            {
                if (manualDispose)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // Free unmanaged state
                GL.DeleteFramebuffers(1, ref m_frameBufferIndex);
                GL.DeleteRenderbuffers(1, ref m_rgbRenderBufferIndex);

                m_hasBeenDisposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
