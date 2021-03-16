using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpFont;
using System.Drawing;
using System.ComponentModel;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using WArchiveTools;
using WArchiveTools.FileSystem;
using WArchiveTools.Compression;
using System.IO;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace WindEditor.Minitors.Text
{
    public class TextboxRendererViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private Face m_RockNRollFont;
        private GLControl m_GlControl;

        private Bitmap test_tex;

        private Matrix4 m_ProjectionMat;

        private BGTextboxRenderer m_BGTextboxRenderer;

        public TextboxRendererViewModel()
        {
            m_RockNRollFont = new Face(new Library(), "resources/font/RocknRollOne-Regular.ttf");

            LoadTextboxImages();

            m_ProjectionMat = Matrix4.CreateOrthographicOffCenter(0, 640, 448, 0, -10, 10);
        }

        private void LoadTextboxImages()
        {
            VirtualFilesystemDirectory msg_res = ArchiveUtilities.LoadArchive(Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "Msg", "msgres.arc"));

            VirtualFilesystemFile dialog_box = msg_res.GetFileAtPath("timg/hukidashi_00.bti");
            BinaryTextureImage img = new BinaryTextureImage();
            
            using (MemoryStream m = new MemoryStream(dialog_box.Data))
            {
                EndianBinaryReader r = new EndianBinaryReader(m, Endian.Big);
                MemoryStream s = WArchiveTools.Compression.Yaz0.Decode(r);

                EndianBinaryReader real_reader = new EndianBinaryReader(s, Endian.Big);
                img.Load(real_reader, 0, 0);

                test_tex = img.CreateBitmap();
            }
        }

        internal void OnTextboxRendererWindowLoaded(GLControl glControl)
        {
            m_GlControl = glControl;
            m_BGTextboxRenderer = new BGTextboxRenderer();

            GL.Viewport(new Rectangle(0, 0, 640, 448));
        }

        public void Tick(float DeltaTime, Message CurrentMessage)
        {
            m_GlControl.MakeCurrent();

            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit | ClearBufferMask.DepthBufferBit);

            m_BGTextboxRenderer.Render(m_ProjectionMat, CurrentMessage);

            m_GlControl.SwapBuffers();
        }
    }
}
