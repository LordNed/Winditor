using Editor.Collision;
using GameFormatReader.Common;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;

namespace Editor
{
    class WindEditor
    {
        //private Shader m_primitiveShader;
        //private int m_vbo, m_ebo;

        private List<IRenderable> m_renderables = new List<IRenderable>();
        private List<WWorld> m_editorWorlds = new List<WWorld>();

        public WindEditor()
        {
            WWorld baseWorld = new WWorld();
            baseWorld.LoadMap(@"E:\New_Data_Drive\WindwakerModding\De-Arc-ed Stage\MiniHyo\");
            m_editorWorlds.Add(baseWorld);
            

            //var collision = new WCollisionMesh();
            //using (EndianBinaryReader reader = new EndianBinaryReader(File.OpenRead(@"E:\New_Data_Drive\WindwakerModding\De-Arc-ed Stage\MiniHyo\Room0\dzb\room.dzb"), Endian.Big))
            //{
            //    collision.Load(reader);
            //}
            //m_renderables.Add(collision);


            //collision = new WCollisionMesh();
            //using (EndianBinaryReader reader = new EndianBinaryReader(File.OpenRead(@"E:\New_Data_Drive\WindwakerModding\De-Arc-ed Stage\Obombh\Room0\dzb\room.dzb"), Endian.Big))
            //{
            //    collision.Load(reader);
            //}
            //m_renderables.Add(collision);
          
            //m_primitiveShader = new Shader("UnlitColor");
            //m_primitiveShader.CompileSource(File.ReadAllText("Editor/Shaders/UnlitColor.vert"), ShaderType.VertexShader);
            //m_primitiveShader.CompileSource(File.ReadAllText("Editor/Shaders/UnlitColor.frag"), ShaderType.FragmentShader);
            //m_primitiveShader.LinkShader();


            //// More shitty hacks
            //Vector3[] verts = new Vector3[]
            //{
            //    new Vector3(-1f, 0f, 0f),
            //    new Vector3(1f, 0f, 0f),
            //    new Vector3(0f, 1f, 0f)
            //};


            //int[] indexes = new int[]
            //{
            //    0, 1, 2
            //};

            //GL.GenBuffers(1, out m_vbo);
            //GL.GenBuffers(1, out m_ebo);

            //// Upload Verts
            //GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * verts.Length), verts, BufferUsageHint.StaticDraw);

            //// Upload eBO
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * indexes.Length), indexes, BufferUsageHint.StaticDraw);
        }

        public void OnApplicationShutdown()
        {
            ReleaseResources();
        }

        public void ProcessTick()
        {
            GL.ClearColor(0.6f, 0.25f, 0.35f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            foreach (WWorld world in m_editorWorlds)
                world.ProcessTick();

            GL.Flush();
        }

        public void ReleaseResources()
        {
            foreach (WWorld world in m_editorWorlds)
                world.ReleaseResources();

            foreach (var item in m_renderables)
                item.ReleaseResources();
        }
    }
}
