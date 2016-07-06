using GameFormatReader.Common;
using JStudio.J3D;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WArchiveTools;
using WArchiveTools.FileSystem;

namespace WindEditor
{
    public class WActorDescriptor
    {
        [JsonProperty("Archive Name")]
        public string ArchiveName;

        [JsonProperty("Actor Name")]
        public string ActorName;

        [JsonProperty("English Name")]
        public string Description;

        [JsonProperty("Main Model")]
        public string ModelPath;

        [JsonProperty("Secondary Models")]
        public string SecondaryModelPaths;
    }


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

        private static Dictionary<string, WActorDescriptor> m_actorDescriptors;

        static WResourceManager()
        {
            m_j3dList = new List<TSharedRef<J3D>>();
            m_objList = new List<TSharedRef<Obj>>();

            // We're going to laod the actor descriptors from the json data, and then store them in a 
            // dictionary which is looked up by the Actor Name. This should give us a quick lookup time.
            m_actorDescriptors = new Dictionary<string, WActorDescriptor>();

            string jsonData = File.ReadAllText("resources/ActorDatabase.json");

            WActorDescriptor[] allDescriptors = JsonConvert.DeserializeObject<WActorDescriptor[]>(jsonData);
            foreach (var descriptor in allDescriptors)
            {
                if (string.IsNullOrEmpty(descriptor.ActorName))
                    continue;

                m_actorDescriptors[descriptor.ActorName] = descriptor;
            }
        }

        public static J3D LoadActorByName(string actorName)
        {
            // Stop to check if we've already loaded this model.
            var existRef = m_j3dList.Find(x => string.Compare(x.FilePath, actorName, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (existRef != null)
            {
                existRef.ReferenceCount++;
                return existRef.Asset;
            }

            // Check to see if we have an Actor Descriptor for this actor.
            if (!m_actorDescriptors.ContainsKey(actorName))
                return null;

            WActorDescriptor descriptor = m_actorDescriptors[actorName];

            // Check to see that this actor descriptor specifies a model path.
            if (string.IsNullOrEmpty(descriptor.ModelPath) || string.IsNullOrEmpty(descriptor.ArchiveName))
                return null;

            string archivePath = Path.Combine(Properties.Settings.Default.RootDirectory, "res/Object/", descriptor.ArchiveName + ".arc");

            // Check to see that the archive exists
            if (!File.Exists(archivePath))
                return null;

            // Finally, open the archive so we can look insdie of it to see if it exists.
            VirtualFilesystemDirectory archive = ArchiveUtilities.LoadArchive(archivePath);
            VirtualFilesystemFile archiveFile = archive.GetFileAtPath(descriptor.ModelPath);

            if (archiveFile == null)
            {
                Console.WriteLine("LoadActorByName failed because the specified path \"{0}\" does not exist in archive \"{1}\"!", descriptor.ModelPath, descriptor.ArchiveName);
                return null;
            }

            // Now that we finally have the file, we can try to load a J3D from it.
            byte[] j3dData = archiveFile.Data;

            J3D j3d = new J3D();
            using (EndianBinaryReader reader = new EndianBinaryReader(j3dData, Endian.Big))
                j3d.LoadFromStream(reader, Properties.Settings.Default.DumpTexturesToDisk, Properties.Settings.Default.DumpShadersToDisk);

            existRef = new TSharedRef<J3D>();
            existRef.FilePath = actorName;
            existRef.Asset = j3d;
            existRef.ReferenceCount++;

            return j3d;
        }

        public static J3D LoadResource(string filePath)
        {
            var existRef = m_j3dList.Find(x => string.Compare(x.FilePath, filePath, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (existRef == null)
            {
                J3D j3d = new J3D();
                using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(filePath), Endian.Big))
                    j3d.LoadFromStream(reader, Properties.Settings.Default.DumpTexturesToDisk, Properties.Settings.Default.DumpShadersToDisk);

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

        public static void UnloadAllResources()
        {
            foreach (var j3d in m_j3dList)
                //j3d.Asset.Dispose();

                foreach (var obj in m_objList)
                    //obj.Asset.Dispose();

                    m_actorDescriptors.Clear();
        }
    }
}
