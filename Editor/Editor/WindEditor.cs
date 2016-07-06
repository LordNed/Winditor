using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WindEditor
{
    class WWindEditor
    {
        public WWorld MainWorld { get { return m_editorWorlds[0]; } }
        public ICommand OpenProjectCommand { get { return new RelayCommand(x => OnApplicationRequestOpenProject()); } }
        public ICommand ExitApplicationCommand { get { return new RelayCommand(x => OnApplicationShutdown()); } }
        public ICommand CloseProjectCommand { get { return new RelayCommand(x => OnApplicationRequestCloseProject()); } }
        public ICommand SetDataRootCommand { get { return new RelayCommand(x => OnUserSetDataRoot()); } }

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
                var folderPath = ofd.FileName;

                // ToDo: Close all of the additional worlds as well.
                //foreach (WWorld world in m_editorWorlds)
                    //world.UnloadMap();

                MainWorld.LoadMapFromDirectory(folderPath);
            }
        }

        public void OnApplicationRequestCloseProject()
        {
            MainWorld.UnloadMap();
        }

        public void OnApplicationShutdown()
        {
            foreach (WWorld world in m_editorWorlds)
                world.ShutdownWorld();

            App.Current.Shutdown();
        }

        public void ProcessTick()
        {
            foreach (WWorld world in m_editorWorlds)
                world.ProcessTick();

            GL.Flush();
        }

        private void OnUserSetDataRoot()
        {
            // Violate dat MVVM.
            WindEditor.View.OptionsMenu optionsMenu = new View.OptionsMenu();
            optionsMenu.Show();
        }
    }
}
