using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Windows.Input;
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
                // Just assume the folder paths are valid now.
                LoadProject(ofd.FileName);
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

        public void LoadProject(string folderPath)
        {
            if (MainWorld.Map != null)
                MainWorld.UnloadMap();

            MainWorld.LoadMapFromDirectory(folderPath);
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
