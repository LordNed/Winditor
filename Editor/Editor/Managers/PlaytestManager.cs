using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using WindEditor.Editor.Modes;
using System.Windows.Forms;
using WindEditor.View;

namespace WindEditor
{
    public class PlaytestManager
    {
        private Process m_DolphinInstance;
        private ProcessStartInfo m_DolphinStartInfo;
        private PlaytestInventoryWindow m_InventorySettings;

        List<string> m_BackedUpFilePaths;

        public PlaytestManager()
        {
            m_DolphinInstance = null;
            m_InventorySettings = new PlaytestInventoryWindow();
            m_InventorySettings.Closing += M_InventorySettings_Closing;
        }

        private void M_InventorySettings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            m_InventorySettings.ViewModel.OnCancelChanges();
        }

        public void RequestStartPlaytest(WMap map, MapLayer active_layer)
        {
            if (m_DolphinInstance != null)
            {
                m_DolphinInstance.CloseMainWindow();
                m_DolphinInstance.WaitForExit();
            }

            StartPlaytest(map, active_layer);
        }

        public void RequestShowInventorySettings()
        {
            m_InventorySettings.ShowDialog();
        }

        private void StartPlaytest(WMap map, MapLayer active_layer)
        {
            string dolphinPath = Path.Combine(WSettingsManager.GetSettings().DolphinDirectory.FilePath, "Dolphin.exe");
            if (!File.Exists(dolphinPath))
            {
                MessageBox.Show("You must specify the path to Dolphin in the options menu before you can playtest.", "Dolphin not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (map.MapName.Length > 7)
            {
                string error = "";
                error += "The name of the stage you are trying to playtest is too long.\n";
                error += "Stage names must be 7 characters or shorter.\n";
                error += "The game would crash if you loaded this map without shortening its name.\n";
                error += "Aborting playtest.";
                MessageBox.Show(error, "Warning");
                return;
            }

            Console.WriteLine($"Stage name: { map.MapName }, Room Name: { map.FocusedSceneLabel }");

            string map_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "stage", map.MapName);
            map.ExportToDirectory(map_path);

            MakeBackupSystem();

            string dolPath = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys", "main.dol");

            m_DolphinStartInfo = new ProcessStartInfo(dolphinPath);
            m_DolphinStartInfo.Arguments = $"-b -e \"{ dolPath }\"";

            int room_no = 0;
            int spawn_id = 0;

            GetRoomAndSpawnID(map.FocusedScene, out room_no, out spawn_id);

            int numMatchingSpawns = 0;
            foreach (var scn in map.SceneList)
            {
                numMatchingSpawns += scn.GetChildrenOfType<SpawnPoint>().Where(x => x.Room == room_no && x.SpawnID == spawn_id).Count();
            }
            if (numMatchingSpawns == 0)
            {
                MessageBox.Show($"No spawns found with room number {room_no} and spawn ID {spawn_id}.", "Warning");
                return;
            }
            if (numMatchingSpawns > 1)
            {
                MessageBox.Show($"There are {numMatchingSpawns} duplicate spawns with room number {room_no} and spawn ID {spawn_id}.", "Warning");
                return;
            }

            if (!File.Exists(dolPath))
            {
                Console.WriteLine("ISO root has no executable!");
                return;
            }

            Patch testRoomPatch = JsonConvert.DeserializeObject<Patch>(File.ReadAllText(@"resources\patches\test_room_diff.json"));
            
            testRoomPatch.Files[0].Patchlets.Add(new Patchlet(0x800531E3, new List<byte>(new byte[] { (byte)spawn_id })));
            testRoomPatch.Files[0].Patchlets.Add(new Patchlet(0x800531E7, new List<byte>(new byte[] { (byte)room_no })));
            testRoomPatch.Files[0].Patchlets.Add(new Patchlet(0x800531EB, new List<byte>(new byte[] { (byte)(active_layer - 1) })));

            // Starting items list is at 0x8022D03C and can contain a maximum of 256 item IDs
            List<byte> ItemIDs = m_InventorySettings.ViewModel.ItemIDs;
            for (int i = 0; i < ItemIDs.Count; i++)
            {
                testRoomPatch.Files[0].Patchlets.Add(new Patchlet(0x8022D03C + i, new List<byte>(new byte[] { ItemIDs[i] })));
            }

            // Save slot
            testRoomPatch.Files[0].Patchlets.Add(new Patchlet(0x8022CFDF, new List<byte>(new byte[] { m_InventorySettings.ViewModel.SaveFileIndex })));

            testRoomPatch.Apply(WSettingsManager.GetSettings().RootDirectoryPath);

            Patch devModePatch = JsonConvert.DeserializeObject<Patch>(File.ReadAllText(@"resources\patches\developer_mode_diff.json"));
            devModePatch.Apply(WSettingsManager.GetSettings().RootDirectoryPath);

            Patch particleIdsPatch = JsonConvert.DeserializeObject<Patch>(File.ReadAllText(@"resources\patches\missing_particle_ids_diff.json"));
            particleIdsPatch.Apply(WSettingsManager.GetSettings().RootDirectoryPath);

            if (WSettingsManager.GetSettings().HeapDisplay)
            {
                Patch heapDisplayPatch = JsonConvert.DeserializeObject<Patch>(File.ReadAllText(@"resources\patches\heap_display_diff.json"));
                heapDisplayPatch.Apply(WSettingsManager.GetSettings().RootDirectoryPath);
            }

            m_DolphinInstance = Process.Start(m_DolphinStartInfo);

            m_DolphinInstance.EnableRaisingEvents = true;
            m_DolphinInstance.Exited += OnDolphinExited;
        }

        /// <summary>
        /// Make backup of system files. (decompiled .iso ROM)
        /// </summary>
        private void MakeBackupSystem()
        {
            // Make backup of system files
            List<string> filesToBackUp = new List<string> { "sys/main.dol", "sys/boot.bin" };

            m_BackedUpFilePaths = new List<string>();
            foreach (string filePath in filesToBackUp)
            {
                string fullPath = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, filePath);
                m_BackedUpFilePaths.Add(fullPath);
            }

            foreach (string filePath in m_BackedUpFilePaths)
            {
                string backupPath = filePath + ".bak";

                //Solve problem of only .bak existing 
                if (!File.Exists(filePath) && File.Exists(backupPath))
                {
                    File.Move(backupPath, filePath);
                }

                // make backup
                if (!File.Exists(backupPath))
                {
                    File.Copy(filePath, backupPath);
                }
            }
        }

