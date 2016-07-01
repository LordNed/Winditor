using GameFormatReader.Common;
using JStudio.J3D;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindEditor
{
    public static class WResourceManager
    {
        class TSharedRef<T>
        {
            public string FilePath;
            public T Asset;
            public int ReferenceCount;
        }

        private static List<TSharedRef<J3D>> m_j3dList = new List<TSharedRef<J3D>>();
        private static List<TSharedRef<Obj>> m_objList = new List<TSharedRef<Obj>>();

        public static J3D LoadResource(string filePath)
        {
            var existRef = m_j3dList.Find(x => string.Compare(x.FilePath, filePath, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (existRef == null)
            {
                J3D j3d = new J3D();
                j3d.LoadFromStream(new EndianBinaryReader(File.ReadAllBytes(filePath), Endian.Big));
                existRef = new TSharedRef<J3D>();
                existRef.FilePath = filePath;
                existRef.Asset = j3d;

                m_j3dList.Add(existRef);
            }

            existRef.ReferenceCount++;
            return existRef.Asset;
        }

        public static Obj LoadObjResource(string filePath)
        {
            var existRef = m_objList.Find(x => string.Compare(x.FilePath, filePath, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (existRef == null)
            {
                Obj obj = new Obj();
                obj.Load(filePath);

                existRef = new TSharedRef<Obj>();
                existRef.FilePath = filePath;
                existRef.Asset = obj;

                m_objList.Add(existRef);
            }

            existRef.ReferenceCount++;
            return existRef.Asset;
        }
    }
}
