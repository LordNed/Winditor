namespace GameFormatReader.GCWii.Graphics.Enums
{
    /// <summary>
    /// Represents possible texture filtering modes on the GameCube and Wii.
    /// </summary>
    public enum TextureFilter
    {
        /// <summary>
        /// Filters using the texture element that is nearest (in Manhattan distance)
        /// to the center of the pixel being textured.
        /// </summary>
        Nearest = 0,

        /// <summary>
        /// Filters using the weighted average of the four texture elements
        /// that are closest to the center of the pixel being textured.
        /// </summary>
        Linear  = 1,

        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel
        /// being textured and uses the <see cref="Nearest"/> criterion
        /// (the texture element nearest to the center of the pixel)
        /// to produce a texture value.
        /// </summary>
        NearestMipmap = 2,

        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel
        /// being textured and uses the <see cref="Linear"/> criterion
        /// (a weighted average of the four texture elements that are closest
        ///  to the center of the pixel) to produce a texture value.
        /// </summary>
        LinearMipmapNearest = 3,

        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel
        /// being textured and uses the <see cref="Nearest"/> criterion
        /// (the texture element nearest to the center of the pixel)
        /// to produce a texture value from each mipmap.
        /// The final texture value is a weighted average of those two values.
        /// </summary>
        NearestMipmapLinear = 4,

        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel
        /// being textured and uses the <see cref="Linear"/> criterion
        /// (a weighted average of the four texture elements that are closest to the center of the pixel)
        /// to produce a texture value from each mipmap.
        /// The final texture value is a weighted average of those two values.
        /// </summary>
        LinearMipmapLinear = 5,
    }
}
