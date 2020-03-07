using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using Assimp;
using System.IO;
using SuperBMDLib.BMD;
using System.Text.RegularExpressions;

namespace SuperBMDLib
{
    public class Model
    {
        public INF1 Scenegraph        { get; private set; }
        public VTX1 VertexData        { get; private set; }
        public EVP1 SkinningEnvelopes { get; private set; }
        public DRW1 PartialWeightData { get; private set; }
        public JNT1 Joints            { get; private set; }
        public SHP1 Shapes            { get; private set; }
        public MAT3 Materials         { get; private set; }
        public MDL3 MatDisplayList    { get; private set; }
        public TEX1 Textures          { get; private set; }

        private int packetCount;
        private int vertexCount;

        public static Model Load(Arguments args)
        {
            string extension = Path.GetExtension(args.input_path);
            Model output = null;

            if (extension == ".bmd" || extension == ".bdl")
            {
                using (FileStream str = new FileStream(args.input_path, FileMode.Open, FileAccess.Read))
                {
                    EndianBinaryReader reader = new EndianBinaryReader(str, Endian.Big);
                    output = new Model(reader, args);
                }
            }
            else
            {
                Assimp.AssimpContext cont = new Assimp.AssimpContext();

                // AssImp adds dummy nodes for pivots from FBX, so we'll force them off
                cont.SetConfig(new Assimp.Configs.FBXPreservePivotsConfig(false));

                Assimp.PostProcessSteps postprocess = Assimp.PostProcessSteps.Triangulate | Assimp.PostProcessSteps.JoinIdenticalVertices;
                
                if (args.tristrip_mode == "none") {
                    // By not joining identical vertices, the Tri Strip algorithm we use cannot make tristrips, 
                    // effectively disabling tri stripping
                    postprocess = Assimp.PostProcessSteps.Triangulate; 
                }
                Assimp.Scene aiScene = cont.ImportFile(args.input_path, postprocess);

                output = new Model(aiScene, args);
            }

            return output;
        }

        public Model(EndianBinaryReader reader, Arguments args)
        {
            int j3d2Magic = reader.ReadInt32();
            int modelMagic = reader.ReadInt32();

            if (j3d2Magic != 0x4A334432)
                throw new Exception("Model was not a BMD or BDL! (J3D2 magic not found)");
            if ((modelMagic != 0x62646C34) && (modelMagic != 0x626D6433))
                throw new Exception("Model was not a BMD or BDL! (Model type was not bmd3 or bdl4)");

            int modelSize = reader.ReadInt32();
            int sectionCount = reader.ReadInt32();

            // Skip the dummy section, SVR3
            reader.Skip(16);

            Scenegraph        = new INF1(reader, 32);
            VertexData        = new VTX1(reader, (int)reader.BaseStream.Position);
            SkinningEnvelopes = new EVP1(reader, (int)reader.BaseStream.Position);
            PartialWeightData = new DRW1(reader, (int)reader.BaseStream.Position);
            Joints            = new JNT1(reader, (int)reader.BaseStream.Position);
            SkinningEnvelopes.SetInverseBindMatrices(Joints.FlatSkeleton);
            Shapes            = SHP1.Create(reader, (int)reader.BaseStream.Position);
            Shapes.SetVertexWeights(SkinningEnvelopes, PartialWeightData);
            Materials         = new MAT3(reader, (int)reader.BaseStream.Position);
            SkipMDL3(reader);
            Textures          = new TEX1(reader, (int)reader.BaseStream.Position);
            Materials.SetTextureNames(Textures);
            Materials.DumpMaterials(Path.GetDirectoryName(args.input_path));

            foreach (Geometry.Shape shape in Shapes.Shapes)
                packetCount += shape.Packets.Count;

            vertexCount = VertexData.Attributes.Positions.Count;
        }

        private void SkipMDL3(EndianBinaryReader reader)
        {
            if (reader.PeekReadInt32() == 0x4D444C33)
            {
                int mdl3Size = reader.ReadInt32At(reader.BaseStream.Position + 4);
                reader.Skip(mdl3Size);
            }
        }

