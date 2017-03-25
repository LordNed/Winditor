using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Windows.Input;
using System.IO;
using WArchiveTools;
using WArchiveTools.FileSystem;
using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;

namespace WindEditor
{
    class WWindEditor
    {
        public WWorld MainWorld { get { return m_editorWorlds[0]; } }
        public ICommand OpenProjectCommand { get { return new RelayCommand(x => OnApplicationRequestOpenProject()); } }
        public ICommand SaveProjectCommand { get { return new RelayCommand(x => OnApplicationRequestSaveProject(), x => MainWorld.Map != null); } }
        public ICommand SaveProjectAsCommand { get { return new RelayCommand(x => OnApplicationRequestSaveAsProject(), x => MainWorld.Map != null); } }
        public ICommand CloseProjectCommand { get { return new RelayCommand(x => OnApplicationRequestCloseProject()); } }

        private List<WWorld> m_editorWorlds = new List<WWorld>();

        public WWindEditor()
        {
            // Add the default Editor World.
            m_editorWorlds.Add(new WWorld());
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

                // There were no files in the directory, so we'll assume the map is already extracted
                if (files.Count == 0)
                {
                    // Just assume the folder paths are valid now.
                    LoadProject(ofd.FileName, ofd.FileName);
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
                        string tempArcPath = $"{tempMapPath}\\{archiveRoot.Name}";

                        if (!Directory.Exists(tempArcPath))
                            Directory.CreateDirectory(tempMapPath);

                        DumpContents(archiveRoot, tempArcPath);
                    }

                    LoadProject(tempMapPath, ofd.FileName);
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
            MainWorld.SaveMapToDirectory("");
        }

        public void OnApplicationRequestSaveAsProject()
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
                // Just assume the folder paths are valid now.
                MainWorld.SaveMapToDirectory(ofd.FileName);
            }
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
    }
}
