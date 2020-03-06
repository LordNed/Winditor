using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Util;
using GameFormatReader.Common;
using OpenTK;
using Assimp;
using SuperBMDLib.BMD;
using SuperBMDLib.Rigging;
using BrawlLib.Modeling.Triangle_Converter;
using System.Diagnostics;

namespace SuperBMDLib.Geometry
{
    public class Shape
    {
        public VertexData AttributeData { get; private set; }
        public ShapeVertexDescriptor Descriptor { get; private set; }

        public byte MatrixType { get; private set; }
        public BoundingVolume Bounds { get; private set; }

        public List<Packet> Packets { get; private set; }

        private Vector4[] m_PositionMatrices;
        private Vector4[] m_NormalMatrices;

        // The maximum number of unique vertex weights that can be in a single shape packet without causing visual errors.
        private const int MaxMatricesPerPacket = 10;

        public Shape()
        {
            MatrixType = 3;
            AttributeData = new VertexData();
            Descriptor = new ShapeVertexDescriptor();
            Packets = new List<Packet>();
            Bounds = new BoundingVolume();

            m_PositionMatrices = new Vector4[64];
            m_NormalMatrices = new Vector4[32];
        }

        public Shape(ShapeVertexDescriptor desc, BoundingVolume bounds, List<Packet> prims, int matrixType)
        {
            Descriptor = desc;
            Bounds = bounds;
            Packets = prims;
            MatrixType = (byte)matrixType;
        }

        public void SetDescriptorAttributes(Mesh mesh, int jointCount)
        {
            int indexOffset = 0;

            if (jointCount > 1)
                Descriptor.SetAttribute(Enums.GXVertexAttribute.PositionMatrixIdx, Enums.VertexInputType.Direct, indexOffset++);

            if (mesh.HasVertices)
                Descriptor.SetAttribute(Enums.GXVertexAttribute.Position, Enums.VertexInputType.Index16, indexOffset++);
            if (mesh.HasNormals)
                Descriptor.SetAttribute(Enums.GXVertexAttribute.Normal, Enums.VertexInputType.Index16, indexOffset++);
            for (int i = 0; i < 2; i++)
            {
                if (mesh.HasVertexColors(i))
                    Descriptor.SetAttribute(Enums.GXVertexAttribute.Color0 + i, Enums.VertexInputType.Index16, indexOffset++);
            }

            for (int i = 0; i < 8; i++)
            {
                if (mesh.HasTextureCoords(i))
                    Descriptor.SetAttribute(Enums.GXVertexAttribute.Tex0 + i, Enums.VertexInputType.Index16, indexOffset++);
            }
        }

        uint[] MakeTriIndexList(Mesh mesh) {
            uint[] triindices = new uint[mesh.Faces.Count * 3];

            int i = 0;
            foreach (Face face in mesh.Faces) {
                for (int j = 0; j < 3; j++) {
                    if (face.Indices.Count < 3) {
                        throw new System.Exception(
                            String.Format(
                                "Edge No. {0} in mesh {1} has less than 3 vertices (loose vertex or edge). " +
                                "You need to remove it.", i, mesh.Name)
                            );
                    }
                    triindices[i * 3 + j] = (uint)face.Indices[2-j];
                }

                i += 1;
            }
            return triindices;
        }