        public Model(Scene scene, Arguments args)
        {
            EnsureOneMaterialPerMesh(scene);
            SortMeshesByObjectNames(scene);

            if (args.rotate_model)
            {
                RotateModel(scene);
            }

            VertexData = new VTX1(scene);
            Joints = new JNT1(scene, VertexData);
            Textures = new TEX1(scene, args);

            SkinningEnvelopes = new EVP1();
            SkinningEnvelopes.SetInverseBindMatrices(scene, Joints.FlatSkeleton);

            PartialWeightData = new DRW1(scene, Joints.BoneNameIndices);

            Shapes = SHP1.Create(scene, Joints.BoneNameIndices, VertexData.Attributes, SkinningEnvelopes, PartialWeightData, args.tristrip_mode);

            Materials = new MAT3(scene, Textures, Shapes, args);

            if (args.output_bdl)
                MatDisplayList = new MDL3(Materials.m_Materials, Textures.Textures);

            Scenegraph = new INF1(scene, Joints);

            foreach (Geometry.Shape shape in Shapes.Shapes)
                packetCount += shape.Packets.Count;

            vertexCount = VertexData.Attributes.Positions.Count;
        }

        public void ExportBMD(string fileName, bool isBDL)
        {
            string outDir = Path.GetDirectoryName(fileName);
            string fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
            fileNameNoExt = fileNameNoExt.Split('.')[0];
            if (isBDL)
            {
                fileName = Path.Combine(outDir, fileNameNoExt + ".bdl");
            } else
            {
                fileName = Path.Combine(outDir, fileNameNoExt + ".bmd");
            }

            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                EndianBinaryWriter writer = new EndianBinaryWriter(stream, Endian.Big);

                if (isBDL)
                    writer.Write("J3D2bdl4".ToCharArray());
                else
                    writer.Write("J3D2bmd3".ToCharArray());

                writer.Write(0); // Placeholder for file size

                if (isBDL)
                    writer.Write(9); // Number of sections; bmd has 8, bdl has 9
                else
                    writer.Write(8);

                writer.Write("SuperBMD - Gamma".ToCharArray());

                Scenegraph.Write(writer, packetCount, vertexCount);
                VertexData.Write(writer);
                SkinningEnvelopes.Write(writer);
                PartialWeightData.Write(writer);
                Joints.Write(writer);
                Shapes.Write(writer);
                Materials.Write(writer);

                if (isBDL)
                    MatDisplayList.Write(writer);

                Textures.Write(writer);

                writer.Seek(8, SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length);
            }
        }

