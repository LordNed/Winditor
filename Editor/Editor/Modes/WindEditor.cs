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
using System.Windows;
using WindEditor.Collision;
using System.Text.RegularExpressions;

namespace WindEditor
{
    class WWindEditor
    {
        public WWorld MainWorld { get { return m_editorWorlds[0]; } }
        public ICommand OpenProjectCommand { get { return new RelayCommand(x => OnApplicationRequestOpenProject()); } }
        public ICommand OpenRoomsCommand { get { return new RelayCommand(x => OnApplicationRequestOpenRooms()); } }
        public ICommand ExportProjectCommand { get { return new RelayCommand(x => OnApplicationRequestExportProject(), x => MainWorld.Map != null); } }
        public ICommand ExportProjectAsCommand { get { return new RelayCommand(x => OnApplicationRequestExportAsProject(), x => MainWorld.Map != null); } }
        public ICommand CloseProjectCommand { get { return new RelayCommand(x => OnApplicationRequestCloseProject()); } }
        public ICommand StartPlaytestCommand { get { return new RelayCommand(x => OnApplicationRequestPlaytest(), x => MainWorld.Map != null); } }
        public ICommand OpenPlaytestInventoryCommand { get { return new RelayCommand(x => OnApplicationRequestOpenPlaytestInventory(), x => MainWorld.Map != null); } }

        public ICommand SwitchToActorModeCommand { get { return new RelayCommand(x => OnRequestSwitchToActorMode(), X => !(MainWorld.CurrentMode is ActorMode || MainWorld.Map == null)); } }
        public ICommand SwitchToCollisionModeCommand { get { return new RelayCommand(x => OnRequestSwitchToCollisionMode(), X => !(MainWorld.CurrentMode is CollisionMode || MainWorld.Map == null)); } }
        public ICommand SwitchToEventModeCommand { get { return new RelayCommand(x => OnRequestSwitchToEventMode(), X => !(MainWorld.CurrentMode is EventMode || MainWorld.Map == null)); } }

        public ICommand ImportCollisionCommand { get { return new RelayCommand(x => OnRequestImportCollision(), X => !(MainWorld.Map == null)); ; } }
        public ICommand ImportVisualMeshCommand { get { return new RelayCommand(x => OnRequestImportVisualMesh(), X => !(MainWorld.Map == null)); ; } }
        public ICommand ExportCollisionCommand { get { return new RelayCommand(x => OnRequestExportCollision(), X => !(MainWorld.Map == null)); ; } }
        public ICommand ExportVisualMeshCommand { get { return new RelayCommand(x => OnRequestExportVisualMesh(), X => !(MainWorld.Map == null)); ; } }
        public ICommand ImportIslandCommand { get { return new RelayCommand(x => OnRequestImportIsland(), X => !(MainWorld.Map == null)); ; } }
        public ICommand TutorialsCommand { get { return new RelayCommand(x => OnRequestOpenTutorials()); ; } }
        public ICommand IssuesCommand { get { return new RelayCommand(x => OnRequestReportIssue()); ; } }
        public ICommand AboutCommand { get { return new RelayCommand(x => OnRequestReportIssue()); ; } }

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
            if (WSettingsManager.GetSettings().LastStagePath.FilePath != "")
            {
                ofd.InitialDirectory = WSettingsManager.GetSettings().LastStagePath.FilePath;
            }

            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                List<string> files = new List<string>(Directory.GetFiles(ofd.FileName));
                List<string> dirs = new List<string>(Directory.GetDirectories(ofd.FileName));

                // There are directories here, and one of them is an unpacked Stage folder, so try to load all of these instead of loading the .arcs.
                if (dirs.Count != 0 && File.Exists(Path.Combine(ofd.FileName, "Stage/dzs/stage.dzs")))
                {
                    LoadProject(ofd.FileName, ofd.FileName);
                    m_sourceDataPath = ofd.FileName;
                }
                // We'll have to dump the contents of the arcs
                else
                {
                    string tempMapPath = HandleTempPath(ofd.FileName);

                    foreach (var arc in files)
                    {
                        var filename = Path.GetFileName(arc);
                        Regex reg = new Regex(@"(Stage|Room\d+).arc", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        if (!reg.IsMatch(filename))
                            continue;

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

                    WSettingsManager.GetSettings().LastStagePath.FilePath = ofd.FileName;
                    WSettingsManager.SaveSettings();
                }
            }
        }

