using JStudio.J3D;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Runtime.InteropServices;
using WindEditor;

namespace JStudio.OpenGL
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct GXLight
    {
        public Vector4 Position;
        public Vector4 Direction;
        public Vector4 Color;
        public Vector4 CosAtten;
        public Vector4 DistAtten;

        public GXLight(Vector4 pos, Vector4 dir, Vector4 col, Vector4 cosAtten, Vector4 distAtten)
        {
            Position = pos;
            Direction = dir;
            Color = col;
            CosAtten = cosAtten;
            DistAtten = distAtten;
        }

        public static int SizeInBytes = 16 * 5;
    }

    [Serializable] [StructLayout(LayoutKind.Sequential)]
    public struct PSBlock
    {
        // Tev Default Register Colors
        public WLinearColor Color0;
        public WLinearColor Color1;
        public WLinearColor Color2;
        public WLinearColor Color3;

        // Tev Konst Colors
        public WLinearColor kColor0;
        public WLinearColor kColor1;
        public WLinearColor kColor2;
        public WLinearColor kColor3;

        // Dimensions of the 8 textures uploaded.
        public Vector4 TexDimension0;
        public Vector4 TexDimension1;
        public Vector4 TexDimension2;
        public Vector4 TexDimension3;
        public Vector4 TexDimension4;
        public Vector4 TexDimension5;
        public Vector4 TexDimension6;
        public Vector4 TexDimension7;

        // Color of the Fog
        public WLinearColor FogColor;
    }

    public static class GXToOpenGL
    {
        public static TextureWrapMode GetWrapMode(BinaryTextureImage.WrapModes fromMode)
        {
            switch (fromMode)
            {
                case BinaryTextureImage.WrapModes.ClampToEdge: return TextureWrapMode.ClampToEdge;
                case BinaryTextureImage.WrapModes.Repeat: return TextureWrapMode.Repeat;
                case BinaryTextureImage.WrapModes.MirroredRepeat: return TextureWrapMode.MirroredRepeat;
            }

            return TextureWrapMode.Repeat;
        }

        public static TextureMinFilter GetMinFilter(BinaryTextureImage.FilterMode fromMode)
        {
            switch (fromMode)
            {
                case BinaryTextureImage.FilterMode.Nearest: return TextureMinFilter.Nearest;
                case BinaryTextureImage.FilterMode.Linear: return TextureMinFilter.Linear;
                case BinaryTextureImage.FilterMode.NearestMipmapNearest: return TextureMinFilter.NearestMipmapNearest;
                case BinaryTextureImage.FilterMode.NearestMipmapLinear: return TextureMinFilter.NearestMipmapLinear;
                case BinaryTextureImage.FilterMode.LinearMipmapNearest: return TextureMinFilter.LinearMipmapNearest;
                case BinaryTextureImage.FilterMode.LinearMipmapLinear: return TextureMinFilter.LinearMipmapLinear;
            }

            return TextureMinFilter.Nearest;
        }

        public static TextureMagFilter GetMagFilter(BinaryTextureImage.FilterMode fromMode)
        {
            switch (fromMode)
            {
                case BinaryTextureImage.FilterMode.Nearest: return TextureMagFilter.Nearest;
                case BinaryTextureImage.FilterMode.Linear: return TextureMagFilter.Linear;
            }

            return TextureMagFilter.Nearest;
        }

        public static void SetBlendState(BlendMode blendMode)
        {
            if (blendMode.Type == GXBlendMode.Blend)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(GetBlendFactorSrc(blendMode.SourceFactor), GetBlendFactorDest(blendMode.DestinationFactor));
            }
            else if (blendMode.Type == GXBlendMode.None)
            {
                GL.Disable(EnableCap.Blend);
            }
            else
            {
                // Logic, Subtract?
            }
        }

        public static BlendingFactorSrc GetBlendFactorSrc(GXBlendModeControl sourceFactor)
        {
            switch (sourceFactor)
            {
                case GXBlendModeControl.Zero: return BlendingFactorSrc.Zero;
                case GXBlendModeControl.One: return BlendingFactorSrc.One;
                case GXBlendModeControl.SrcColor: return BlendingFactorSrc.SrcColor;
                case GXBlendModeControl.InverseSrcColor: return BlendingFactorSrc.OneMinusSrcColor;
                case GXBlendModeControl.SrcAlpha: return BlendingFactorSrc.SrcAlpha;
                case GXBlendModeControl.InverseSrcAlpha: return BlendingFactorSrc.OneMinusSrcAlpha;
                case GXBlendModeControl.DstAlpha: return BlendingFactorSrc.DstAlpha;
                case GXBlendModeControl.InverseDstAlpha: return BlendingFactorSrc.OneMinusDstAlpha;
                default:
                    Console.WriteLine("Unsupported GXBlendModeControl: \"{0}\" in GetOpenGLBlendSrc!", sourceFactor);
                    return BlendingFactorSrc.SrcAlpha;

            }
        }

        public static BlendingFactorDest GetBlendFactorDest(GXBlendModeControl destinationFactor)
        {
            switch (destinationFactor)
            {
                case GXBlendModeControl.Zero: return BlendingFactorDest.Zero;
                case GXBlendModeControl.One: return BlendingFactorDest.One;
                case GXBlendModeControl.SrcColor: return BlendingFactorDest.SrcColor;
                case GXBlendModeControl.InverseSrcColor: return BlendingFactorDest.OneMinusSrcColor;
                case GXBlendModeControl.SrcAlpha: return BlendingFactorDest.SrcAlpha;
                case GXBlendModeControl.InverseSrcAlpha: return BlendingFactorDest.OneMinusSrcAlpha;
                case GXBlendModeControl.DstAlpha: return BlendingFactorDest.DstAlpha;
                case GXBlendModeControl.InverseDstAlpha: return BlendingFactorDest.OneMinusDstAlpha;
                default:
                    Console.WriteLine("Unsupported GXBlendModeControl: \"{0}\" in GetOpenGLBlendDest!", destinationFactor);
                    return BlendingFactorDest.OneMinusSrcAlpha;
            }
        }

        public static void SetCullState(GXCullMode cullState)
        {
            GL.Enable(EnableCap.CullFace);
            switch (cullState)
            {
                case GXCullMode.None: GL.Disable(EnableCap.CullFace); break;
                case GXCullMode.Front: GL.CullFace(CullFaceMode.Front); break;
                case GXCullMode.Back: GL.CullFace(CullFaceMode.Back); break;
                case GXCullMode.All: GL.CullFace(CullFaceMode.FrontAndBack); break;
            }
        }

        public static void SetDepthState(ZMode depthState)
        {
            if (depthState.Enable)
            {
                GL.Enable(EnableCap.DepthTest);
                GL.DepthMask(depthState.UpdateEnable);
                switch (depthState.Function)
                {
                    case GXCompareType.Never: GL.DepthFunc(DepthFunction.Never); break;
                    case GXCompareType.Less: GL.DepthFunc(DepthFunction.Less); break;
                    case GXCompareType.Equal: GL.DepthFunc(DepthFunction.Equal); break;
                    case GXCompareType.LEqual: GL.DepthFunc(DepthFunction.Lequal); break;
                    case GXCompareType.Greater: GL.DepthFunc(DepthFunction.Gequal); break;
                    case GXCompareType.NEqual: GL.DepthFunc(DepthFunction.Notequal); break;
                    case GXCompareType.GEqual: GL.DepthFunc(DepthFunction.Gequal); break;
                    case GXCompareType.Always: GL.DepthFunc(DepthFunction.Always); break;
                    default: Console.WriteLine("Unsupported GXCompareType: \"{0}\" in GetOpenGLDepthFunc!", depthState.Function); break;
                }
            }
            else
            {
                GL.Disable(EnableCap.DepthTest);
                GL.DepthMask(false);
            }
        }

        public static void SetDitherEnabled(bool ditherEnabled)
        {
            if (ditherEnabled)
                GL.Enable(EnableCap.Dither);
            else
                GL.Disable(EnableCap.Dither);
        }
    }
}