        public void ExportAssImp(string fileName, string modelType, ExportSettings settings)
        {
            fileName = Path.GetFullPath(fileName); // Get absolute path instead of relative
            string outDir = Path.GetDirectoryName(fileName);
            string fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
            fileName = Path.Combine(outDir, fileNameNoExt + ".dae");

            Scene outScene = new Scene();

            outScene.RootNode = new Node("RootNode");

            Materials.FillScene(outScene, Textures, outDir);
            Shapes.FillScene(outScene, VertexData.Attributes, Joints.FlatSkeleton, SkinningEnvelopes.InverseBindMatrices);
            Scenegraph.FillScene(outScene, Joints.FlatSkeleton, settings.UseSkeletonRoot);
            Scenegraph.CorrectMaterialIndices(outScene, Materials);
            Textures.DumpTextures(outDir);


            foreach (Mesh mesh in outScene.Meshes)
            {
                // Assimp has a JoinIdenticalVertices post process step, but we can't use that or the skinning info we manually add won't take it into account.
                RemoveDuplicateVertices(mesh);
            }


            AssimpContext cont = new AssimpContext();
            cont.ExportFile(outScene, fileName, "collada", PostProcessSteps.ValidateDataStructure);


            //if (SkinningEnvelopes.Weights.Count == 0)
            //    return; // There's no skinning information, so we can stop here

            // Now we need to add some skinning info, since AssImp doesn't do it for some bizarre reason

            StreamWriter test = new StreamWriter(fileName + ".tmp");
            StreamReader dae = File.OpenText(fileName);
            
            while (!dae.EndOfStream)
            {
                string line = dae.ReadLine();

                if (line == "  <library_visual_scenes>")
                {
                    AddControllerLibrary(outScene, test);
                    test.WriteLine(line);
                    test.Flush();
                }
                else if (line.Contains("<node"))
                {
                    string[] testLn = line.Split('\"');
                    string name = testLn[3];

                    if (Joints.FlatSkeleton.Exists(x => x.Name == name))
                    {
                        string jointLine = line.Replace(">", $" sid=\"{ name }\" type=\"JOINT\">");
                        test.WriteLine(jointLine);
                        test.Flush();
                    }
                    else
                    {
                        test.WriteLine(line);
                        test.Flush();
                    }
                }
                else if (line.Contains("</visual_scene>"))
                {
                    foreach (Mesh mesh in outScene.Meshes)
                    {
                        test.WriteLine($"      <node id=\"{ mesh.Name }\" name=\"{ mesh.Name }\" type=\"NODE\">");

                        test.WriteLine($"       <instance_controller url=\"#{ mesh.Name }-skin\">");
                        test.WriteLine("        <skeleton>#skeleton_root</skeleton>");
                        test.WriteLine("        <bind_material>");
                        test.WriteLine("         <technique_common>");
                        test.WriteLine($"          <instance_material symbol=\"m{mesh.MaterialIndex}{ Materials.m_Materials[mesh.MaterialIndex].Name }\" target=\"#m{mesh.MaterialIndex}{ Materials.m_Materials[mesh.MaterialIndex].Name.Replace("(","_").Replace(")","_") }\" />");
                        test.WriteLine("         </technique_common>");
                        test.WriteLine("        </bind_material>");
                        test.WriteLine("       </instance_controller>");

                        test.WriteLine("      </node>");
                        test.Flush();
                    }

                    test.WriteLine(line);
                    test.Flush();
                }
                else if (line.Contains("<matrix"))
                {
                    string matLine = line.Replace("<matrix>", "<matrix sid=\"matrix\">");
                    test.WriteLine(matLine);
                    test.Flush();
                }
                else
                {
                    test.WriteLine(line);
                    test.Flush();
                }
            }

            test.Close();
            dae.Close();

            File.Copy(fileName + ".tmp", fileName, true);
            File.Delete(fileName + ".tmp");
        }

        private void AddControllerLibrary(Scene scene, StreamWriter writer)
        {
            writer.WriteLine("  <library_controllers>");

            for (int i = 0; i < scene.MeshCount; i++)
            {
                Mesh curMesh = scene.Meshes[i];
                curMesh.Name = curMesh.Name.Replace('_', '-');

                writer.WriteLine($"   <controller id=\"{ curMesh.Name }-skin\" name=\"{ curMesh.Name }Skin\">");

                writer.WriteLine($"    <skin source=\"#meshId{ i }\">");

                WriteBindShapeMatrixToStream(writer);
                WriteJointNameArrayToStream(curMesh, writer);
                WriteInverseBindMatricesToStream(curMesh, writer);
                WriteSkinWeightsToStream(curMesh, writer);

                writer.WriteLine("     <joints>");

                writer.WriteLine($"      <input semantic=\"JOINT\" source=\"#{ curMesh.Name }-skin-joints-array\"></input>");
                writer.WriteLine($"      <input semantic=\"INV_BIND_MATRIX\" source=\"#{ curMesh.Name }-skin-bind_poses-array\"></input>");

                writer.WriteLine("     </joints>");
                writer.Flush();

                WriteVertexWeightsToStream(curMesh, writer);

                writer.WriteLine("    </skin>");

                writer.WriteLine("   </controller>");
                writer.Flush();
            }

            writer.WriteLine("  </library_controllers>");
            writer.Flush();
        }

