namespace GameFormatReader.GCWii.Graphics.Enums
{
    /// <summary>
    /// Defines possible texture formats on the GameCube and Wii.
    /// </summary>
    public enum TextureFormat
    {
        /// <summary>4-bit intensity, 8x8 tiles</summary>
        I4     = 0,
        /// <summary>8-bit intensity, 8x4 tiles</summary>
        I8     = 1,
        /// <summary>4-bit intensity with 4-bit alpha, 8x4 tiles</summary>
        IA4    = 2,
        /// <summary>8-bit intensity with 8-bit alpha, 8x8 tiles</summary>
        IA8    = 3,
        /// <summary>4x4 tiles</summary>
        RGB565 = 4,
        /// <summary>4x4 tiles - is RGB5 if color value is negative and RGB4A3 otherwise.</summary>
        RGB5A3 = 5,
        /// <summary>4x4 tiles in two cache lines - first is AR and second is GB</summary>
        RGBA8  = 6,
        /// <summary>4-bit color index, 8x8 tiles</summary>
        CI4    = 8,
        /// <summary>8-bit color index, 8x4 tiles</summary>
        CI8    = 9,
        /// <summary>14-bit color index, 4x4 tiles</summary>
        CI14X2 = 10,
        /// <summary>S3TC compressed, 2x2 blocks of 4x4 tiles</summary>
        CMP    = 14,
    }
}
