using GameFormatReader.Common;
using JStudio.J3D;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindEditor
{
    public static class WResourceManager
    {
        class TSharedRef
        {
            public string FilePath;
            public J3D Asset;
            public int ReferenceCount;
        }

        private static List<TSharedRef> m_resourceList = new List<TSharedRef>();

        public static J3D LoadResource(string filePath)
        {
            TSharedRef existRef = m_resourceList.Find(x => string.Compare(x.FilePath, filePath, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (existRef == null)
            {
                J3D j3d = new J3D();
                j3d.LoadFromStream(new EndianBinaryReader(File.ReadAllBytes(filePath), Endian.Big));
                existRef = new TSharedRef();
                existRef.FilePath = filePath;
                existRef.Asset = j3d;
            }

            existRef.ReferenceCount++;
            return existRef.Asset;
        }
    }
}