        private void WriteBindShapeMatrixToStream(StreamWriter writer)
        {
            writer.WriteLine("     <bind_shape_matrix>");

            writer.WriteLine("      1 0 0 0");
            writer.WriteLine("      0 1 0 0");
            writer.WriteLine("      0 0 1 0");
            writer.WriteLine("      0 0 0 1");

            writer.WriteLine("     </bind_shape_matrix>");
            writer.Flush();
        }

        private void WriteJointNameArrayToStream(Mesh mesh, StreamWriter writer)
        {
            writer.WriteLine($"      <source id =\"{ mesh.Name }-skin-joints-array\">");
            writer.WriteLine($"      <Name_array id=\"{ mesh.Name }-skin-joints-array\" count=\"{ mesh.Bones.Count }\">");

            writer.Write("       ");
            foreach (Bone bone in mesh.Bones)
            {
                writer.Write($"{ bone.Name }");
                if (bone != mesh.Bones.Last())
                    writer.Write(' ');
                else
                    writer.Write('\n');

                writer.Flush();
            }

            writer.WriteLine("      </Name_array>");
            writer.Flush();

            writer.WriteLine("      <technique_common>");
            writer.WriteLine($"       <accessor source=\"#{ mesh.Name }-skin-joints-array\" count=\"{ mesh.Bones.Count }\" stride=\"1\">");
            writer.WriteLine("         <param name=\"JOINT\" type=\"Name\"></param>");
            writer.WriteLine("       </accessor>");
            writer.WriteLine("      </technique_common>");
            writer.WriteLine("      </source>");
            writer.Flush();
        }

        private void WriteInverseBindMatricesToStream(Mesh mesh, StreamWriter writer)
        {
            writer.WriteLine($"      <source id =\"{ mesh.Name }-skin-bind_poses-array\">");
            writer.WriteLine($"      <float_array id=\"{ mesh.Name }-skin-bind_poses-array\" count=\"{ mesh.Bones.Count * 16 }\">");

            foreach (Bone bone in mesh.Bones)
            {
                Matrix4x4 ibm = bone.OffsetMatrix;
                ibm.Transpose();

                writer.WriteLine($"       {ibm.A1.ToString("F")} {ibm.A2.ToString("F")} {ibm.A3.ToString("F")} {ibm.A4.ToString("F")}");
                writer.WriteLine($"       {ibm.B1.ToString("F")} {ibm.B2.ToString("F")} {ibm.B3.ToString("F")} {ibm.B4.ToString("F")}");
                writer.WriteLine($"       {ibm.C1.ToString("F")} {ibm.C2.ToString("F")} {ibm.C3.ToString("F")} {ibm.C4.ToString("F")}");
                writer.WriteLine($"       {ibm.D1.ToString("F")} {ibm.D2.ToString("F")} {ibm.D3.ToString("F")} {ibm.D4.ToString("F")}");

                if (bone != mesh.Bones.Last())
                    writer.WriteLine("");
            }

            writer.WriteLine("      </float_array>");
            writer.Flush();

            writer.WriteLine("      <technique_common>");
            writer.WriteLine($"       <accessor source=\"#{ mesh.Name }-skin-bind_poses-array\" count=\"{ mesh.Bones.Count }\" stride=\"16\">");
            writer.WriteLine("         <param name=\"TRANSFORM\" type=\"float4x4\"></param>");
            writer.WriteLine("       </accessor>");
            writer.WriteLine("      </technique_common>");
            writer.WriteLine("      </source>");
            writer.Flush();
        }

