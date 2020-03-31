namespace GameFormatReader.GCWii.Graphics.Enums
{
    /// <summary>
    /// Defines possible texture wrap modes
    /// </summary>
    public enum WrapMode
    {
        /// <summary>
        /// The coordinate will simply be clamped between 0 and 1.
        /// </summary>
        Clamp  = 0,

        /// <summary>
        /// Integer part of the coordinate will be ignored and a repeating pattern is formed.
        /// </summary>
        Repeat = 1,

        /// <summary>
        /// Integer part of the coordinate will be ignored, but it 
        /// will be mirrored when the integer part of the coordinate is odd.
        /// </summary>
        Mirror = 2,
    }
}
