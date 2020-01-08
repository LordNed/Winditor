using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using System.IO;
using WArchiveTools;
using WArchiveTools.FileSystem;
using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;
using Newtonsoft.Json;
using WindEditor.Minitors;
using System.Windows.Controls;
using System.Reflection;
using WindEditor.ViewModel;
using WindEditor.Editor;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WWindEditor
    {
        public WWorld MainWorld { get { return m_editorWorlds[0]; } }
        public ICommand OpenProjectCommand { get { return new RelayCommand(x => OnApplicationRequestOpenProject()); } }
        public ICommand OpenRoomsCommand { get { return new RelayCommand(x => OnApplicationRequestOpenRooms()); } }
        public ICommand SaveProjectCommand { get { return new RelayCommand(x => OnApplicationRequestSaveProject(), x => MainWorld.Map != null); } }
        public ICommand SaveProjectAsCommand { get { return new RelayCommand(x => OnApplicationRequestSaveAsProject(), x => MainWorld.Map != null); } }
        public ICommand ExportProjectCommand { get { return new RelayCommand(x => OnApplicationRequestExportProject(), x => MainWorld.Map != null); } }
        public ICommand ExportProjectAsCommand { get { return new RelayCommand(x => OnApplicationRequestExportAsProject(), x => MainWorld.Map != null); } }
        public ICommand CloseProjectCommand { get { return new RelayCommand(x => OnApplicationRequestCloseProject()); } }
        public ICommand StartPlaytestCommand { get { return new RelayCommand(x => OnApplicationRequestPlaytest(), x => MainWorld.Map != null); } }

        public ICommand SwitchToActorModeCommand { get { return new RelayCommand(x => OnRequestSwitchToActorMode(), X => !(MainWorld.CurrentMode is ActorMode || MainWorld.Map == null)); } }
        public ICommand SwitchToCollisionModeCommand { get { return new RelayCommand(x => OnRequestSwitchToCollisionMode(), X => !(MainWorld.CurrentMode is CollisionMode || MainWorld.Map == null)); } }

        public PlaytestManager Playtester { get; set; }
        public MapLayer ActiveLayer { get; set; }

        private List<WWorld> m_editorWorlds = new List<WWorld>();
        private string m_sourceDataPath;

        private List<IMinitor> m_RegisteredMinitors;

        public WWindEditor()
        {
            // Add the default Editor World.
            m_editorWorlds.Add(new WWorld());

            Playtester = new PlaytestManager();

            m_RegisteredMinitors = new List<IMinitor>();

			// Load our global data
			foreach (var file in Directory.GetFiles("resources/templates/"))
			{
				MapActorDescriptor descriptor = JsonConvert.DeserializeObject<MapActorDescriptor>(File.ReadAllText(file));
				Globals.ActorDescriptors.Add(descriptor);
			}
		}

        internal void OnViewportResized(int width, int height)
        {
            foreach(WWorld world in m_editorWorlds)
            {
                world.OnViewportResized(width, height);
            }
        }

        public void OnApplicationRequestOpenProject()
        {
            var ofd = new CommonOpenFileDialog()
            {
                Title = "Choose Directory",
                IsFolderPicker = true,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                List<string> files = new List<string>(Directory.GetFiles(ofd.FileName));
                List<string> dirs = new List<string>(Directory.GetDirectories(ofd.FileName));

                // There are directories here, so we will assume they are from already unpacked data.
                if (dirs.Count != 0)
                {
                    // Just assume the folder paths are valid now.
                    LoadProject(ofd.FileName, ofd.FileName);
                    m_sourceDataPath = ofd.FileName;
                }
                // We'll have to dump the contents of the arcs
                else
                {
                    string tempMapPath = Path.Combine(GetStageDumpPath(), Path.GetFileName(ofd.FileName)); // This is where we'll dump the arc contents to

                    DeleteDumpContentsFromTempDir();

                    if (!Directory.Exists(GetStageDumpPath()))
                        Directory.CreateDirectory(GetStageDumpPath());

                    if (!Directory.Exists(tempMapPath))
                        Directory.CreateDirectory(tempMapPath);

                    foreach (var arc in files)
                    {
                        VirtualFilesystemDirectory archiveRoot = ArchiveUtilities.LoadArchive(arc);
                        if (archiveRoot == null)
                            continue;

                        string tempArcPath = $"{tempMapPath}\\{archiveRoot.Name}";

                        if (!Directory.Exists(tempArcPath))
                            Directory.CreateDirectory(tempMapPath);

                        DumpContents(archiveRoot, tempArcPath);
                    }

                    LoadProject(tempMapPath, ofd.FileName);

                    // This will signal that we loaded from archives, and that there is no valid path to save the map yet.
                    MainWorld.Map.SavePath = null;
                    m_sourceDataPath = tempMapPath;
                }
            }
        }

        public void OnApplicationRequestOpenRooms()
        {
            var ofd = new CommonOpenFileDialog()
            {
                Title = "Choose Rooms to Open",
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = true,
                ShowPlacesList = true
            };

            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                List<string> files = new List<string>(ofd.FileNames);
                string dirPath = Path.GetDirectoryName(files[0]);

                string stageArcPath = Path.Combine(dirPath, "Stage.arc");
                if (!files.Contains(stageArcPath) && File.Exists(stageArcPath))
                {
                    // Always load the stage arc even if it wasn't selected by the user.
                    files.Add(stageArcPath);
                }

                string tempMapPath = Path.Combine(GetStageDumpPath(), Path.GetFileName(dirPath)); // This is where we'll dump the arc contents to

                DeleteDumpContentsFromTempDir();

                if (!Directory.Exists(GetStageDumpPath()))
                    Directory.CreateDirectory(GetStageDumpPath());

                if (!Directory.Exists(tempMapPath))
                    Directory.CreateDirectory(tempMapPath);

                foreach (var arc in files)
                {
                    VirtualFilesystemDirectory archiveRoot = ArchiveUtilities.LoadArchive(arc);
                    if (archiveRoot == null)
                        continue;

                    string tempArcPath = $"{tempMapPath}\\{archiveRoot.Name}";

                    if (!Directory.Exists(tempArcPath))
                        Directory.CreateDirectory(tempMapPath);

                    DumpContents(archiveRoot, tempArcPath);
                }

                LoadProject(tempMapPath, dirPath);

                // This will signal that we loaded from archives, and that there is no valid path to save the map yet.
                MainWorld.Map.SavePath = null;
                m_sourceDataPath = tempMapPath;
            }
        }

        private void DumpContents(VirtualFilesystemDirectory dir, string rootPath)
        {
            foreach (var child in dir.Children)
            {
                if (child.Type == NodeType.File)
                    CreateFileFromArc(child as VirtualFilesystemFile, rootPath);
                if (child.Type == NodeType.Directory)
                {
                    string dirPath = $"{rootPath}\\{child.Name}";

                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);

                    DumpContents(child as VirtualFilesystemDirectory, dirPath);
                }
            }
        }

        private string GetStageDumpPath()
        {
            return Path.Combine(Path.GetTempPath(), "WinditorStageDumps");
        }

        private void DeleteDumpContentsFromTempDir()
        {
            DirectoryInfo dir = new DirectoryInfo(GetStageDumpPath());
            if (dir.Exists)
            {
                dir.Delete(true);
            }
        }

        private void CreateFileFromArc(VirtualFilesystemFile file, string rootPath)
        {
            string filePath = $"{rootPath}\\{file.NameWithExtension}";
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.Write(file.Data, 0, file.Data.Length);
            }
        }

        public void OnApplicationRequestSaveProject()
        {
            // Data was loaded from archives to a temp folder. We need a new folder to copy this data to!
            if (MainWorld.Map.SavePath == null)
            {
                string path = GetUserPath();
                if (path == null)
                    return;

                string newMapDir = $"{ path }\\{ MainWorld.Map.MapName }";
                CopyTempDataToPermanentDir(m_sourceDataPath, newMapDir);

                MainWorld.Map.SavePath = newMapDir;
            }

            MainWorld.SaveMapToDirectory("");
        }

        public void OnApplicationRequestSaveAsProject()
        {
            string path = GetUserPath();
            if (path == null)
                return;

            MainWorld.SaveMapToDirectory(path);
        }

        public void OnApplicationRequestExportProject()
        {
            string path = GetUserPath();
            if (path == null)
                return;

            MainWorld.Map.ExportToDirectory(path);
        }

        public void OnApplicationRequestExportAsProject()
        {
            string path = GetUserPath();
            if (path == null)
                return;

            MainWorld.Map.ExportToDirectory(path);
        }

        private void CopyTempDataToPermanentDir(string sourceDir, string destDir)
        {
            foreach (string file in Directory.EnumerateFiles(sourceDir))
            {
                File.Copy(file, $"{ destDir }\\{ Path.GetFileName(file) }", true);
            }

            foreach (string dir in Directory.EnumerateDirectories(sourceDir))
            {
                string newDest = $"{ destDir }\\{ Path.GetFileNameWithoutExtension(dir) }";
                Directory.CreateDirectory(newDest);

                CopyTempDataToPermanentDir(dir, newDest);
            }
        }

        private string GetUserPath()
        {
            string path = null;

            var ofd = new CommonOpenFileDialog();
            ofd.Title = "Choose Directory";
            ofd.IsFolderPicker = true;
            ofd.AddToMostRecentlyUsedList = false;
            ofd.AllowNonFileSystemItems = false;
            ofd.EnsureFileExists = true;
            ofd.EnsurePathExists = true;
            ofd.EnsureReadOnly = false;
            ofd.EnsureValidNames = true;
            ofd.Multiselect = false;
            ofd.ShowPlacesList = true;

            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = ofd.FileName;
            }

            return path;
        }

        public void LoadProject(string folderPath, string sourcePath)
        {
            if (MainWorld.Map != null)
                MainWorld.UnloadMap();

            MainWorld.LoadMapFromDirectory(folderPath, sourcePath);
        }

        public void OnApplicationRequestCloseProject()
        {
            MainWorld.UnloadMap();
        }

        public void ProcessTick()
        {
            foreach (WWorld world in m_editorWorlds)
                world.ProcessTick();

            GL.Flush();
        }

        public void Shutdown()
        {
            foreach (WWorld world in m_editorWorlds)
                world.ShutdownWorld();
        }

        public bool TryCloseMinitors()
        {
            foreach (IMinitor i in m_RegisteredMinitors)
            {
                if (!i.RequestCloseModule())
                    return false;
            }

            return true;
        }

        public void OnApplicationRequestPlaytest()
        {
            if (!TryCloseMinitors())
            {
                return;
            }

            Playtester.RequestStartPlaytest(MainWorld.Map, ActiveLayer);
        }

        public void InitMinitorModules()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<string> classlist = new List<string>();

            foreach (Type type in asm.GetTypes())
            {
                if (type.Namespace == "WindEditor.Minitors" && type.GetInterface("IMinitor") != null)
                {
                    IMinitor new_editor = (IMinitor)Activator.CreateInstance(type);
                    MainWorld.InitMinitorModule(new_editor);
                    m_RegisteredMinitors.Add(new_editor);
                }
            }
        }

        public List<MenuItem> GetRegisteredEditorMenus()
        {
            List<MenuItem> items = new List<MenuItem>();

            foreach (IMinitor e in m_RegisteredMinitors)
            {
                items.Add(e.GetMenuItem());
            }

            return items;
        }

        public void OnRequestSwitchToActorMode()
        {
            MainWorld.SwitchToActorMode();
        }

        public void OnRequestSwitchToCollisionMode()
        {
            MainWorld.SwitchToCollisionMode();
        }
    }
}
