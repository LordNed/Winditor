using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace WindEditor
{
    public class PlaytestManager
    {
        private Process m_DolphinInstance;

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
            Patch p = new Patch(@"resources/patches/room_playtest_diff.txt");
            p.ApplyToFile(@"D:\SZS Tools\Root Pure\&&systemdata\patch_test.dol");

            /*string dol_path = Path.Combine(Directory.GetCurrentDirectory(), "dolphin", "Dolphin.exe");
            m_DolphinInstance = Process.Start(Path.Combine(Directory.GetCurrentDirectory(), "dolphin", "Dolphin.exe"));

            m_DolphinInstance.EnableRaisingEvents = true;
            m_DolphinInstance.Exited += OnDolphinExited;*/
        }

        private void OnDolphinExited(object sender, EventArgs e)
        {
            m_DolphinInstance = null;
        }
    }
}
