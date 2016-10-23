using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JStudio.J3D;
using WindEditor;
using OpenTK;

namespace JStudio.Framework
{
    public static class ObjExporter
    {
        public static void Export(J3D.J3D file, string toFilePath)
        {
            Obj outputObj = new Obj();
            List<Obj.Object> objObjects = new List<Obj.Object>();

            
            foreach(var shape in file.SHP1Tag.Shapes)
            {
                // We can read the Position/UV/Normal data off of the shapes here, but we don't know which
                // material they belong to yet unless we iterate through the J3D's scene graph.
                Obj.Object objShape = new Obj.Object();
                objShape.Positions.AddRange(shape.VertexData.Position);
                objShape.Normals.AddRange(shape.VertexData.Normal);
                objShape.TexCoords.AddRange(shape.VertexData.Tex0);

                for(int i = 0; i < shape.VertexData.Color0.Count;i++)
                {
                    WLinearColor col = shape.VertexData.Color0[i];
                    objShape.Colors.Add(new Vector4(col.R, col.G, col.B, col.A));
                }

                objShape.BuildFacesFromData();
                objObjects.Add(objShape);
            }

            // Create a material for each material in the J3D file.
            Obj.MaterialLibrary objMaterialLibrary = new Obj.MaterialLibrary(file.Name);

            foreach(var mat in file.MAT3Tag.MaterialList)
            {
                Obj.ObjMaterial objMaterial = new Obj.ObjMaterial(mat.Name);

                // Because of the complexity of TEV Materials and their multi-inputs, we don't assign
                // anything to the Ambient/Diffuse/Specular colors of the material. Instead we leave them
                // at their defaults and just assign a Diffuse texture since it is also the only one we can
                // reliably determine from a TEV material. We also are forced to use the first TextureIndex,
                // even though there can be up to 8.

                int textureIndex = mat.TextureIndexes[0];
                if (textureIndex < 0)
                    continue;

                Texture texture = file.TEX1Tag.Textures[textureIndex];
                var textureBitmap = texture.CompressedData.CreateBitmap();

                objMaterial.AmbientTexture = objMaterial.DiffuseTexture = textureBitmap;

                // Add it to our Obj Material Library
                objMaterialLibrary.Materials.Add(objMaterial);
            }

            // Now we have to do a custom iteration through the SceneGraph of the J3D file to assign
            // Materials to Objects.
        }
    }
}
