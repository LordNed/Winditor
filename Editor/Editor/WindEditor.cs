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

namespace WindEditor
{
    class WWindEditor
    {
        public WWorld MainWorld { get { return m_editorWorlds[0]; } }
        public ICommand OpenProjectCommand { get { return new RelayCommand(x => OnApplicationRequestOpenProject()); } }
        public ICommand SaveProjectCommand { get { return new RelayCommand(x => OnApplicationRequestSaveProject(), x => MainWorld.Map != null); } }
        public ICommand SaveProjectAsCommand { get { return new RelayCommand(x => OnApplicationRequestSaveAsProject(), x => MainWorld.Map != null); } }
        public ICommand ExportProjectCommand { get { return new RelayCommand(x => OnApplicationRequestExportProject(), x => MainWorld.Map != null); } }
        public ICommand ExportProjectAsCommand { get { return new RelayCommand(x => OnApplicationRequestExportAsProject(), x => MainWorld.Map != null); } }
        public ICommand CloseProjectCommand { get { return new RelayCommand(x => OnApplicationRequestCloseProject()); } }
        public ICommand StartPlaytestCommand { get { return new RelayCommand(x => OnApplicationRequestPlaytest(), x => MainWorld.Map != null); } }

        public PlaytestManager Playtester { get; set; }

        private List<WWorld> m_editorWorlds = new List<WWorld>();
        private string m_sourceDataPath;

        public WWindEditor()
        {
            // Add the default Editor World.
            m_editorWorlds.Add(new WWorld());

            Playtester = new PlaytestManager();

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
                    string tempMapPath = Path.GetTempPath() + Path.GetFileName(ofd.FileName); // This is where we'll dump the arc contents to

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

        private void CreateFileFromArc(VirtualFilesystemFile file, string rootPath)
        {
            string filePath = $"{rootPath}\\{file.NameWithExtension}";

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
            MainWorld.Map.ExportToDirectory(GetUserPath());
        }

        public void OnApplicationRequestExportAsProject()
        {
            MainWorld.Map.ExportToDirectory(GetUserPath());
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
            MainWorld.ActorEditor.DetailsViewModel.Categories = new OrderedDictionary();
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

        public void OnApplicationRequestPlaytest()
        {
            Playtester.RequestStartPlaytest(MainWorld.Map);
        }
    }
}
