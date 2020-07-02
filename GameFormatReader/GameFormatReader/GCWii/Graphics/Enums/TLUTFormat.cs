namespace GameFormatReader.GCWii.Graphics.Enums
{
    /// <summary>
    /// Defines possible texture lookup table formats.
    /// </summary>
    public enum TLUTFormat
    {
        /// <summary>8-bit intensity with 8-bit alpha, 8x8 tiles</summary>
        IA8    = 0,
        /// <summary>4x4 tiles</summary>
        RGB565 = 1,
        /// <summary>4x4 tiles - is RGB5 if color value is negative and RGB4A3 otherwise.</summary>
        RGB5A3 = 2,
    }
}