        private void WriteSkinWeightsToStream(Mesh mesh, StreamWriter writer)
        {
            int totalWeightCount = 0;

            foreach (Bone bone in mesh.Bones)
            {
                totalWeightCount += bone.VertexWeightCount;
            }

            writer.WriteLine($"      <source id =\"{ mesh.Name }-skin-weights-array\">");
            writer.WriteLine($"      <float_array id=\"{ mesh.Name }-skin-weights-array\" count=\"{ totalWeightCount }\">");
            writer.Write("       ");

            foreach (Bone bone in mesh.Bones)
            {
                foreach (VertexWeight weight in bone.VertexWeights)
                {
                    writer.Write($"{ weight.Weight } " );
                }

                if (bone == mesh.Bones.Last())
                    writer.WriteLine();
            }

            writer.WriteLine("      </float_array>");
            writer.Flush();

            writer.WriteLine("      <technique_common>");
            writer.WriteLine($"       <accessor source=\"#{ mesh.Name }-skin-weights-array\" count=\"{ totalWeightCount }\" stride=\"1\">");
            writer.WriteLine("         <param name=\"WEIGHT\" type=\"float\"></param>");
            writer.WriteLine("       </accessor>");
            writer.WriteLine("      </technique_common>");
            writer.WriteLine("      </source>");
            writer.Flush();
        }

        private void WriteVertexWeightsToStream(Mesh mesh, StreamWriter writer)
        {
            List<float> weights = new List<float>();
            Dictionary<int, Rigging.Weight> vertIDWeights = new Dictionary<int, Rigging.Weight>();

            foreach (Bone bone in mesh.Bones)
            {
                foreach (VertexWeight weight in bone.VertexWeights)
                {
                    weights.Add(weight.Weight);

                    if (!vertIDWeights.ContainsKey(weight.VertexID))
                        vertIDWeights.Add(weight.VertexID, new Rigging.Weight());

                    vertIDWeights[weight.VertexID].AddWeight(weight.Weight, mesh.Bones.IndexOf(bone));
                }
            }

            writer.WriteLine($"      <vertex_weights count=\"{ vertIDWeights.Count }\">");

            writer.WriteLine($"       <input semantic=\"JOINT\" source=\"#{ mesh.Name }-skin-joints-array\" offset=\"0\"></input>");
            writer.WriteLine($"       <input semantic=\"WEIGHT\" source=\"#{ mesh.Name }-skin-weights-array\" offset=\"1\"></input>");

            writer.WriteLine("       <vcount>");

            writer.Write("        ");
            for (int i = 0; i < vertIDWeights.Count; i++)
                writer.Write($"{ vertIDWeights[i].WeightCount } ");

            writer.WriteLine("\n       </vcount>");

            writer.WriteLine("       <v>");
            writer.Write("        ");

            for (int i = 0; i < vertIDWeights.Count; i++)
            {
                Rigging.Weight curWeight = vertIDWeights[i];

                for (int j = 0; j < curWeight.WeightCount; j++)
                {
                    writer.Write($"{ curWeight.BoneIndices[j] } { weights.IndexOf(curWeight.Weights[j]) } ");
                }
            }

            writer.WriteLine("\n       </v>");

            writer.WriteLine($"      </vertex_weights>");
        }

