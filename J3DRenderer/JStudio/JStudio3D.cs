using GameFormatReader.Common;
using OpenTK;
using System.ComponentModel;

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

        private int m_totalFileSize;

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
                        break;
                    // SHAPE - Face/Triangle information for model.
                    case "SHP1":
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
            }
        }

        internal void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix, Matrix4 identity)
        {

        }

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
