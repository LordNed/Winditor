using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace WindEditor
{
    public class Obj
    {
        public class Object
        {
            public string Name { get; protected set; }
            public ObjMaterial Material { get; set; }
            public List<ObjFace> Faces { get; set; }

            public List<Vector3> Positions { get; protected set; }
            public List<Vector3> Normals { get; protected set; }
            public List<Vector2> TexCoords { get; protected set; }
            public List<Vector4> Colors { get; protected set; }

            public Object()
            {
                Positions = new List<Vector3>();
                Normals = new List<Vector3>();
                TexCoords = new List<Vector2>();
                Colors = new List<Vector4>();
            }

            public Object(string name)
            {
                Name = name;
            }

            public void BuildFacesFromData()
            {
                List<ObjFace> faces = new List<ObjFace>();
                for (int i = 0; i < Positions.Count; i += 3)
                {
                    ObjFace face = new ObjFace();
                    bool hasTexCoords = TexCoords.Count > 0;
                    bool hasNormal = Normals.Count > 0;

                    face.Positions = new int[3];
                    for (int j = 0; j < 3; j++)
                        face.Positions[j] = i + 1;

                    if (hasTexCoords)
                    {
                        face.TexCoords = new int[3];
                        for (int j = 0; j < 3; j++)
                            face.TexCoords[j] = i + 1;
                    }

                    if (hasNormal)
                    {
                        face.Normals = new int[3];
                        for (int j = 0; j < 3; j++)
                            face.Normals[j] = i + 1;
                    }
                }
            }
        }

        public class ObjFace
        {
            public int[] Positions;
            public int[] Normals;
            public int[] TexCoords;
        }

        public class ObjMaterial
        {
            public string Name { get; protected set; }
            public WLinearColor AmbientColor { get; set; }
            public WLinearColor DiffuseColor { get; set; }
            public WLinearColor SpecularColor { get; set; }

            public Bitmap AmbientTexture { get; set; }
            public Bitmap DiffuseTexture { get; set; }
            public Bitmap SpecularColorTexture { get; set; }
            public Bitmap SpecularHighlightTexture { get; set; }
            public Bitmap AlphaTexture { get; set; }
            public Bitmap BumpTexture { get; set; }

            public ObjMaterial(string name)
            {
                Name = name;
                AmbientColor = DiffuseColor = WLinearColor.White;
                SpecularColor = WLinearColor.Black;
            }
        }

        public class MaterialLibrary
        {
            public string Name { get; protected set; }
            public List<ObjMaterial> Materials { get; protected set; }

            public MaterialLibrary(string name)
            {
                Name = name;
                Materials = new List<ObjMaterial>();
            }
        }

        public List<Vector3> Vertices;
        public List<Vector2> TexCoords;
        public List<ObjFace> Faces;
        public List<Vector3> Normals;


        public ObjMaterial Material;

        public void Load(string filePath)
        {
            Vertices = new List<Vector3>();
            Normals = new List<Vector3>();
            TexCoords = new List<Vector2>();
            Faces = new List<ObjFace>();

            string folderPath = Path.GetDirectoryName(filePath);

            var input = File.ReadLines(filePath);
            foreach (string line in input)
            {
                ProcessLine(line, folderPath);
            }
        }

        private void ProcessLine(string line, string folderPath)
        {
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "mtllib":
                        Material = ProcessMaterial(parts[1], folderPath);
                        break;

                    case "v":
                        Vector3 vertex = ProcessVertex(parts);
                        Vertices.Add(vertex);
                        break;

                    case "vt":
                        Vector2 texCoord = ProcessTextureCoordinate(parts);
                        TexCoords.Add(texCoord);
                        break;

                    case "vn":
                        Vector3 normal = ProcessVertex(parts);
                        Normals.Add(normal);
                        break;

                    case "f":
                        ObjFace face = ProcessFace(parts);
                        Faces.Add(face);
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

        private ObjFace ProcessFace(string[] data)
        {
            ObjFace face = new ObjFace();

            for (int i = 0; i < 3; i++)
            {
                string[] faceData = data[i + 1].Split(new[] { '/' });

                // Ensure we've allocated arrays for this type of data.
                if (faceData.Length >= 1) // Position only
                    if (face.Positions == null) face.Positions = new int[3];
                if (faceData.Length >= 2 && faceData[1] != "") // Potentially no Texcoord
                    if (face.TexCoords == null) face.TexCoords = new int[3];
                if (faceData.Length >= 3) // Position + Normal, Potentially no Texcoord
                    if (face.Normals == null) face.Normals = new int[3];

                if (faceData.Length >= 1)
                {
                    int posIndex;
                    bool bHasPos = int.TryParse(faceData[0], out posIndex);
                    if (bHasPos) face.Positions[i] = posIndex - 1;
                }
                if (faceData.Length >= 2)
                {
                    int texCoordIndex;
                    bool bHasTexcoord = int.TryParse(faceData[1], out texCoordIndex);
                    if (bHasTexcoord) face.TexCoords[i] = texCoordIndex - 1;
                }
                if (faceData.Length >= 3)
                {
                    int normalIndex;
                    bool bHasNormal = int.TryParse(faceData[2], out normalIndex);
                    if (bHasNormal) face.Normals[i] = normalIndex - 1;
                }
            }

            return face;
        }

        private ObjMaterial ProcessMaterial(string mtlName, string folderPath)
        {
            ObjMaterial material = null;

            string filePath = Path.Combine(folderPath, mtlName);

            var input = File.ReadLines(filePath);
            foreach (string line in input)
            {
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    switch (parts[0].Trim('\t', ' '))
                    {
                        case "newmtl":
                            material = new ObjMaterial(parts[1]);
                            break;
                        case "map_Kd":
                            string textureName = parts[1];
                            material.DiffuseTexture = new Bitmap(Path.Combine(folderPath, textureName));
                            break;
                    }
                }
            }

            return material;
        }
    }
}