        private string HandleTempPath(string fileName)
        {
            string tempPath = Path.Combine(GetStageDumpPath(), Path.GetFileName(fileName)); // This is where we'll dump the arc contents to

            DeleteDumpContentsFromTempDir();

            if (!Directory.Exists(GetStageDumpPath()))
                Directory.CreateDirectory(GetStageDumpPath());

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            return tempPath;
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
			ofd.Filters.Add(new CommonFileDialogFilter("RARC Archives", ".arc"));
			ofd.Filters.Add(new CommonFileDialogFilter("All Files", ".*"));

            if (WSettingsManager.GetSettings().LastStagePath.FilePath != "")
            {
                ofd.InitialDirectory = WSettingsManager.GetSettings().LastStagePath.FilePath;
            }

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

                WSettingsManager.GetSettings().LastStagePath.FilePath = dirPath;
                WSettingsManager.SaveSettings();
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
            return Path.Combine(Path.GetTempPath(), "Winditor");
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

        public void OnApplicationRequestExportProject()
        {
            if (MainWorld.Map.MapName.Length > 7)
            {
                string error = "";
                error += "The name of the stage you are currently saving is too long.\n";
                error += "Stage names must be 7 characters or shorter.\n";
                error += "The game will crash if you load this map without shortening its name.";
                MessageBox.Show(error, "Warning");
            }

            string path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "stage", MainWorld.Map.MapName);

            try
            {
                MainWorld.Map.ExportToDirectory(path);
            }
            catch (Exception e)
            {
                string error = "";
                error += e.GetType().FullName + "\n";
                error += e.Message + "\n";
                error += e.StackTrace;
                MessageBox.Show(error, "Error building project");
                return;
            }

            WSettingsManager.GetSettings().LastStagePath.FilePath = path;
        }

        public void OnApplicationRequestExportAsProject()
        {
            string path = GetUserPath(WSettingsManager.GetSettings().LastStagePath.FilePath);
            if (path == null)
                return;

            try
            {
                MainWorld.Map.ExportToDirectory(path);
            }
            catch (Exception e)
            {
                string error = "";
                error += e.GetType().FullName + "\n";
                error += e.Message + "\n";
                error += e.StackTrace;
                MessageBox.Show(error, "Error building project");
                return;
            }

            WSettingsManager.GetSettings().LastStagePath.FilePath = path;
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

        private string GetUserPath(string initialPath="")
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

            if (initialPath != "")
            {
                ofd.InitialDirectory = initialPath;
            }

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

            try
            {
                Playtester.RequestStartPlaytest(MainWorld.Map, ActiveLayer);
            }
            catch (Exception e)
            {
                string error = "";
                error += e.GetType().FullName + "\n";
                error += e.Message + "\n";
                error += e.StackTrace;
                MessageBox.Show(error, "Error building and playtesting project");
                return;
            }
        }

