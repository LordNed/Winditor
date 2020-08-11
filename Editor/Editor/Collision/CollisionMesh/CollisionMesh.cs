using GameFormatReader.Common;
using JStudio.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Assimp;
using Collada141;

namespace WindEditor.Collision
{
    public partial class WCollisionMesh : WDOMNode, IRenderable
    {
        private UpAxisType m_UpAxis;
        private List<CollisionGroupNode> m_Nodes;

        private FAABox m_aaBox;

        private CollisionGroupNode m_RootNode;
        public CollisionProperty[] m_Properties;

        public string FileName { get; set; }

        public CollisionGroupNode RootNode
        {
            get { return m_RootNode; }
            set
            {
                if (value != m_RootNode)
                {
                    m_RootNode = value;
                    OnPropertyChanged("RootNode");
                }
            }
        }

        public List<CollisionTriangle> Triangles { get; private set; }
        public int TriangleCount { get { return Triangles != null ? Triangles.Count : 0; } }

        public bool IsDirty { get; set; }
        public bool PreviousRenderVisibility { get; set; }

        /// <summary>
        /// Creates a collision mesh from the given file, handling DZB files only.
        /// </summary>
        /// <param name="world">World containing the collision mesh</param>
        /// <param name="file_name">File to load the model from</param>
        public WCollisionMesh(WWorld world, string file_name) : base(world)
        {
            Init();
            FileName = Path.GetFileNameWithoutExtension(file_name);

            if (file_name.EndsWith(".dzb"))
            {
                using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(file_name), Endian.Big))
                {
                    LoadFromDZB(reader);
                }
            }
            else
            {
                throw new Exception($"File is not a DZB: {file_name}");
            }
        }

        /// <summary>
        /// Creates a collision mesh from the given file, handling COLLADA DAE files only.
        /// </summary>
        /// <param name="world">World containing the collision mesh</param>
        /// <param name="file_name">File to load the model from</param>
        /// <param name="roomIndex">The room number to give to the new collision mesh</param>
        /// <param name="roomTableIndex">The room table index to give to all collision groups in the new collision mesh (hack)</param>
        public WCollisionMesh(WWorld world, string file_name, int roomIndex, int roomTableIndex) : base(world)
        {
            Init();
            FileName = Path.GetFileNameWithoutExtension(file_name);

            if (file_name.EndsWith(".dae"))
            {
                COLLADA dae = COLLADA.Load(file_name);
                LoadFromCollada(dae, roomIndex, roomTableIndex);
            }
            else
            {
                throw new Exception($"File is not a DAE: {file_name}");
            }
        }

        private void Init()
        {
            m_UpAxis = UpAxisType.Y_UP;
            m_Nodes = new List<CollisionGroupNode>();

            IsRendered = false;
            IsDirty = true;
            Triangles = new List<CollisionTriangle>();

            m_vbo = -1;
            m_ebo = -1;
            m_cbo = -1;

            CreateShader();
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
