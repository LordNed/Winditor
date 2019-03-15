using GameFormatReader.Common;
using JStudio.J3D;
using JStudio.J3D.Animation;
using JStudio.JStudio.J3D.ExternalTypes;
using JStudio.OpenGL;
using Newtonsoft.Json;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WArchiveTools;
using WArchiveTools.FileSystem;
using WArchiveTools.Compression;

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

        [JsonProperty("ActorClassType")]
        public string ActorClassType;

        [JsonProperty("Wait Animations")]
        public string WaitAnimation;
    }


    public class WActorResource
    {
        public struct AnimationResource
        {
            [JsonProperty("Anim Type")]
            public string Type;

            [JsonProperty("Archive")]
            public string ArchiveName;

            [JsonProperty("Path")]
            public string Path;

            [JsonProperty("Paused on Load")]
            public bool PausedOnLoad;

            [JsonProperty("Start Time")]
            public float StartTime;
        }

        public struct ModelResource
        {
            [JsonProperty("Path")]
            public string Path;

            [JsonProperty("Offset")]
            public Vector3 Offset;

            [JsonProperty("Animations")]
            public AnimationResource[] Animations;
        }

        [JsonProperty("Name")]
        public string ResourceName;

        [JsonProperty("Archive")]
        public string ArchiveName;

        [JsonProperty("Models")]
        public ModelResource[] Models;
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
        private static List<TSharedRef<BCK>> m_bckList = new List<TSharedRef<BCK>>();
        private static List<TSharedRef<SimpleObjRenderer>> m_objList = new List<TSharedRef<SimpleObjRenderer>>();

        private static Dictionary<string, WActorDescriptor> m_actorDescriptors;
        private static Dictionary<string, WActorResource> m_actorResources;
        private static GXLight m_mainLight;
        private static GXLight m_secondaryLight;

        static WResourceManager()
        {
            m_j3dList = new List<TSharedRef<J3D>>();
            m_objList = new List<TSharedRef<SimpleObjRenderer>>();

            // We're going to laod the actor descriptors from the json data, and then store them in a 
            // dictionary which is looked up by the Actor Name. This should give us a quick lookup time.
            m_actorDescriptors = new Dictionary<string, WActorDescriptor>();
            m_actorResources = new Dictionary<string, WActorResource>();

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

            string resource_json_data = File.ReadAllText("resources/ActorResourceDatabase.json");

            WActorResource[] resources = JsonConvert.DeserializeObject<WActorResource[]>(resource_json_data);
            foreach (var res in resources)
            {
                if (string.IsNullOrEmpty(res.ResourceName))
                    continue;

                m_actorResources[res.ResourceName] = res;
            }
        }

        public static List<J3D> LoadActorResource(string name)
        {
            List<J3D> models = new List<J3D>();

            if (!m_actorResources.ContainsKey(name))
                return null;

            WActorResource res = m_actorResources[name];

            foreach (var model in res.Models)
            {
                string arc_and_file_path = Path.Combine(res.ArchiveName, model.Path);

                var existRef = m_j3dList.Find(x => string.Compare(x.FilePath, arc_and_file_path, StringComparison.InvariantCultureIgnoreCase) == 0);
                if (existRef != null)
                {
                    existRef.ReferenceCount++;
                    models.Add(existRef.Asset);

                    continue;
                }

                J3D loaded_model = LoadModelFromResource(model, res.ArchiveName);

                if (loaded_model == null)
                    continue;

                loaded_model.SetHardwareLight(0, m_mainLight);
                loaded_model.SetHardwareLight(1, m_secondaryLight);
                loaded_model.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");
                loaded_model.SetTextureOverride("ZAtoon", "resources/textures/ZAtoon.png");

                existRef = new TSharedRef<J3D>();
                existRef.FilePath = arc_and_file_path;
                existRef.Asset = loaded_model;
                existRef.ReferenceCount++;

                m_j3dList.Add(existRef);

                models.Add(loaded_model);
            }

            if (name == "Link" || name == "Tetra" || name == "Zelda")
            {
                models[0].SetColorWriteOverride("eyeLdamA", false);
                models[0].SetColorWriteOverride("eyeLdamB", false);
                models[0].SetColorWriteOverride("mayuLdamA", false);
                models[0].SetColorWriteOverride("mayuLdamB", false);
                models[0].SetColorWriteOverride("eyeRdamA", false);
                models[0].SetColorWriteOverride("eyeRdamB", false);
                models[0].SetColorWriteOverride("mayuRdamA", false);
                models[0].SetColorWriteOverride("mayuRdamB", false);
            }

            return models;
        }

        private static J3D LoadModelFromResource(WActorResource.ModelResource res, string archive)
        {
            J3D j3d = null;

            if (string.IsNullOrEmpty(res.Path) || string.IsNullOrEmpty(archive))
                return null;

            string archivePath = Path.Combine(Properties.Settings.Default.RootDirectory, "res/Object/", archive + ".arc");

            if (!File.Exists(archivePath))
                return null;

            VirtualFilesystemDirectory model_arc = ArchiveUtilities.LoadArchive(archivePath);
            VirtualFilesystemFile archiveFile = model_arc.GetFileAtPath(res.Path);

            if (archiveFile == null)
            {
                Console.WriteLine("LoadActorByName failed because the specified path \"{0}\" does not exist in archive \"{1}\"!", res.Path, archive);
                return null;
            }

            byte[] j3dData = archiveFile.Data;

            j3d = new J3D(archiveFile.Name);
            using (EndianBinaryReader reader = new EndianBinaryReader(j3dData, Endian.Big))
                j3d.LoadFromStream(reader, Properties.Settings.Default.DumpTexturesToDisk, Properties.Settings.Default.DumpShadersToDisk);

            // If there are no animations specified, we can stop here and return the model
            if (res.Animations == null)
                return j3d;

            foreach(var anim in res.Animations)
            {
                VirtualFilesystemDirectory anim_arc = model_arc;

                if (!string.IsNullOrEmpty(anim.ArchiveName))
                {
                    string anim_arc_path = Path.Combine(Properties.Settings.Default.RootDirectory, "res/Object/", anim.ArchiveName + ".arc");

                    if (!File.Exists(anim_arc_path))
                        return null;

                    anim_arc = ArchiveUtilities.LoadArchive(anim_arc_path);
                }

                VirtualFilesystemFile anim_file = anim_arc.GetFileAtPath(anim.Path);

                if (anim_file == null)
                    continue;

                byte[] anim_data = anim_file.Data;

                // Decompress the file if necessary
                if (anim_data[0] == 'Y')
                {
                    MemoryStream decompressed_data = null;

                    using (EndianBinaryReader decompressor = new EndianBinaryReader(anim_data, Endian.Big))
                    {
                        decompressed_data = Yaz0.Decode(decompressor);
                    }

                    anim_data = decompressed_data.ToArray();
                }

                switch (anim.Type)
                {
                    case "bck":
                        BCK loaded_bck = new BCK(anim_file.Name);
                        using (EndianBinaryReader reader = new EndianBinaryReader(anim_data, Endian.Big))
                            loaded_bck.LoadFromStream(reader);

                        j3d.BoneAnimations.Add(loaded_bck);
                        j3d.SetBoneAnimation(anim_file.Name);

                        loaded_bck.Tick(anim.StartTime);

                        if (anim.PausedOnLoad)
                            loaded_bck.Pause();
                        break;
                    case "btk":
                        BTK loaded_btk = new BTK(anim_file.Name);
                        using (EndianBinaryReader reader = new EndianBinaryReader(anim_data, Endian.Big))
                            loaded_btk.LoadFromStream(reader);

                        j3d.MaterialAnimations.Add(loaded_btk);
                        j3d.SetMaterialAnimation(anim_file.Name);

                        loaded_btk.Tick(anim.StartTime);

                        if (anim.PausedOnLoad)
                            loaded_btk.Pause();
                        break;
                    case "brk":
                        BRK loaded_brk = new BRK(anim_file.Name);
                        using (EndianBinaryReader reader = new EndianBinaryReader(anim_data, Endian.Big))
                            loaded_brk.LoadFromStream(reader);

                        j3d.RegisterAnimations.Add(loaded_brk);
                        j3d.SetRegisterAnimation(anim_file.Name);

                        loaded_brk.Tick(anim.StartTime);

                        if (anim.PausedOnLoad)
                            loaded_brk.Pause();
                        break;
                    case "bmt":
                        BMT loaded_bmt = new BMT(anim_file.Name);
                        using (EndianBinaryReader reader = new EndianBinaryReader(anim_data, Endian.Big))
                            loaded_bmt.LoadFromStream(reader);

                        j3d.ExternalMaterials.Add(loaded_bmt);
                        j3d.SetExternalMaterial(anim_file.Name);
                        break;
                    default:
                        break;
                }
            }

            j3d.Tick(1 / (float)60);
            return j3d;
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

        public static Type GetTypeByName(string name)
        {
            if (!m_actorDescriptors.ContainsKey(name))
            {
                return typeof(Actor);
            }

            WActorDescriptor desc = m_actorDescriptors[name];

            if (desc.ActorClassType == null)
            {
                return typeof(Actor);
            }

            return Type.GetType($"WindEditor.{ desc.ActorClassType }");
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