        public void ProcessVerticesWithoutWeights(Mesh mesh, VertexData vertData)
        {
            Packet pack = new Packet();

            
            List<Enums.GXVertexAttribute> activeAttribs = Descriptor.GetActiveAttributes();
            AttributeData.SetAttributesFromList(activeAttribs);

            //Console.WriteLine("Calculating triangle strips");

            uint[] triindices = MakeTriIndexList(mesh);
            TriStripper stripper = new TriStripper(triindices);
            List<PrimitiveBrawl> primlist = stripper.Strip();

            //Console.WriteLine(String.Format("Done, {0} primitives", primlist.Count));

            foreach (PrimitiveBrawl primbrawl in primlist)
            {
                //Primitive prim = new Primitive(Enums.GXPrimitiveType.TriangleStrip);
                Primitive prim = new Primitive((Enums.GXPrimitiveType)primbrawl.Type);
                //Console.WriteLine(String.Format("Primitive type {0}", (Enums.GXPrimitiveType)primbrawl.Type));
                foreach (int vertIndex in primbrawl.Indices)
                {
                    Vertex vert = new Vertex();

                    Weight rootWeight = new Weight();
                    rootWeight.AddWeight(1.0f, 0);

                    vert.SetWeight(rootWeight);
                    //int vertIndex = face.Indices[i];

                    foreach (Enums.GXVertexAttribute attrib in activeAttribs) {
                        switch (attrib) {
                            case Enums.GXVertexAttribute.Position:
                                List<Vector3> posData = (List<Vector3>)vertData.GetAttributeData(Enums.GXVertexAttribute.Position);
                                Vector3 vertPos = mesh.Vertices[vertIndex].ToOpenTKVector3();

                                if (!posData.Contains(vertPos))
                                    posData.Add(vertPos);
                                AttributeData.Positions.Add(vertPos);

                                vert.SetAttributeIndex(Enums.GXVertexAttribute.Position, (uint)posData.IndexOf(vertPos));
                                break;
                            case Enums.GXVertexAttribute.Normal:
                                List<Vector3> normData = (List<Vector3>)vertData.GetAttributeData(Enums.GXVertexAttribute.Normal);
                                Vector3 vertNrm = mesh.Normals[vertIndex].ToOpenTKVector3();

                                if (!normData.Contains(vertNrm))
                                    normData.Add(vertNrm);
                                AttributeData.Normals.Add(vertNrm);

                                vert.SetAttributeIndex(Enums.GXVertexAttribute.Normal, (uint)normData.IndexOf(vertNrm));
                                break;
                            case Enums.GXVertexAttribute.Color0:
                            case Enums.GXVertexAttribute.Color1:
                                int colNo = (int)attrib - 11;
                                List<Color> colData = (List<Color>)vertData.GetAttributeData(Enums.GXVertexAttribute.Color0 + colNo);
                                Color vertCol = mesh.VertexColorChannels[colNo][vertIndex].ToSuperBMDColorRGBA();

                                
                                if (colNo == 0)
                                    AttributeData.Color_0.Add(vertCol);
                                else
                                    AttributeData.Color_1.Add(vertCol);
                                

                                vert.SetAttributeIndex(Enums.GXVertexAttribute.Color0 + colNo, (uint)colData.IndexOf(vertCol));
                                break;
                            case Enums.GXVertexAttribute.Tex0:
                            case Enums.GXVertexAttribute.Tex1:
                            case Enums.GXVertexAttribute.Tex2:
                            case Enums.GXVertexAttribute.Tex3:
                            case Enums.GXVertexAttribute.Tex4:
                            case Enums.GXVertexAttribute.Tex5:
                            case Enums.GXVertexAttribute.Tex6:
                            case Enums.GXVertexAttribute.Tex7:
                                int texNo = (int)attrib - 13;
                                List<Vector2> texCoordData = (List<Vector2>)vertData.GetAttributeData(Enums.GXVertexAttribute.Tex0 + texNo);
                                Vector2 vertTexCoord = mesh.TextureCoordinateChannels[texNo][vertIndex].ToOpenTKVector2();
                                vertTexCoord = new Vector2(vertTexCoord.X, 1.0f - vertTexCoord.Y);

 
                                switch (texNo) {
                                    case 0:
                                        AttributeData.TexCoord_0.Add(vertTexCoord);
                                        break;
                                    case 1:
                                        AttributeData.TexCoord_1.Add(vertTexCoord);
                                        break;
                                    case 2:
                                        AttributeData.TexCoord_2.Add(vertTexCoord);
                                        break;
                                    case 3:
                                        AttributeData.TexCoord_3.Add(vertTexCoord);
                                        break;
                                    case 4:
                                        AttributeData.TexCoord_4.Add(vertTexCoord);
                                        break;
                                    case 5:
                                        AttributeData.TexCoord_5.Add(vertTexCoord);
                                        break;
                                    case 6:
                                        AttributeData.TexCoord_6.Add(vertTexCoord);
                                        break;
                                    case 7:
                                        AttributeData.TexCoord_7.Add(vertTexCoord);
                                        break;
                                }

                                vert.SetAttributeIndex(Enums.GXVertexAttribute.Tex0 + texNo, (uint)texCoordData.IndexOf(vertTexCoord));
                                break;
                        }
                    }

                    //triindices[vertIndex] = 1;
                    prim.Vertices.Add(vert);
                }

                pack.Primitives.Add(prim);
            }

            
            pack.MatrixIndices.Add(0);
            Packets.Add(pack);

            Bounds.GetBoundsValues(AttributeData.Positions);
        }

