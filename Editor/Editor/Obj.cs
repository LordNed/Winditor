using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;

namespace Editor
{
    class Obj
    {
        class ObjFace
        {
            public int[] Positions;
            public int[] Normals;
            public int[] TexCoords;
        }

        public List<Vector3> Vertices;
        public List<Vector2> TexCoords;

        //public ObjMaterial Material;

        public void Load(string filePath)
        {
            Vertices = new List<Vector3>();
            TexCoords = new List<Vector2>();

            var input = File.ReadLines(filePath);
            foreach(string line in input)
            {
                ProcessLine(line);
            }
        }

        private void ProcessLine(string line)
        {
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length > 0)
            {
                switch(parts[0])
                {
                    case "mtllib":
                        //Material = new ObjMaterial();
                        //Material.Load(parts[1]);
                        break;

                    case "v":
                        Vector3 vertex = ProcessVertex(parts);
                        Vertices.Add(vertex);
                        break;

                    case "f":
                        // face
                        break;

                    case "vt":
                        Vector2 texCoord = ProcessTextureCoordinate(parts);
                        TexCoords.Add(texCoord);
                        break;
                }
            }
        }

        private Vector3 ProcessVertex(string[] data)
        {
            Vector3 vert = new Vector3();
            float.TryParse(data[1], out vert.X);
            float.TryParse(data[2], out vert.Y);
            float.TryParse(data[3], out vert.Z);

            return vert;
        }

        private Vector2 ProcessTextureCoordinate(string[] data)
        {
            Vector2 texCoord = new Vector2();
            float.TryParse(data[1], out texCoord.X);
            float.TryParse(data[2], out texCoord.Y);

            return texCoord;
        }

        private ObjFace ProcessFace(string [] data)
        {
            ObjFace face = new ObjFace();
            face.Positions = new int[3];


            for(int i = 0; i < 3; i++)
            {
                string[] faceData = data[i + 1].Split(new[] { '/' });
            }

            return face;
        }
    }
}