        private void OnDolphinExited(object sender, EventArgs e)
        {
            m_DolphinInstance.Exited -= OnDolphinExited;
            m_DolphinInstance = null;

            foreach (string filePath in m_BackedUpFilePaths)
            {
                string backupPath = filePath + ".bak";
                if (File.Exists(filePath) && File.Exists(backupPath))
                {
                    File.Delete(filePath);
                    File.Move(backupPath, filePath);
                }
            }
        }

        private byte GetRoomNumberFromSceneName(string name)
        {
            if (name == "Stage")
                return 0;

            byte room_no = 0;

            string room_removed = name.ToLowerInvariant().Replace("room", "");
            byte.TryParse(room_removed, out room_no);

            return room_no;
        }

        private void GetRoomAndSpawnID(WScene scene, out int room, out int spawn)
        {
            room = 0;
            spawn = 0;

            Selection<WDOMNode> selected = null;

            if (scene.World.CurrentMode is ActorMode)
            {
                ActorMode mode = scene.World.CurrentMode as ActorMode;
                selected = mode.EditorSelection;
            }

            room = GetRoomNumberFromSceneName(scene.Name);
            if (selected != null && selected.PrimarySelectedObject is SpawnPoint)
            {
                // If the user has a spawn point selected, spawn the player at that spawn point.
                SpawnPoint spawn_pt = (SpawnPoint)selected.PrimarySelectedObject;

                room = spawn_pt.Room;
                spawn = spawn_pt.SpawnID;
            }
            else if (selected != null && selected.PrimarySelectedObject != null)
            {
                // If the user has something besides a spawn point selected, spawn the player at the first spawn point in the room that the selected object is in.
                WDOMNode cur_object = selected.PrimarySelectedObject;
                while (cur_object.Parent != null)
                {
                    cur_object = cur_object.Parent;
                }
                WRoom room_node;
                if (cur_object is WRoom)
                {
                    room_node = cur_object as WRoom;
                } else
                {
                    // A stage entity is selected. Use whatever spawn point is physically closest to the selected entity, regardless of what scene that spawn is in.
                    List<SpawnPoint> allSpawnPts = new List<SpawnPoint>();
                    foreach (WScene scn in scene.World.Map.SceneList)
                    {
                        allSpawnPts.AddRange(scn.GetChildrenOfType<SpawnPoint>());
                    }

                    SpawnPoint closestSpawnPt = allSpawnPts.OrderBy(spawnPt => (spawnPt.Transform.Position - selected.PrimarySelectedObject.Transform.Position).Length).First();
                    room = closestSpawnPt.Room;
                    spawn = closestSpawnPt.SpawnID;
                    return;
                }

                SpawnPoint spawn_pt = room_node.GetChildrenOfType<SpawnPoint>().FirstOrDefault();
                if (spawn_pt != null)
                {
                    room = spawn_pt.Room;
                    spawn = spawn_pt.SpawnID;
                }
                else
                {
                    WStage stage = room_node.World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
                    spawn_pt = stage.GetChildrenOfType<SpawnPoint>().FirstOrDefault(x => x.Room == room_node.RoomIndex);
                    if (spawn_pt != null)
                    {
                        room = spawn_pt.Room;
                        spawn = spawn_pt.SpawnID;
                    }
                }
            }
        }
    }
}
