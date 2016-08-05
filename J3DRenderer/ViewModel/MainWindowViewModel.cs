﻿using OpenTK.Graphics.OpenGL;
using OpenTK;
using WindEditor;
using System.IO;
using GameFormatReader.Common;
using System.ComponentModel;
using JStudio.J3D;
using JStudio.OpenGL;
using J3DRenderer.Framework;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;

namespace J3DRenderer
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public J3D LoadedModel { get { return m_loadedModels.Count > 0 ? m_loadedModels[0] : null; } }
        public HighresScreenshotViewModel HighResScreenshot { get { return m_highresScreenshot; } }

        private GLControl m_glControl;
        private System.Diagnostics.Stopwatch m_dtStopwatch;

        // Rendering
        private WCamera m_renderCamera;
        private int m_viewportHeight;
        private int m_viewportWidth;
        private float m_timeSinceStartup;

        private J3D[] m_skybox;

        private ScreenspaceQuad m_screenQuad;
        private Shader m_alphaVisualizationShader;
        private WFrameBuffer m_frameBuffer;
        private HighresScreenshotViewModel m_highresScreenshot;

        private List<J3D> m_loadedModels;

        GXLight m_mainLight;
        GXLight m_secondaryLight;

        public MainWindowViewModel()
        {
            m_renderCamera = new WCamera();
            m_loadedModels = new List<J3D>();
            m_renderCamera.Transform.Position = new Vector3(500, 75, 500);
            m_renderCamera.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitY, WMath.DegreesToRadians(45f));
            m_highresScreenshot = new HighresScreenshotViewModel();
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
            Application.Current.MainWindow.Closing += OnMainWindowClosing;
        }

        internal void OnMainEditorWindowLoaded(GLControl child)
        {
            m_glControl = child;
            m_frameBuffer = new WFrameBuffer(m_glControl.Width, m_glControl.Height);

            m_alphaVisualizationShader = new Shader("AlphaVisualize");
            m_alphaVisualizationShader.CompileSource(File.ReadAllText("resources/shaders/Debug_AlphaVisualizer.vert"), ShaderType.VertexShader);
            m_alphaVisualizationShader.CompileSource(File.ReadAllText("resources/shaders/Debug_AlphaVisualizer.frag"), ShaderType.FragmentShader);
            m_alphaVisualizationShader.LinkShader();

            m_screenQuad = new ScreenspaceQuad();

            // Set up the Editor Tick Loop
            System.Windows.Forms.Timer editorTickTimer = new System.Windows.Forms.Timer();
            editorTickTimer.Interval = 16; //ms
            editorTickTimer.Tick += (o, args) =>
            {
                DoApplicationTick();
            };
            editorTickTimer.Enabled = true;

            var lightPos = new Vector4(250, 200, 250, 0);
            m_mainLight = new GXLight(lightPos, -lightPos.Normalized(), new Vector4(1, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
            m_secondaryLight = new GXLight(lightPos, -lightPos.Normalized(), new Vector4(0, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
        }

        public void OnFilesDropped(string[] droppedFilePaths)
        {
            // Only check the first one for now...
            if (droppedFilePaths.Length > 0)
                LoadModelFromFilepath(droppedFilePaths[0], true);
        }

        private void LoadModelFromFilepath(string filePath, bool unloadExisting)
        {
            if (unloadExisting)
            {
                foreach (var model in m_loadedModels)
                    model.Dispose();
                m_loadedModels.Clear();
            }

            if (!File.Exists(filePath))
                Console.WriteLine("Cannot load: \"{0}\", not a file!", filePath);

            if (string.Compare(Path.GetExtension(filePath), ".bdl", true) == 0 || string.Compare(Path.GetExtension(filePath), ".bmd", true) == 0)
            {
                var newModel = new J3D(Path.GetFileNameWithoutExtension(filePath));
                using (EndianBinaryReader reader = new EndianBinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read), Endian.Big))
                    newModel.LoadFromStream(reader);

                newModel.SetHardwareLight(0, m_mainLight);
                newModel.SetHardwareLight(1, m_secondaryLight);
                newModel.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");
                newModel.SetTextureOverride("ZAtoon", "resources/textures/ZAtoon.png");
                m_loadedModels.Add(newModel);
            }


            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LoadedModel"));

            //// Animations
            //foreach (var bck in Directory.GetFiles("resources/cl/bcks/"))
            //{
            //    m_childLink.LoadBoneAnimation(bck);
            //}
        }

        private void DoApplicationTick()
        {
            // Poll the mouse at a high resolution
            System.Drawing.Point mousePos = m_glControl.PointToClient(System.Windows.Forms.Cursor.Position);

            mousePos.X = WMath.Clamp(mousePos.X, 0, m_glControl.Width);
            mousePos.Y = WMath.Clamp(mousePos.Y, 0, m_glControl.Height);
            WInput.SetMousePosition(new Vector2(mousePos.X, mousePos.Y));

            ProcessTick();
            WInput.Internal_UpdateInputState();

            m_glControl.SwapBuffers();
        }

        private void ProcessTick()
        {
            System.Random rnd = new System.Random(m_glControl.GetHashCode());
            //GL.ClearColor(0.15f, 0.83f, 0.10f, 1f);
            GL.ClearColor(rnd.Next(255) / 255f, rnd.Next(255) / 255f, rnd.Next(255) / 255f, 1);

            // If the user has requested a higher resolution screenshot, we're going to resize the backbuffer if required.
            if (m_highresScreenshot.UserRequestedScreenshot)
            {
                int scaledWidth = (int)(m_viewportWidth * m_highresScreenshot.ResolutionMultiplier);
                int scaledHeight = (int)(m_viewportHeight * m_highresScreenshot.ResolutionMultiplier);

                if (scaledWidth != m_frameBuffer.Width || scaledHeight != m_frameBuffer.Height)
                    m_frameBuffer.ResizeBuffer(scaledWidth, scaledHeight);
            }

            m_frameBuffer.Bind();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GL.Viewport(0, 0, m_frameBuffer.Width, m_frameBuffer.Height);

            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();
            m_renderCamera.Tick(deltaTime);

            deltaTime = WMath.Clamp(deltaTime, 0, 0.25f); // quater second max because debugging
            m_timeSinceStartup += deltaTime;

            // Rotate our light
            float angleInRad = m_timeSinceStartup % WMath.DegreesToRadians(360f);
            Quaternion lightRot = Quaternion.FromAxisAngle(Vector3.UnitY, angleInRad);
            Vector3 newLightPos = Vector3.Transform(new Vector3(-500, 0, 0), lightRot) + new Vector3(0, 50, 0);
            m_mainLight.Position = new Vector4(newLightPos, 0);

            // Render something
            //for (int i = 0; i < m_skybox.Length; i++)
            //{
            //    if (m_skybox[i] == null)
            //        continue;

            //    m_skybox[i].Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
            //}

            foreach (var j3d in m_loadedModels)
            {
                j3d.SetHardwareLight(0, m_mainLight);
                j3d.Tick(deltaTime);
                j3d.Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
            }

            // Debug Rendering
            if (WInput.GetKey(Key.I))
            {
                GL.Disable(EnableCap.CullFace);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.DstAlpha, BlendingFactorDest.Zero);

                m_alphaVisualizationShader.Bind();
                m_screenQuad.Draw();
            }

            // Blit the framebuffer to the backbuffer so it shows up on screen.
            m_frameBuffer.BlitToBackbuffer(m_viewportWidth, m_viewportHeight);

            // Now that we've rendered to the framebuffer, if they've requested a screenshot, write that to disk.
            if (m_highresScreenshot.UserRequestedScreenshot)
            {
                CaptureScreenshotToDisk();

                // Resize the buffer back down if required.
                if (m_frameBuffer.Width != m_viewportWidth || m_frameBuffer.Height != m_viewportHeight)
                    m_frameBuffer.ResizeBuffer(m_viewportWidth, m_viewportHeight);

                m_highresScreenshot.UserRequestedScreenshot = false;
            }
        }

        private void CaptureScreenshotToDisk()
        {
            byte[] pixelData = m_frameBuffer.ReadPixels(0, 0, m_frameBuffer.Width, m_frameBuffer.Height);

            using (Bitmap bmp = new Bitmap(m_frameBuffer.Width, m_frameBuffer.Height))
            {
                Rectangle rect = new Rectangle(0, 0, m_frameBuffer.Width, m_frameBuffer.Height);
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                //Lock the bitmap for writing, copy the bits and then unlock for saving.
                IntPtr ptr = bmpData.Scan0;
                //byte[] imageData = pixel;
                Marshal.Copy(pixelData, 0, ptr, pixelData.Length);
                bmp.UnlockBits(bmpData);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                // Bitmaps will throw an exception if the output folder doesn't exist so...
                string outputName = string.Format("Screenshots/{0:yyyy-MM-dd_hh-mm-ss-tt}.png", DateTime.UtcNow);
                Directory.CreateDirectory(Path.GetDirectoryName(outputName));
                bmp.Save(outputName);
            }
        }

        internal void OnViewportResized(int width, int height)
        {
            m_viewportWidth = width;
            m_viewportHeight = height;
            m_renderCamera.AspectRatio = width / (float)height;
            m_frameBuffer.ResizeBuffer(width, height);
        }

        private void OnMainWindowClosing(object sender, CancelEventArgs e)
        {
            if (m_frameBuffer != null)
                m_frameBuffer.Dispose();

            if (m_alphaVisualizationShader != null)
                m_alphaVisualizationShader.Dispose();

            foreach (var j3d in m_loadedModels)
                j3d.Dispose();
        }
    }
}