        public void ProcessVerticesWithWeights(Mesh mesh, VertexData vertData, Dictionary<string, int> boneNames, EVP1 envelopes, DRW1 partialWeight, bool doStrip = true)
        {
            Weight[] weights = new Weight[mesh.Vertices.Count];

            for (int i = 0; i < mesh.Vertices.Count; i++) {
                int vertexid = i;
                Weight vertWeight = new Weight();

                foreach (Assimp.Bone bone in mesh.Bones) {
                    foreach (VertexWeight weight in bone.VertexWeights) {
                        if (weight.VertexID == vertexid)
                            vertWeight.AddWeight(weight.Weight, boneNames[bone.Name]);
                    }
                }
                vertWeight.reorderBones();
                weights[vertexid] = vertWeight;
            }

            //Primitive prim = new Primitive(Enums.GXPrimitiveType.Triangles);
            List<Enums.GXVertexAttribute> activeAttribs = Descriptor.GetActiveAttributes();
            AttributeData.SetAttributesFromList(activeAttribs);

            
            uint[] triindices = MakeTriIndexList(mesh);

            List<PrimitiveBrawl> primlist;

            if (doStrip) {
                //Console.WriteLine("Calculating triangle strips for Weighted");
                TriStripper stripper = new TriStripper(triindices, weights);
                primlist = stripper.Strip();
            }
            else {
                //Console.WriteLine("Calculating triangle list for Weighted");
                primlist = new List<PrimitiveBrawl>();
                PrimitiveBrawl prim = new PrimitiveBrawl(PrimType.TriangleList); // Trilist
                foreach (uint index in triindices) {
                    prim.Indices.Add(index);
                }
                primlist.Add(prim);
            }

            //Console.WriteLine(String.Format("Done, {0} primitives", primlist.Count));



            Packet pack = new Packet();
            List<Weight> packetWeights = new List<Weight>();
            int numMatrices = 0;
            foreach (PrimitiveBrawl primbrawl in primlist) {
                int numNewMatricesForFirstThreeVerts = 0;
                if (!packetWeights.Contains(weights[primbrawl.Indices[0]]))
                    numNewMatricesForFirstThreeVerts++;
                if (!packetWeights.Contains(weights[primbrawl.Indices[1]]))
                    numNewMatricesForFirstThreeVerts++;
                if (!packetWeights.Contains(weights[primbrawl.Indices[2]]))
                    numNewMatricesForFirstThreeVerts++;
                if (numMatrices + numNewMatricesForFirstThreeVerts > MaxMatricesPerPacket)
                {
                    // We won't be able to fit even the first 3 vertices of this primitive without going over the matrix limit.
                    // So we need to start a new packet.
                    packetWeights.Clear();
                    numMatrices = 0;
                    Packets.Add(pack);
                    pack = new Packet();
                }


                Primitive prim = new Primitive((Enums.GXPrimitiveType)primbrawl.Type);

                int currvert = -1;
                int maxvert = primbrawl.Indices.Count-1;
                Enums.GXPrimitiveType primtype = (Enums.GXPrimitiveType)primbrawl.Type;

                if (primtype == Enums.GXPrimitiveType.TriangleStrip) {
                    //Console.WriteLine("Doing Tristrip");
                    foreach (int vertIndex in primbrawl.Indices) {
                        currvert++;
                        Weight vertWeight = weights[vertIndex];

                        int oldmat = numMatrices;
                        if (!packetWeights.Contains(vertWeight))
                        {
                            packetWeights.Add(vertWeight);
                            numMatrices++;
                        }

                        //Console.WriteLine(String.Format("Added {0} matrices, is now {1}", numMatrices - oldmat, numMatrices));

                        // There are too many matrices, we need to create a new packet
                        if (numMatrices > MaxMatricesPerPacket) {
                            // If we break up and the resulting TriStrip becomes invalid,
                            // then we need to handle those cases.

                            //Console.WriteLine(String.Format("Breaking up because over the limit: {0}", numMatrices));

                            if (prim.PrimitiveType == Enums.GXPrimitiveType.TriangleStrip) {
                                Debug.Assert(prim.Vertices.Count >= 3);
                            }
                            else if (prim.PrimitiveType == Enums.GXPrimitiveType.Triangles) {
                                Debug.Assert(prim.Vertices.Count % 3 == 0);
                            }
                            pack.Primitives.Add(prim);


                            Primitive newprim = new Primitive(Enums.GXPrimitiveType.TriangleStrip);
                            Vertex prev3 = new Vertex(prim.Vertices[prim.Vertices.Count - 3]);
                            Vertex prev2 = new Vertex(prim.Vertices[prim.Vertices.Count - 2]);
                            Vertex prev = new Vertex(prim.Vertices[prim.Vertices.Count - 1]);
                            bool isOdd = currvert % 2 != 0;
                            if (isOdd)
                            {
                                // Need to preserve whether each vertex is even or odd inside the triangle strip.
                                // Do this by adding an extra vertex from the previous packet to the start of this one.
                                newprim.Vertices.Add(prev3);
                            }
                            newprim.Vertices.Add(prev2);
                            newprim.Vertices.Add(prev);

                            prim = newprim;

                            packetWeights.Clear();
                            numMatrices = 0;
                            Packets.Add(pack);
                            Packet oldPack = pack;
                            pack = new Packet();

                            // Calculate matrices for current packet in case we added vertices
                            foreach (Vertex vertex in prim.Vertices) {
                                if (!packetWeights.Contains(vertex.VertexWeight))
                                {
                                    packetWeights.Add(vertex.VertexWeight);
                                    numMatrices++;
                                }

                                // Re-add the matrix index for the duplicated verts to the new packet.
                                // And recalculate the matrix index index in each vert's attribute data.
                                uint oldMatrixIndexIndex = vertex.GetAttributeIndex(Enums.GXVertexAttribute.PositionMatrixIdx);
                                int matrixIndex = oldPack.MatrixIndices[(int)oldMatrixIndexIndex];

                                if (!pack.MatrixIndices.Contains(matrixIndex))
                                    pack.MatrixIndices.Add(matrixIndex);
                                vertex.SetAttributeIndex(Enums.GXVertexAttribute.PositionMatrixIdx, (uint)pack.MatrixIndices.IndexOf(matrixIndex));
                            }

                            if (!packetWeights.Contains(vertWeight))
                            {
                                packetWeights.Add(vertWeight);
                                numMatrices++;
                            }
                        }

                        Vertex vert = new Vertex();
                        Weight curWeight = vertWeight;

                        vert.SetWeight(curWeight);

                        foreach (Enums.GXVertexAttribute attrib in activeAttribs) {
                            switch (attrib) {
                                case Enums.GXVertexAttribute.PositionMatrixIdx:
                                    int newMatrixIndex = -1;

                                    if (curWeight.WeightCount == 1) {
                                        newMatrixIndex = partialWeight.MeshWeights.IndexOf(curWeight);
                                    }
                                    else {
                                        if (!envelopes.Weights.Contains(curWeight))
                                            envelopes.Weights.Add(curWeight);

                                        int envIndex = envelopes.Weights.IndexOf(curWeight);
                                        int drwIndex = partialWeight.MeshWeights.IndexOf(curWeight);

                                        if (drwIndex == -1)
                                        {
                                            throw new System.Exception($"Model has unweighted vertices in mesh \"{mesh.Name}\". Please weight all vertices to at least one bone.");
                                        }

                                        newMatrixIndex = drwIndex;
                                        partialWeight.Indices[drwIndex] = envIndex;
                                    }

                                    if (!pack.MatrixIndices.Contains(newMatrixIndex))
                                        pack.MatrixIndices.Add(newMatrixIndex);

                                    vert.SetAttributeIndex(Enums.GXVertexAttribute.PositionMatrixIdx, (uint)pack.MatrixIndices.IndexOf(newMatrixIndex));
                                    break;
                                case Enums.GXVertexAttribute.Position:
                                    List<Vector3> posData = (List<Vector3>)vertData.GetAttributeData(Enums.GXVertexAttribute.Position);
                                    Vector3 vertPos = mesh.Vertices[vertIndex].ToOpenTKVector3();

                                    if (curWeight.WeightCount == 1) {
                                        Matrix4 ibm = envelopes.InverseBindMatrices[curWeight.BoneIndices[0]];

                                        Vector3 transVec = Vector3.TransformPosition(vertPos, ibm);
                                        if (!posData.Contains(transVec))
                                            posData.Add(transVec);
                                        AttributeData.Positions.Add(transVec);
                                        vert.SetAttributeIndex(Enums.GXVertexAttribute.Position, (uint)posData.IndexOf(transVec));
                                    }
                                    else {
                                        if (!posData.Contains(vertPos))
                                            posData.Add(vertPos);
                                        AttributeData.Positions.Add(vertPos);

                                        vert.SetAttributeIndex(Enums.GXVertexAttribute.Position, (uint)posData.IndexOf(vertPos));
                                    }
                                    break;
                                case Enums.GXVertexAttribute.Normal:
                                    List<Vector3> normData = (List<Vector3>)vertData.GetAttributeData(Enums.GXVertexAttribute.Normal);
                                    Vector3 vertNrm = mesh.Normals[vertIndex].ToOpenTKVector3();

                                    if (curWeight.WeightCount == 1)
                                    {
                                        Matrix4 ibm = envelopes.InverseBindMatrices[curWeight.BoneIndices[0]];
                                        vertNrm = Vector3.TransformNormal(vertNrm, ibm);
                                        if (!normData.Contains(vertNrm))
                                            normData.Add(vertNrm);
                                    } else
                                    {
                                        if (!normData.Contains(vertNrm))
                                            normData.Add(vertNrm);
                                    }

                                    AttributeData.Normals.Add(vertNrm);
                                    vert.SetAttributeIndex(Enums.GXVertexAttribute.Normal, (uint)normData.IndexOf(vertNrm));
                                    break;
                                case Enums.GXVertexAttribute.Color0:
                                case Enums.GXVertexAttribute.Color1:
                                    int colNo = (int)attrib - 11;
                                    List<Color> colData = (List<Color>)vertData.GetAttributeData(Enums.GXVertexAttribute.Color0 + colNo);
                                    Color vertCol = mesh.VertexColorChannels[colNo][vertIndex].ToSuperBMDColorRGBA();

                                    if (colNo == 0)
                                        AttributeData.Color_0.Add(vertCol);
                                    else
                                        AttributeData.Color_1.Add(vertCol);

                                    vert.SetAttributeIndex(Enums.GXVertexAttribute.Color0 + colNo, (uint)colData.IndexOf(vertCol));
                                    break;
                                case Enums.GXVertexAttribute.Tex0:
                                case Enums.GXVertexAttribute.Tex1:
                                case Enums.GXVertexAttribute.Tex2:
                                case Enums.GXVertexAttribute.Tex3:
                                case Enums.GXVertexAttribute.Tex4:
                                case Enums.GXVertexAttribute.Tex5:
                                case Enums.GXVertexAttribute.Tex6:
                                case Enums.GXVertexAttribute.Tex7:
                                    int texNo = (int)attrib - 13;
                                    List<Vector2> texCoordData = (List<Vector2>)vertData.GetAttributeData(Enums.GXVertexAttribute.Tex0 + texNo);
                                    Vector2 vertTexCoord = mesh.TextureCoordinateChannels[texNo][vertIndex].ToOpenTKVector2();
                                    vertTexCoord = new Vector2(vertTexCoord.X, 1.0f - vertTexCoord.Y);

                                    switch (texNo) {
                                        case 0:
                                            AttributeData.TexCoord_0.Add(vertTexCoord);
                                            break;
                                        case 1:
                                            AttributeData.TexCoord_1.Add(vertTexCoord);
                                            break;
                                        case 2:
                                            AttributeData.TexCoord_2.Add(vertTexCoord);
                                            break;
                                        case 3:
                                            AttributeData.TexCoord_3.Add(vertTexCoord);
                                            break;
                                        case 4:
                                            AttributeData.TexCoord_4.Add(vertTexCoord);
                                            break;
                                        case 5:
                                            AttributeData.TexCoord_5.Add(vertTexCoord);
                                            break;
                                        case 6:
                                            AttributeData.TexCoord_6.Add(vertTexCoord);
                                            break;
                                        case 7:
                                            AttributeData.TexCoord_7.Add(vertTexCoord);
                                            break;
                                    }

                                    vert.SetAttributeIndex(Enums.GXVertexAttribute.Tex0 + texNo, (uint)texCoordData.IndexOf(vertTexCoord));
                                    break;
                            }
                        }
                        prim.Vertices.Add(vert);
                    }
                }
                else if (primtype == Enums.GXPrimitiveType.Triangles) {
                    for (int j = 0; j < primbrawl.Indices.Count / 3; j++) {
                        int vert1Index = (int)primbrawl.Indices[j*3 + 0];
                        int vert2Index = (int)primbrawl.Indices[j*3 + 1];
                        int vert3Index = (int)primbrawl.Indices[j*3 + 2];
                        Weight vert1Weight = weights[vert1Index];
                        Weight vert2Weight = weights[vert2Index];
                        Weight vert3Weight = weights[vert3Index];
                        int oldcount = numMatrices;
                        if (!packetWeights.Contains(vert1Weight))
                        {
                            packetWeights.Add(vert1Weight);
                            numMatrices++;
                        }
                        if (!packetWeights.Contains(vert2Weight))
                        {
                            packetWeights.Add(vert2Weight);
                            numMatrices++;
                        }
                        if (!packetWeights.Contains(vert3Weight))
                        {
                            packetWeights.Add(vert3Weight);
                            numMatrices++;
                        }

                        // There are too many matrices, we need to create a new packet
                        if (numMatrices > MaxMatricesPerPacket) {
                            //Console.WriteLine(String.Format("Making new packet because previous one would have {0}", numMatrices));
                            //Console.WriteLine(oldcount);
                            pack.Primitives.Add(prim);
                            Packets.Add(pack);

                            prim = new Primitive(Enums.GXPrimitiveType.Triangles);
                            pack = new Packet();

                            packetWeights.Clear();
                            numMatrices = 0;

                            if (!packetWeights.Contains(vert1Weight))
                            {
                                packetWeights.Add(vert1Weight);
                                numMatrices++;
                            }
                            if (!packetWeights.Contains(vert2Weight))
                            {
                                packetWeights.Add(vert2Weight);
                                numMatrices++;
                            }
                            if (!packetWeights.Contains(vert3Weight))
                            {
                                packetWeights.Add(vert3Weight);
                                numMatrices++;
                            }
                        }

                        int[] vertexIndexArray = new int[] { vert1Index, vert2Index, vert3Index };
                        Weight[] vertWeightArray = new Weight[] { vert1Weight, vert2Weight, vert3Weight };

                        for (int i = 0; i < 3; i++) {
                            Vertex vert = new Vertex();
                            int vertIndex = vertexIndexArray[i];
                            Weight curWeight = vertWeightArray[i];

                            vert.SetWeight(curWeight);

                            foreach (Enums.GXVertexAttribute attrib in activeAttribs) {
                                switch (attrib) {
                                    case Enums.GXVertexAttribute.PositionMatrixIdx:
                                        int newMatrixIndex = -1;

                                        if (curWeight.WeightCount == 1) {
                                            newMatrixIndex = partialWeight.MeshWeights.IndexOf(curWeight);
                                        }
                                        else {
                                            if (!envelopes.Weights.Contains(curWeight))
                                                envelopes.Weights.Add(curWeight);

                                            int envIndex = envelopes.Weights.IndexOf(curWeight);
                                            int drwIndex = partialWeight.MeshWeights.IndexOf(curWeight);

                                            if (drwIndex == -1)
                                            {
                                                throw new System.Exception($"Model has unweighted vertices in mesh \"{mesh.Name}\". Please weight all vertices to at least one bone.");
                                            }

                                            newMatrixIndex = drwIndex;
                                            partialWeight.Indices[drwIndex] = envIndex;
                                        }

                                        if (!pack.MatrixIndices.Contains(newMatrixIndex))
                                            pack.MatrixIndices.Add(newMatrixIndex);

                                        vert.SetAttributeIndex(Enums.GXVertexAttribute.PositionMatrixIdx, (uint)pack.MatrixIndices.IndexOf(newMatrixIndex));
                                        break;
                                    case Enums.GXVertexAttribute.Position:
                                        List<Vector3> posData = (List<Vector3>)vertData.GetAttributeData(Enums.GXVertexAttribute.Position);
                                        Vector3 vertPos = mesh.Vertices[vertIndex].ToOpenTKVector3();

                                        if (curWeight.WeightCount == 1) {
                                            Matrix4 ibm = envelopes.InverseBindMatrices[curWeight.BoneIndices[0]];

                                            Vector3 transVec = Vector3.TransformPosition(vertPos, ibm);
                                            if (!posData.Contains(transVec))
                                                posData.Add(transVec);
                                            AttributeData.Positions.Add(transVec);
                                            vert.SetAttributeIndex(Enums.GXVertexAttribute.Position, (uint)posData.IndexOf(transVec));
                                        }
                                        else {
                                            if (!posData.Contains(vertPos))
                                                posData.Add(vertPos);
                                            AttributeData.Positions.Add(vertPos);

                                            vert.SetAttributeIndex(Enums.GXVertexAttribute.Position, (uint)posData.IndexOf(vertPos));
                                        }
                                        break;
                                    case Enums.GXVertexAttribute.Normal:
                                        List<Vector3> normData = (List<Vector3>)vertData.GetAttributeData(Enums.GXVertexAttribute.Normal);
                                        Vector3 vertNrm = mesh.Normals[vertIndex].ToOpenTKVector3();

                                        if (curWeight.WeightCount == 1)
                                        {
                                            Matrix4 ibm = envelopes.InverseBindMatrices[curWeight.BoneIndices[0]];
                                            vertNrm = Vector3.TransformNormal(vertNrm, ibm);
                                            if (!normData.Contains(vertNrm))
                                                normData.Add(vertNrm);
                                        } else
                                        {
                                            if (!normData.Contains(vertNrm))
                                                normData.Add(vertNrm);
                                        }

                                        AttributeData.Normals.Add(vertNrm);
                                        vert.SetAttributeIndex(Enums.GXVertexAttribute.Normal, (uint)normData.IndexOf(vertNrm));
                                        break;
                                    case Enums.GXVertexAttribute.Color0:
                                    case Enums.GXVertexAttribute.Color1:
                                        int colNo = (int)attrib - 11;
                                        List<Color> colData = (List<Color>)vertData.GetAttributeData(Enums.GXVertexAttribute.Color0 + colNo);
                                        Color vertCol = mesh.VertexColorChannels[colNo][vertIndex].ToSuperBMDColorRGBA();

                                        if (colNo == 0)
                                            AttributeData.Color_0.Add(vertCol);
                                        else
                                            AttributeData.Color_1.Add(vertCol);

                                        vert.SetAttributeIndex(Enums.GXVertexAttribute.Color0 + colNo, (uint)colData.IndexOf(vertCol));
                                        break;
                                    case Enums.GXVertexAttribute.Tex0:
                                    case Enums.GXVertexAttribute.Tex1:
                                    case Enums.GXVertexAttribute.Tex2:
                                    case Enums.GXVertexAttribute.Tex3:
                                    case Enums.GXVertexAttribute.Tex4:
                                    case Enums.GXVertexAttribute.Tex5:
                                    case Enums.GXVertexAttribute.Tex6:
                                    case Enums.GXVertexAttribute.Tex7:
                                        int texNo = (int)attrib - 13;
                                        List<Vector2> texCoordData = (List<Vector2>)vertData.GetAttributeData(Enums.GXVertexAttribute.Tex0 + texNo);
                                        Vector2 vertTexCoord = mesh.TextureCoordinateChannels[texNo][vertIndex].ToOpenTKVector2();
                                        vertTexCoord = new Vector2(vertTexCoord.X, 1.0f - vertTexCoord.Y);

                                        switch (texNo) {
                                            case 0:
                                                AttributeData.TexCoord_0.Add(vertTexCoord);
                                                break;
                                            case 1:
                                                AttributeData.TexCoord_1.Add(vertTexCoord);
                                                break;
                                            case 2:
                                                AttributeData.TexCoord_2.Add(vertTexCoord);
                                                break;
                                            case 3:
                                                AttributeData.TexCoord_3.Add(vertTexCoord);
                                                break;
                                            case 4:
                                                AttributeData.TexCoord_4.Add(vertTexCoord);
                                                break;
                                            case 5:
                                                AttributeData.TexCoord_5.Add(vertTexCoord);
                                                break;
                                            case 6:
                                                AttributeData.TexCoord_6.Add(vertTexCoord);
                                                break;
                                            case 7:
                                                AttributeData.TexCoord_7.Add(vertTexCoord);
                                                break;
                                        }

                                        vert.SetAttributeIndex(Enums.GXVertexAttribute.Tex0 + texNo, (uint)texCoordData.IndexOf(vertTexCoord));
                                        break;
                                }
                            }

                            prim.Vertices.Add(vert);
                        }
                    }
                }
                /*
                if (prim.PrimitiveType == Enums.GXPrimitiveType.TriangleStrip) {
                    Debug.Assert(prim.Vertices.Count >= 3);
                }
                else if (prim.PrimitiveType == Enums.GXPrimitiveType.Triangles) {
                    Debug.Assert(prim.Vertices.Count % 3 == 0);
                }*/
                //Console.WriteLine(String.Format("We had this many matrices: {0}", numMatrices));
                pack.Primitives.Add(prim);
            }
            Packets.Add(pack);

            int mostmatrices = 0;
            if (true) {
                List<Weight> packWeights = new List<Weight>();
                foreach (Packet packet in Packets) {
                    
                    int matrices = 0;

                    foreach (Primitive prim in packet.Primitives) {
                        foreach (Vertex vert in prim.Vertices) {
                            if (!packWeights.Contains(vert.VertexWeight)) {
                                packWeights.Add(vert.VertexWeight);
                                matrices++;
                            }
                        }
                        

                        if (prim.PrimitiveType == Enums.GXPrimitiveType.TriangleStrip) {
                            Debug.Assert(prim.Vertices.Count >= 3);
                        }
                        else if (prim.PrimitiveType == Enums.GXPrimitiveType.Triangles) {
                            Debug.Assert(prim.Vertices.Count % 3 == 0);
                        }
                    }
                    if (matrices > mostmatrices) mostmatrices = matrices;
                    //Debug.Assert(matrices <= MaxMatricesPerPacket);
                    //Console.WriteLine(matrices);
                    packWeights.Clear();
                }
            }
            //Console.WriteLine(String.Format("Most matrices: {0}", mostmatrices));
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(MatrixType);
            writer.Write((sbyte)-1);
            writer.Write((short)Packets.Count);
            writer.Write((short)0); // Placeholder for descriptor offset
            writer.Write((short)0); // Placeholder for starting packet index
            writer.Write((short)0); // Placeholder for starting packet matrix index offset
            writer.Write((short)-1);
            Bounds.Write(writer);
        }
    }
}