        private void RemoveDuplicateVertices(Mesh mesh)
        {
            // Calculate which vertices are duplicates (based on their position, texture coordinates, and normals).
            List<Tuple<Vector3D, Vector3D?, List<Vector3D>>> uniqueVertInfos = new List<Tuple<Vector3D, Vector3D?, List<Vector3D>>>();
            int[] replaceVertexIDs = new int[mesh.Vertices.Count];
            bool[] vertexIsUnique = new bool[mesh.Vertices.Count];
            for (var origVertexID = 0; origVertexID < mesh.Vertices.Count; origVertexID++)
            {
                var coordsForVert = new List<Vector3D>();
                for (var i = 0; i < mesh.TextureCoordinateChannelCount; i++)
                {
                    coordsForVert.Add(mesh.TextureCoordinateChannels[i][origVertexID]);
                }

                Vector3D? normal;
                if (origVertexID < mesh.Normals.Count)
                {
                    normal = mesh.Normals[origVertexID];
                } else
                {
                    normal = null;
                }

                var vertInfo = new Tuple<Vector3D, Vector3D?, List<Vector3D>>(mesh.Vertices[origVertexID], normal, coordsForVert);

                // Determine if this vertex is a duplicate of a previously encountered vertex or not and if it is keep track of the new index
                var duplicateVertexIndex = -1;
                for (var i = 0; i < uniqueVertInfos.Count; i++)
                {
                    Tuple<Vector3D, Vector3D?, List<Vector3D>> otherVertInfo = uniqueVertInfos[i];
                    if (CheckVertInfosAreDuplicates(vertInfo.Item1, vertInfo.Item2, vertInfo.Item3, otherVertInfo.Item1, otherVertInfo.Item2, otherVertInfo.Item3))
                    {
                        duplicateVertexIndex = i;
                        break;
                    }
                }

                if (duplicateVertexIndex == -1)
                {
                    vertexIsUnique[origVertexID] = true;
                    uniqueVertInfos.Add(vertInfo);
                    replaceVertexIDs[origVertexID] = uniqueVertInfos.Count - 1;
                }
                else
                {
                    vertexIsUnique[origVertexID] = false;
                    replaceVertexIDs[origVertexID] = duplicateVertexIndex;
                }
            }

            // Remove duplicate vertices, normals, and texture coordinates.
            mesh.Vertices.Clear();
            mesh.Normals.Clear();
            // Need to preserve the channel count since it gets set to 0 when clearing all the channels
            int origTexCoordChannelCount = mesh.TextureCoordinateChannelCount;
            for (var i = 0; i < origTexCoordChannelCount; i++)
            {
                mesh.TextureCoordinateChannels[i].Clear();
            }
            foreach (Tuple<Vector3D, Vector3D?, List<Vector3D>> vertInfo in uniqueVertInfos)
            {
                mesh.Vertices.Add(vertInfo.Item1);
                if (vertInfo.Item2 != null)
                {
                    mesh.Normals.Add(vertInfo.Item2.Value);
                }
                for (var i = 0; i < origTexCoordChannelCount; i++)
                {
                    var coord = vertInfo.Item3[i];
                    mesh.TextureCoordinateChannels[i].Add(coord);
                }
            }

            // Update vertex indices for the faces.
            foreach (Face face in mesh.Faces)
            {
                for (var i = 0; i < face.IndexCount; i++)
                {
                    face.Indices[i] = replaceVertexIDs[face.Indices[i]];
                }
            }

            // Update vertex indices for the bone vertex weights.
            foreach (Bone bone in mesh.Bones)
            {
                List<VertexWeight> origVertexWeights = new List<VertexWeight>(bone.VertexWeights);
                bone.VertexWeights.Clear();
                for (var i = 0; i < origVertexWeights.Count; i++)
                {
                    VertexWeight origWeight = origVertexWeights[i];
                    int origVertexID = origWeight.VertexID;
                    if (!vertexIsUnique[origVertexID])
                        continue;

                    int newVertexID = replaceVertexIDs[origVertexID];
                    VertexWeight newWeight = new VertexWeight(newVertexID, origWeight.Weight);
                    bone.VertexWeights.Add(newWeight);
                }
            }
        }

        private bool CheckVertInfosAreDuplicates(Vector3D vert1, Vector3D? norm1, List<Vector3D> vert1TexCoords, Vector3D vert2, Vector3D? norm2, List<Vector3D> vert2TexCoords)
        {
            if (vert1 != vert2)
            {
                // Position is different
                return false;
            }

            if (norm1 != norm2)
            {
                // Normals are different
                return false;
            }

            for (var i = 0; i < vert1TexCoords.Count; i++)
            {
                if (vert1TexCoords[i] != vert2TexCoords[i])
                {
                    // Texture coordinate is different
                    return false;
                }
            }

            return true;
        }

