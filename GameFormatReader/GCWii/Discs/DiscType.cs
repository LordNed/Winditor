namespace GameFormatReader.GCWii.Discs
{
    /// <summary>
    /// The type a <see cref="Disc"/> can be.
    /// </summary>
    public enum DiscType
    {
        Unknown,
        Revolution,          // 'R'
        Wii,                 // 'S'
        Gamecube,            // 'G'
        Utility,             // 'U'
        GamecubeDemo,        // 'D'
        GamecubePromotional, // 'P'
        Diagnostic,          // '0'
        Diagnostic2,         // '1'
        WiiBackup,           // '4'
        WiiFitChanInstaller, // '_'
    }
}
