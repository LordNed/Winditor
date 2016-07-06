using GameFormatReader.Common;
using System.IO;
using WArchiveTools.Archives;

namespace BulkDataProcessingScripts
{
    class ArcExtractorDebugging
    {
        public ArcExtractorDebugging(string filePath)
        {
            Archive arc = new Archive();
            using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(filePath), Endian.Big))
            {
                var arcFolders = arc.ReadFile(reader);
            }
        }
    }
}
