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
        private int m_vbo, m_cbo, m_ebo;
        private Shader m_primitiveShader;
        private int m_triangleCount;
        private UpAxisType m_UpAxis;

        private FAABox m_aaBox;

        private Vector3[] m_Vertices;
        private Vector4[] m_Colors;
        private Vector4[] m_Colors_Black;
        private int[] m_Indices;
        private List<CollisionGroupNode> m_Nodes;
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

        public bool IsDirty { get; set; }

        public WCollisionMesh(WWorld world) : base(world)
        {
            m_UpAxis = UpAxisType.Y_UP;
            Triangles = new List<CollisionTriangle>();
            m_Nodes = new List<CollisionGroupNode>();
            IsRendered = true;
            CreateShader();
        }

        public WCollisionMesh(WWorld world, COLLADA dae) :base(world)
        {
            m_UpAxis = UpAxisType.Y_UP;
            m_Nodes = new List<CollisionGroupNode>();

            IsDirty = true;
            Triangles = new List<CollisionTriangle>();
            IsRendered = true;

            CreateShader();
            LoadFromCollada(dae);
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