        public void OnApplicationRequestOpenPlaytestInventory()
        {
            Playtester.RequestShowInventorySettings();
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

            items.Add(new MenuItem() { Name = "islandimporteritem", Header = "Island Importer",
                Command = ImportIslandCommand, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Stretch });

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

        public void OnRequestSwitchToEventMode()
        {
            MainWorld.SwitchToEventMode();
        }

        public void OnRequestImportCollision()
        {
            View.CollisionImportWindow window = new View.CollisionImportWindow(MainWorld.Map);
            window.FileSelector.IsFilePicker = true;

            if (window.ShowDialog() == true)
            {
                if (window.FileName == "" || !File.Exists(window.FileName))
                {
                    MessageBox.Show("Invalid filename entered!", "Collision Import Error");
                    return;
                }

                if (window.SceneNumber == -1 || window.SceneNumber > MainWorld.Map.SceneList.Count - 1)
                {
                    MessageBox.Show("Invalid room number entered!", "Collision Import Error");
                    return;
                }

                string ext = Path.GetExtension(window.FileName);
                if (ext != ".dae" && ext != ".obj" && ext != ".dzb")
                {
                    MessageBox.Show($"Input file { window.FileName } was not a supported format.", "Collision Import Error");
                    return;
                }

                WRoom room = GetRoomFromDropdownIndex(window.SceneNumber);

                CategoryDOMNode colCategory = room.GetChildrenOfType<CategoryDOMNode>().Find(x => x.Name == "Collision");
                List<WCollisionMesh> originalMeshList = room.GetChildrenOfType<WCollisionMesh>();

                WCollisionMesh newMesh = null;

                if (ext == ".dae" || ext == ".obj")
                {
                    int origRootRoomTableIndex = 0;
                    if (originalMeshList.Count > 0)
                        origRootRoomTableIndex = originalMeshList[0].RootNode.RoomTableIndex;

                    try
                    {
                        newMesh = new WCollisionMesh(MainWorld, window.FileName, room.RoomIndex, origRootRoomTableIndex);
                        newMesh.Name = "room";
                    }
                    catch (Exception e)
                    {
                        if (newMesh != null)
                        {
                            newMesh.ReleaseResources();
                        }
                        string error = "";
                        error += e.GetType().FullName + "\n";
                        error += e.Message + "\n";
                        error += e.StackTrace;
                        MessageBox.Show(error, "Mesh Import Error");
                        return;
                    }
                }
                else
                    newMesh = new WCollisionMesh(MainWorld, window.FileName);

                if (originalMeshList.Count > 0)
                {
                    originalMeshList[0].ReleaseResources();
                    colCategory.Children.Remove(originalMeshList[0]);

                    if (MainWorld.CollisionMode.ActiveCollisionMesh == originalMeshList[0])
                    {
                        newMesh.IsRendered = true;
                        MainWorld.CollisionMode.ClearSelection();
                        MainWorld.CollisionMode.ActiveCollisionMesh = newMesh;
                    }
                }

                colCategory.Children.Add(newMesh);
            }
        }

        public void OnRequestImportVisualMesh()
        {
            View.VisualMeshImportWindow window = new View.VisualMeshImportWindow(MainWorld.Map);
            window.FileSelector.IsFilePicker = true;

            if (window.ShowDialog() == true)
            {
                if (window.FileName == "" || !File.Exists(window.FileName))
                {
                    MessageBox.Show("Invalid filename entered!", "Mesh Import Error");
                    return;
                }

                if (window.SceneNumber == -1 || window.SceneNumber > MainWorld.Map.SceneList.Count)
                {
                    MessageBox.Show("Invalid room number entered!", "Mesh Import Error");
                    return;
                }

                try
                {
                    if (window.SceneNumber == 0)
                    {
                        ImportVisualMeshToStage(window);
                    }
                    else
                    {
                        ImportVisualMeshToRoom(window);
                    }
                }
                catch (Exception e)
                {
                    string error = "";
                    error += e.GetType().FullName + "\n";
                    error += e.Message + "\n";
                    error += e.StackTrace;
                    MessageBox.Show(error, "Mesh Import Error");
                    return;
                }
            }
        }

        private void ImportVisualMeshToStage(View.VisualMeshImportWindow importWindow)
        {
            WStage stage = GetStage();

            if (importWindow.SlotNumber == 4)
            {
                ImportVisualMeshToSkybox(importWindow, stage);
            }
            else
            {
                CategoryDOMNode meshCategory = stage.GetChildrenOfType<CategoryDOMNode>().Find(x => x.Name == "Models");
                string meshName = "";

                switch (importWindow.SlotNumber)
                {
                    case 0:
                        meshName = "vr_sky";
                        break;
                    case 4:
                        meshName = "door10";
                        break;
                    case 5:
                        meshName = "door20";
                        break;
                    case 6:
                        meshName = "key10";
                        break;
                    case 7:
                        meshName = "stop10";
                        break;
                }

                ImportVisualMeshToCategory(importWindow, meshCategory, meshName);
            }
        }

        private void ImportVisualMeshToSkybox(View.VisualMeshImportWindow importWindow, WStage stage)
        {

        }

        private void ImportVisualMeshToRoom(View.VisualMeshImportWindow importWindow)
        {
            WRoom room = GetRoomFromDropdownIndex(importWindow.SceneNumber - 1);
            CategoryDOMNode meshCategory = room.GetChildrenOfType<CategoryDOMNode>().Find(x => x.Name == "Models");

            string newMeshName = "model";

            if (importWindow.SlotNumber > 0)
            {
                newMeshName += importWindow.SlotNumber;
            }

            ImportVisualMeshToCategory(importWindow, meshCategory, newMeshName);

            // Re-apply MULT transform to the imported mesh.
            room.SetRoomTransform(room.RoomTransform);
        }

        private void ImportVisualMeshToCategory(View.VisualMeshImportWindow importWindow, CategoryDOMNode category, string meshName)
        {
            List<J3DNode> meshList = category.GetChildrenOfType<J3DNode>();

            bool isBDL = true;

            J3DNode oldMeshNode = meshList.Find(x => x.Name == meshName);
            if (oldMeshNode != null)
            {
                category.Children.Remove(oldMeshNode);
                isBDL = oldMeshNode.Model.StudioType == "bdl4";
            }

            string fileExt = Path.GetExtension(importWindow.FileName);
            string loadFilename = "";

            if (fileExt == ".bmd" || fileExt == ".bdl")
            {
                loadFilename = importWindow.FileName;
            }
            else
            {
                loadFilename = ImportVisualMesh(importWindow, isBDL);
            }

            JStudio.J3D.J3D newMesh = WResourceManager.LoadResource(loadFilename);
            newMesh.Name = meshName;
            J3DNode newNode = new J3DNode(newMesh, MainWorld, loadFilename);

            category.Children.Add(newNode);
        }

        private string ImportVisualMesh(View.VisualMeshImportWindow importWindow, bool isBDL)
        {
            string tempFileName = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            string loadFilename = Path.Combine(Path.GetTempPath(), "Winditor", tempFileName + (isBDL ? ".bdl" : ".bmd"));

            List<string> superBMDArgs = new List<string>(new string[] { "-i", $"{ importWindow.FileName }" });
            superBMDArgs.Add("--rotate");
            if (isBDL)
            {
                superBMDArgs.Add("-b");
            }
            string materials_path = Path.Combine(Path.GetDirectoryName(importWindow.FileName), "materials.json");
            if (File.Exists(materials_path))
            {
                superBMDArgs.Add("--materialPresets");
                superBMDArgs.Add(materials_path);
            } else if (importWindow.GenerateMaterials)
            {
                superBMDArgs.Add("-glm");
            }
            string tex_headers_path = Path.Combine(Path.GetDirectoryName(importWindow.FileName), "tex_headers.json");
            if (File.Exists(tex_headers_path))
            {
                superBMDArgs.Add("--texHeaders");
                superBMDArgs.Add(tex_headers_path);
            }

            SuperBMDLib.Arguments args = new SuperBMDLib.Arguments(superBMDArgs.ToArray());

            SuperBMDLib.Model newJ3D = SuperBMDLib.Model.Load(args);
            newJ3D.ExportBMD(loadFilename, isBDL);

            return loadFilename;
        }

        private WRoom GetRoomFromDropdownIndex(int index)
        {
            int i = 0;
            foreach (WScene scene in MainWorld.Map.SceneList)
            {
                if (scene is WRoom)
                {
                    if (i == index)
                        return scene as WRoom;

                    i += 1;
                }
            }

            return null;
        }

        private WStage GetStage()
        {
            WStage stage = null;

            for (int i = 0; i < MainWorld.Map.SceneList.Count; i++)
            {
                WStage castTest = MainWorld.Map.SceneList[i] as WStage;
                if (castTest != null)
                {
                    stage = castTest;
                    break;
                }
            }

            return stage;
        }

        public void OnRequestExportCollision()
        {
            View.CollisionExportWindow window = new View.CollisionExportWindow(MainWorld.Map);
            window.FileSelector.IsFilePicker = true;
            window.FileSelector.IsFileSaver = true;
            window.FileSelector.FileExtension = "dae";
            
            if (window.ShowDialog() == true)
            {
                if (window.FileName == "")
                {
                    MessageBox.Show("No filename entered!", "Collision Export Error");
                    return;
                }
                
                if (window.SceneNumber == -1 || window.SceneNumber > MainWorld.Map.SceneList.Count - 1)
                {
                    MessageBox.Show("Invalid room number entered!", "Collision Export Error");
                    return;
                }
                
                WRoom room = GetRoomFromDropdownIndex(window.SceneNumber);
                
                CategoryDOMNode colCategory = room.GetChildrenOfType<CategoryDOMNode>().Find(x => x.Name == "Collision");
                WCollisionMesh mesh = colCategory.Children[0] as WCollisionMesh;
                mesh.ToDAEFile(window.FileName);

                MessageBox.Show("Successfully saved collision to file.", "Success");
            }
        }

        public void OnRequestExportVisualMesh()
        {
            View.VisualMeshExportWindow window = new View.VisualMeshExportWindow(MainWorld.Map);
            window.FileSelector.IsFilePicker = true;
            window.FileSelector.IsFileSaver = true;
            window.FileSelector.FileExtension = "dae";

            if (window.ShowDialog() == true)
            {
                if (window.FileName == "")
                {
                    MessageBox.Show("No filename entered!", "Mesh Export Error");
                    return;
                }
            
                if (window.SceneNumber == -1 || window.SceneNumber > MainWorld.Map.SceneList.Count)
                {
                    MessageBox.Show("Invalid room number entered!", "Mesh Export Error");
                    return;
                }
            
                if (window.SceneNumber == 0)
                {
                    ExportVisualMeshFromStage(window);
                }
                else
                {
                    ExportVisualMeshFromRoom(window);
                }
            }

            MessageBox.Show("Successfully saved mesh to file.", "Success");
        }

        public void ExportVisualMeshFromStage(View.VisualMeshExportWindow exportWindow)
        {
            // TODO
        }

        public void ExportVisualMeshFromRoom(View.VisualMeshExportWindow exportWindow)
        {
            WRoom room = GetRoomFromDropdownIndex(exportWindow.SceneNumber - 1);
            CategoryDOMNode meshCategory = room.GetChildrenOfType<CategoryDOMNode>().Find(x => x.Name == "Models");

            string newMeshName = "model";

            if (exportWindow.SlotNumber > 0)
            {
                newMeshName += exportWindow.SlotNumber;
            }

            ExportVisualMeshToCategory(exportWindow, meshCategory, newMeshName);
        }

        private void ExportVisualMeshToCategory(View.VisualMeshExportWindow exportWindow, CategoryDOMNode category, string meshName)
        {
            List<J3DNode> meshList = category.GetChildrenOfType<J3DNode>();

            J3DNode meshNode = meshList.Find(x => x.Name == meshName);
            if (meshNode == null)
            {
                MessageBox.Show("No mesh in the selected slot!", "Mesh Export Error");
                return;
            }

            ExportVisualMesh(exportWindow, meshNode.Filename);
        }

        private void ExportVisualMesh(View.VisualMeshExportWindow exportWindow, string modelFilename)
        {
            List<string> superBMDArgs = new List<string>(new string[] {
                "-i", $"{ modelFilename }",
                "-o", $"{ exportWindow.FileName }",
            });
            
            SuperBMDLib.Arguments args = new SuperBMDLib.Arguments(superBMDArgs.ToArray());
            
            SuperBMDLib.Model newJ3D = SuperBMDLib.Model.Load(args);
            newJ3D.ExportAssImp(exportWindow.FileName, "dae", new SuperBMDLib.ExportSettings());
            // TODO: the daes exported by this have issues that prevents them from being read properly by blender
        }

        private void OnRequestImportIsland()
        {
            View.IslandImportWindow window = new View.IslandImportWindow(MainWorld.Map);
            window.FileSelector.IsFilePicker = true;
            window.FileSelector.FileExtension = "arc";

            if (window.ShowDialog() == true)
            {
                if (window.FileName == "")
                {
                    MessageBox.Show("No filename entered!", "Island Import Error");
                    return;
                }

                if (window.SceneNumber == -1)
                {
                    MessageBox.Show("Invalid room number entered!", "Island Import Error");
                    return;
                }

                WRoom oldRoom = GetRoomFromDropdownIndex(window.SceneNumber);
                if (oldRoom != null)
                {
                    MainWorld.Map.SceneList.Remove(oldRoom);
                }

                List<WCollisionMesh> colList = oldRoom.GetChildrenOfType<WCollisionMesh>();

                if (colList.Count > 0)
                    colList[0].ReleaseResources();

                string tempMapPath = Path.Combine(GetStageDumpPath(), Path.GetFileName(window.FileName));

                VirtualFilesystemDirectory archiveRoot = ArchiveUtilities.LoadArchive(window.FileName);
                if (archiveRoot == null)
                {
                    MessageBox.Show("Invalid archive selected!", "Island Import Error");
                    return;
                }

                string tempArcPath = $"{tempMapPath}\\{archiveRoot.Name}";

                if (!Directory.Exists(tempArcPath))
                    Directory.CreateDirectory(tempMapPath);

                DumpContents(archiveRoot, tempArcPath);

                WRoom newRoom = new WRoom(MainWorld, oldRoom.RoomIndex);
                newRoom.Load(tempArcPath);
                newRoom.RoomTransform = oldRoom.RoomTransform;
                newRoom.ApplyTransformToObjects();

                newRoom.Name = "room" + oldRoom.RoomIndex;
                archiveRoot.Name = "room" + oldRoom.RoomIndex;
                newRoom.SourceDirectory = archiveRoot;

                MainWorld.Map.SceneList.Add(newRoom);
            }
        }

        private void OnRequestOpenTutorials()
        {
            System.Diagnostics.Process.Start("https://lordned.github.io/Winditor/tutorials/tutorials.html");
        }

        private void OnRequestReportIssue()
        {
            System.Diagnostics.Process.Start("https://github.com/LordNed/Winditor/issues");
        }
    }
}
