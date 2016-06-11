using OpenTK.Graphics.OpenGL;

namespace JStudio.OpenGL
{
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
    }
}