        private void SortMeshesByObjectNames(Scene scene)
        {
            // Sort meshes by their name instead of keeping the order they're in inside the file.
            // Specifically, natural sorting is used so that mesh-9 comes before mesh-10.

            List<string> meshNames = new List<string>();
            int maxNumberLength = 0;
            GetMeshNamesRecursive(scene.RootNode, meshNames, ref maxNumberLength);

            if (meshNames.Count != scene.Meshes.Count)
            {
                throw new Exception($"Number of meshes ({scene.Meshes.Count}) is not the same as the number of mesh objects ({meshNames.Count}); cannot sort.\nMesh objects: {String.Join(", ", meshNames)}\nMeshes: {String.Join(", ", scene.Meshes.Select(mesh => mesh.Name))}");
            }

            // Pad the numbers in mesh names with 0s.
            List<string> meshNamesPadded = new List<string>();
            foreach (string meshName in meshNames)
            {
                meshNamesPadded.Add(Regex.Replace(meshName, @"\d+", m => m.Value.PadLeft(maxNumberLength, '0')));
            }

            // Use Array.Sort to sort the meshes by the order of their object names.
            var meshNamesArray = meshNamesPadded.ToArray();
            var meshesArray = scene.Meshes.ToArray();
            Array.Sort(meshNamesArray, meshesArray);

            for (int i = 0; i < scene.Meshes.Count; i++)
            {
                scene.Meshes[i] = meshesArray[i];
            }
        }
        
        private void GetMeshNamesRecursive(Node parentNode, List<string> meshNames, ref int maxNumberLength)
        {
            foreach (Node node in parentNode.Children)
            {
                if (node.HasMeshes)
                {
                    int currMaxNumberLength = node.Name.SelectMany(i => Regex.Matches(node.Name, @"\d+").Cast<Match>().Select(m => m.Value.Length)).DefaultIfEmpty(0).Max();
                    if (currMaxNumberLength > maxNumberLength)
                    {
                        maxNumberLength = currMaxNumberLength;
                    }
                    for (int i = 0; i < node.MeshCount; i++)
                    {
                        meshNames.Add(node.Name);
                    }
                }
                
                if (node.Children.Count > 0)
                {
                  GetMeshNamesRecursive(node, meshNames, ref maxNumberLength);
                }
            }
        }

        private void EnsureOneMaterialPerMesh(Scene scene)
        {
            foreach (Mesh mesh1 in scene.Meshes)
            {
                foreach (Mesh mesh2 in scene.Meshes)
                {
                    if (mesh1.Name == mesh2.Name && mesh1.MaterialIndex != mesh2.MaterialIndex)
                    {
                        throw new Exception($"Mesh \"{mesh1.Name}\" has more than one material assigned to it. Currently only one material per mesh is supported.");
                    }
                }
            }
        }

        private void RotateModel(Scene scene)
        {
            Assimp.Node root = null;
            for (int i = 0; i < scene.RootNode.ChildCount; i++)
            {
                if (scene.RootNode.Children[i].Name.ToLowerInvariant() == "skeleton_root")
                {
                    if (scene.RootNode.Children[i].ChildCount == 0)
                    {
                        throw new System.Exception("skeleton_root has no children! If you are making a rigged model, make sure skeleton_root contains the root of your skeleton.");
                    }
                    root = scene.RootNode.Children[i].Children[0];
                    break;
                }
            }

            Matrix4x4 rotate = Matrix4x4.FromRotationX((float)((1 / 2.0) * Math.PI));
            Matrix4x4 rotateinv = rotate;
            rotateinv.Inverse();


            foreach (Mesh mesh in scene.Meshes)
            {
                if (root != null)
                {
                    foreach (Assimp.Bone bone in mesh.Bones)
                    {
                        bone.OffsetMatrix = rotateinv * bone.OffsetMatrix;
                    }
                }

                for (int i = 0; i < mesh.VertexCount; i++)
                {
                    Vector3D vertex = mesh.Vertices[i];
                    vertex.Set(vertex.X, -vertex.Z, vertex.Y);
                    mesh.Vertices[i] = vertex;
                }
                for (int i = 0; i < mesh.Normals.Count; i++)
                {
                    Vector3D norm = mesh.Normals[i];
                    norm.Set(norm.X, -norm.Z, norm.Y);

                    mesh.Normals[i] = norm;
                }
            }
        }
    }
}
