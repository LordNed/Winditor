using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

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

        public void RequestStartPlaytest(WMap map)
        {
            if (m_DolphinInstance != null)
            {
                m_DolphinInstance.CloseMainWindow();
                m_DolphinInstance.WaitForExit();
            }

            StartPlaytest(map);
        }

        private void StartPlaytest(WMap map)
        {
            Console.WriteLine($"Stage name: { map.MapName }, Room Name: { map.FocusedSceneLabel }");

            string map_path = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "files", "res", "stage", map.MapName);
            map.ExportToDirectory(map_path);

            string dol_dir = Path.Combine(WSettingsManager.GetSettings().RootDirectoryPath, "sys");
            m_DolPath = Path.Combine(dol_dir, "main.dol");
            m_BackupDolPath = Path.Combine(dol_dir, "main_backup.dol");

            m_DolphinStartInfo = new ProcessStartInfo(@"dolphin\Dolphin.exe");
            m_DolphinStartInfo.Arguments = $"-b -e \"{ m_DolPath }\"";

            byte room_no = 0;
            byte spawn_id = 0;

            GetRoomAndSpawnID(map.FocusedScene, out room_no, out spawn_id);

            Patch p = JsonConvert.DeserializeObject<Patch>(File.ReadAllText(@"resources\patches\test_room_diff.json"));
            p.Files[0].Patchlets.Add(new Patchlet(2149765112, new List<byte>(Encoding.ASCII.GetBytes(map.MapName))));
            p.Files[0].Patchlets.Add(new Patchlet(2147824099, new List<byte>(new byte[] { spawn_id })));
            p.Files[0].Patchlets.Add(new Patchlet(2147824103, new List<byte>(new byte[] { room_no })));
            p.Files[0].Patchlets.Add(new Patchlet(2147824107, new List<byte>(new byte[] { 255 })));

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

            Selection selected = scene.World.ActorEditor.EditorSelection;

            if (selected.PrimarySelectedObject is SpawnPoint)
            {
                SpawnPoint spawn_pt = (SpawnPoint)selected.PrimarySelectedObject;

                room = spawn_pt.Room;
                spawn = spawn_pt.SpawnIndex;
            }
            else
            {
                room = GetRoomNumberFromSceneName(scene.Name);
            }
        }
    }
}
