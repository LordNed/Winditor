using JStudio.OpenGL;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace JStudio.J3D
{
    public class VertexDescription
    {
        private List<ShaderAttributeIds> m_enabledAttributes;

        public VertexDescription()
        {
            m_enabledAttributes = new List<ShaderAttributeIds>();
        }

        internal bool AttributeIsEnabled(ShaderAttributeIds attribute)
        {
            return m_enabledAttributes.Contains(attribute);
        }

        internal int GetAttributeSize(ShaderAttributeIds attribute)
        {
            switch (attribute)
            {
                case ShaderAttributeIds.Position:
                case ShaderAttributeIds.Normal:
                    return 3;
                case ShaderAttributeIds.Color0:
                case ShaderAttributeIds.Color1:
                    return 4;
                case ShaderAttributeIds.Tex0:
                case ShaderAttributeIds.Tex1:
                    return 2;
                default:
                    return 0;
            }
        }

        internal VertexAttribPointerType GetAttributePointerType(ShaderAttributeIds attribute)
        {
            switch (attribute)
            {
                case ShaderAttributeIds.Position:
                case ShaderAttributeIds.Normal:
                case ShaderAttributeIds.Binormal:
                case ShaderAttributeIds.Color0:
                case ShaderAttributeIds.Color1:
                case ShaderAttributeIds.Tex0:
                case ShaderAttributeIds.Tex1:
                case ShaderAttributeIds.Tex2:
                case ShaderAttributeIds.Tex3:
                case ShaderAttributeIds.Tex4:
                case ShaderAttributeIds.Tex5:
                case ShaderAttributeIds.Tex6:
                case ShaderAttributeIds.Tex7:
                    return VertexAttribPointerType.Float;
                case ShaderAttributeIds.PosMtxIndex:
                    return VertexAttribPointerType.Int;

                default:
                    Console.WriteLine("Unsupported ShaderAttributeId: {0}", attribute);
                    return VertexAttribPointerType.Float;
            }
        }

        internal int GetStride(ShaderAttributeIds attribute)
        {
            switch (attribute)
            {
                case ShaderAttributeIds.Position:
                case ShaderAttributeIds.Normal:
                case ShaderAttributeIds.Binormal:
                    return 4 * 3;
                case ShaderAttributeIds.Color0:
                case ShaderAttributeIds.Color1:
                    return 4 * 4;
                case ShaderAttributeIds.Tex0:
                case ShaderAttributeIds.Tex1:
                case ShaderAttributeIds.Tex2:
                case ShaderAttributeIds.Tex3:
                case ShaderAttributeIds.Tex4:
                case ShaderAttributeIds.Tex5:
                case ShaderAttributeIds.Tex6:
                case ShaderAttributeIds.Tex7:
                    return 4 * 2;
                case ShaderAttributeIds.PosMtxIndex:
                    return 4 * 1;
                default:
                    Console.WriteLine("Unsupported ShaderAttributeId: {0}", attribute);
                    return 0;
            }
        }

        internal void EnableAttribute(ShaderAttributeIds attribute)
        {
            m_enabledAttributes.Add(attribute);
        }
    }
}
