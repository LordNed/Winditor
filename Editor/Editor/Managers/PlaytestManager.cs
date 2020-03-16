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

namespace WindEditor
{
    public class PlaytestManager
    {
        private Process m_DolphinInstance;
        private ProcessStartInfo m_DolphinStartInfo;

        string m_DolPath;
        string m_BackupDolPath;

        public PlaytestManager()
        {
            m_DolphinInstance = null;
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

        private void StartPlaytest(WMap map, MapLayer active_layer)
        {
            string dolphinPath = Path.Combine(WSettingsManager.GetSettings().DolphinDirectory.FilePath, "Dolphin.exe");
            if (!File.Exists(dolphinPath))
            {
                MessageBox.Show("You must specify the path to Dolphin in the options menu before you can playtest.", "Dolphin not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Console.WriteLine($"Stage name: { map.MapName }, Room Name: { map.FocusedSceneLabel }");

            string map_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "stage", map.MapName);
            map.ExportToDirectory(map_path);

            string dol_dir = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys");
            m_DolPath = Path.Combine(dol_dir, "main.dol");
            m_BackupDolPath = Path.Combine(dol_dir, "main_backup.dol");

            m_DolphinStartInfo = new ProcessStartInfo(dolphinPath);
            m_DolphinStartInfo.Arguments = $"-b -e \"{ m_DolPath }\"";

            byte room_no = 0;
            byte spawn_id = 0;

            GetRoomAndSpawnID(map.FocusedScene, out room_no, out spawn_id);

            Patch p = JsonConvert.DeserializeObject<Patch>(File.ReadAllText(@"resources\patches\test_room_diff.json"));
            p.Files[0].Patchlets.Add(new Patchlet(2149765112, new List<byte>(Encoding.ASCII.GetBytes(map.MapName))));
            p.Files[0].Patchlets.Add(new Patchlet(2147824099, new List<byte>(new byte[] { spawn_id })));
            p.Files[0].Patchlets.Add(new Patchlet(2147824103, new List<byte>(new byte[] { room_no })));
            p.Files[0].Patchlets.Add(new Patchlet(2147824107, new List<byte>(new byte[] { (byte)(active_layer - 1) })));

            if (!File.Exists(m_DolPath))
            {
                Console.WriteLine("ISO root has no executable!");
                return;
            }

            if (File.Exists(m_BackupDolPath))
            {
                File.Delete(m_BackupDolPath);
            }

            File.Copy(m_DolPath, m_BackupDolPath);

            p.Apply(WSettingsManager.GetSettings().RootDirectoryPath);

            m_DolphinInstance = Process.Start(m_DolphinStartInfo);

            m_DolphinInstance.EnableRaisingEvents = true;
            m_DolphinInstance.Exited += OnDolphinExited;
        }

        private void OnDolphinExited(object sender, EventArgs e)
        {
            m_DolphinInstance.Exited -= OnDolphinExited;
            m_DolphinInstance = null;

            if (File.Exists(m_DolPath))
            {
                File.Delete(m_DolPath);
            }

            File.Copy(m_BackupDolPath, m_DolPath);
            File.Delete(m_BackupDolPath);
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

        private void GetRoomAndSpawnID(WScene scene, out byte room, out byte spawn)
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
