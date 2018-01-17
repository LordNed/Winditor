using GameFormatReader.Common;
using JStudio.J3D;
using JStudio.J3D.Animation;
using JStudio.OpenGL;
using Newtonsoft.Json;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        [JsonProperty("Wait Animation Archive")]
        public string WaitAnimationArchive;

        [JsonProperty("Wait Animation")]
        public string WaitAnimation;

        [JsonProperty("Texture Animation")]
        public string TextureAnimation;
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
        private static List<TSharedRef<SimpleObjRenderer>> m_objList = new List<TSharedRef<SimpleObjRenderer>>();

        private static Dictionary<string, WActorDescriptor> m_actorDescriptors;
        private static GXLight m_mainLight;
        private static GXLight m_secondaryLight;

        static WResourceManager()
        {
            m_j3dList = new List<TSharedRef<J3D>>();
            m_objList = new List<TSharedRef<SimpleObjRenderer>>();

            // We're going to laod the actor descriptors from the json data, and then store them in a 
            // dictionary which is looked up by the Actor Name. This should give us a quick lookup time.
            m_actorDescriptors = new Dictionary<string, WActorDescriptor>();

            string jsonData = File.ReadAllText("resources/ActorDatabase.json");

            var lightPos = new Vector4(250, 200, 250, 0);
            m_mainLight = new GXLight(lightPos, -lightPos.Normalized(), new Vector4(0, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
            m_secondaryLight = new GXLight(lightPos, -lightPos.Normalized(), new Vector4(0, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
            //m_secondaryLight.Position = new Vector4(CalculateLightPosition((float)Math.PI / 2f), 0);

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

            J3D j3d = new J3D(archiveFile.Name);
            using (EndianBinaryReader reader = new EndianBinaryReader(j3dData, Endian.Big))
                j3d.LoadFromStream(reader, Properties.Settings.Default.DumpTexturesToDisk, Properties.Settings.Default.DumpShadersToDisk);

            j3d.SetHardwareLight(0, m_mainLight);
            j3d.SetHardwareLight(1, m_secondaryLight);

            // Apply patches for Wind Waker by default, since they don't seem to break anything else.
            j3d.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");
            j3d.SetTextureOverride("ZAtoon", "resources/textures/ZAtoon.png");
            j3d.SetColorWriteOverride("eyeLdamA", false);
            j3d.SetColorWriteOverride("eyeLdamB", false);
            j3d.SetColorWriteOverride("mayuLdamA", false);
            j3d.SetColorWriteOverride("mayuLdamB", false);
            j3d.SetColorWriteOverride("eyeRdamA", false);
            j3d.SetColorWriteOverride("eyeRdamB", false);
            j3d.SetColorWriteOverride("mayuRdamA", false);
            j3d.SetColorWriteOverride("mayuRdamB", false);

            existRef = new TSharedRef<J3D>();
            existRef.FilePath = actorName;
            existRef.Asset = j3d;
            existRef.ReferenceCount++;

            m_j3dList.Add(existRef);

            return j3d;
        }

        public static BCK LoadAnimationByName(string actorName)
        {
            // Check to see if we have an Actor Descriptor for this actor.
            if (!m_actorDescriptors.ContainsKey(actorName))
                return null;

            WActorDescriptor descriptor = m_actorDescriptors[actorName];

            // Check to see that this actor descriptor specifies a model path.
            if (string.IsNullOrEmpty(descriptor.ModelPath) || string.IsNullOrEmpty(descriptor.ArchiveName))
                return null;

            string archivePath = "";

            if (descriptor.WaitAnimationArchive != null)
            {
                archivePath = Path.Combine(Properties.Settings.Default.RootDirectory, "res/Object/", descriptor.WaitAnimationArchive);
            }
            else
                archivePath = Path.Combine(Properties.Settings.Default.RootDirectory, "res/Object/", descriptor.ArchiveName + ".arc");

            // Check to see that the archive exists
            if (!File.Exists(archivePath))
                return null;

            // Finally, open the archive so we can look insdie of it to see if it exists.
            VirtualFilesystemDirectory archive = ArchiveUtilities.LoadArchive(archivePath);
            VirtualFilesystemFile archiveFile = archive.GetFileAtPath(descriptor.WaitAnimation);

            if (archiveFile == null)
            {
                Console.WriteLine("LoadAnimationByName failed because the specified path \"{0}\" does not exist in archive \"{1}\"!", descriptor.WaitAnimation, descriptor.ArchiveName);
                return null;
            }

            byte[] bckData = archiveFile.Data;

            if (bckData[0] == 'Y')
            {
                using (EndianBinaryReader reader = new EndianBinaryReader(bckData, Endian.Big))
                {
                    MemoryStream decodedAnim = WArchiveTools.Compression.Yaz0.Decode(reader);
                    decodedAnim.Position = 0;
                    bckData = decodedAnim.ToArray();
                }
            }

            BCK anim = new BCK(archiveFile.Name);
            using (EndianBinaryReader reader = new EndianBinaryReader(bckData, Endian.Big))
                anim.LoadFromStream(reader);

            return anim;
        }

        public static BTK LoadTextureAnimByName(string actorName)
        {
            // Check to see if we have an Actor Descriptor for this actor.
            if (!m_actorDescriptors.ContainsKey(actorName))
                return null;

            WActorDescriptor descriptor = m_actorDescriptors[actorName];

            // Check to see that this actor descriptor specifies a model path.
            if (string.IsNullOrEmpty(descriptor.TextureAnimation) || string.IsNullOrEmpty(descriptor.ArchiveName))
                return null;

            string archivePath = Path.Combine(Properties.Settings.Default.RootDirectory, "res/Object/", descriptor.ArchiveName + ".arc");

            // Check to see that the archive exists
            if (!File.Exists(archivePath))
                return null;

            // Finally, open the archive so we can look insdie of it to see if it exists.
            VirtualFilesystemDirectory archive = ArchiveUtilities.LoadArchive(archivePath);
            VirtualFilesystemFile archiveFile = archive.GetFileAtPath(descriptor.TextureAnimation);

            if (archiveFile == null)
            {
                Console.WriteLine("LoadTextureAnimationByName failed because the specified path \"{0}\" does not exist in archive \"{1}\"!", descriptor.WaitAnimation, descriptor.ArchiveName);
                return null;
            }

            byte[] btkData = archiveFile.Data;

            if (btkData[0] == 'Y')
            {
                using (EndianBinaryReader reader = new EndianBinaryReader(btkData, Endian.Big))
                {
                    MemoryStream decodedAnim = WArchiveTools.Compression.Yaz0.Decode(reader);
                    decodedAnim.Position = 0;
                    btkData = decodedAnim.ToArray();
                }
            }

            BTK anim = new BTK(archiveFile.Name);
            using (EndianBinaryReader reader = new EndianBinaryReader(btkData, Endian.Big))
                anim.LoadFromStream(reader);

            return anim;
        }

        public static J3D LoadResource(string filePath)
        {
            var existRef = m_j3dList.Find(x => string.Compare(x.FilePath, filePath, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (existRef == null)
            {
                J3D j3d = new J3D(Path.GetFileNameWithoutExtension(filePath));
                using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(filePath), Endian.Big))
                    j3d.LoadFromStream(reader, Properties.Settings.Default.DumpTexturesToDisk, Properties.Settings.Default.DumpShadersToDisk);

                j3d.SetHardwareLight(0, m_mainLight);
                j3d.SetHardwareLight(1, m_secondaryLight);

                // Apply patches for Wind Waker by default, since they don't seem to break anything else.
                j3d.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");
                j3d.SetTextureOverride("ZAtoon", "resources/textures/ZAtoon.png");
                j3d.SetColorWriteOverride("eyeLdamA", false);
                j3d.SetColorWriteOverride("eyeLdamB", false);
                j3d.SetColorWriteOverride("mayuLdamA", false);
                j3d.SetColorWriteOverride("mayuLdamB", false);
                j3d.SetColorWriteOverride("eyeRdamA", false);
                j3d.SetColorWriteOverride("eyeRdamB", false);
                j3d.SetColorWriteOverride("mayuRdamA", false);
                j3d.SetColorWriteOverride("mayuRdamB", false);

                existRef = new TSharedRef<J3D>();
                existRef.FilePath = filePath;
                existRef.Asset = j3d;

                m_j3dList.Add(existRef);
            }

            existRef.ReferenceCount++;
            return existRef.Asset;
        }

        public static SimpleObjRenderer LoadObjResource(string filePath)
        {
            var existRef = m_objList.Find(x => string.Compare(x.FilePath, filePath, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (existRef == null)
            {
                Obj obj = new Obj();
                obj.Load(filePath);

                existRef = new TSharedRef<SimpleObjRenderer>();
                existRef.FilePath = filePath;
                existRef.Asset = new SimpleObjRenderer(obj);

                m_objList.Add(existRef);
            }

            existRef.ReferenceCount++;
            return existRef.Asset;
        }

        public static void DumpResourceStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("objList:");
            for (int i = 0; i < m_objList.Count; i++)
                sb.AppendFormat("\t{0} [{1}]\n", m_objList[i].FilePath, m_objList[i].ReferenceCount);

            sb.AppendLine();
            sb.AppendLine("j3dList:");
            for (int i = 0; i < m_j3dList.Count; i++)
                sb.AppendFormat("\t{0} [{1}]\n", m_j3dList[i].FilePath, m_j3dList[i].ReferenceCount);

            Console.WriteLine(sb.ToString());
        }

        public static void UnloadAllResources()
        {
            Console.WriteLine("ResourceManager::UnloadAllResources()");

            foreach (var j3d in m_j3dList)
                j3d.Asset.Dispose();

            foreach (var obj in m_objList)
                obj.Asset.Dispose();

            m_actorDescriptors.Clear();
        }
    }
}
