using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WindEditor;
using Newtonsoft.Json;

namespace BulkDataProcessingScripts
{
    struct ActorRecord
    {
        public string Name;
        public string Stage;
        public string Room;

        public override string ToString()
        {
            return $"{ Name } in { Room } of { Stage }";
        }
    }

    class ActorInfoTxtsToLocation
    {
        public ActorInfoTxtsToLocation(string info_folder, string res_folder)
        {
            Dictionary<string, string> islands = ReadNames(Path.Combine(info_folder, "island_names.txt"));
            Dictionary<string, string> maps = ReadNames(Path.Combine(info_folder, "stage_names.txt"));
            ActorRecord[] records = ReadActorData(Path.Combine(info_folder, "All Entity Params.txt"));

            Dictionary<string, List<string>> actor_map = new Dictionary<string, List<string>>();

            foreach (ActorRecord r in records)
            {
                if (!actor_map.ContainsKey(r.Name))
                {
                    actor_map.Add(r.Name, new List<string>());
                }

                List<string> recorded_maps = actor_map[r.Name];

                string stage_name = $"{maps[r.Stage]} ({ r.Stage })";

                if (stage_name.Contains("Great Sea"))
                {
                    if (r.Room == "Stage")
                    {
                        stage_name = $"Great Sea ({r.Stage}/Stage)";
                    }
                    else
                    {
                        stage_name = $"{islands[r.Room]} ({r.Stage}/{r.Room})";
                    }
                }

                if (!recorded_maps.Contains(stage_name))
                {
                    recorded_maps.Add(stage_name);
                }
            }

            string databse_path = Path.Combine(res_folder, "ActorDatabase.json");
            WActorDescriptor[] allDescriptors = JsonConvert.DeserializeObject<WActorDescriptor[]>(File.ReadAllText(databse_path));

            foreach (WActorDescriptor desc in allDescriptors)
            {
                if (actor_map.ContainsKey(desc.ActorName))
                {
                    desc.Locations = actor_map[desc.ActorName].ToArray();
                }
                else
                {
                    desc.Locations = new string[] { "N/A" };
                }
            }

            File.WriteAllText(Path.Combine(res_folder, "ActorDatabase_test.json"), JsonConvert.SerializeObject(allDescriptors, Formatting.Indented));
        }

        private Dictionary<string, string> ReadNames(string file_path)
        {
            Dictionary<string, string> names = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(file_path);

            for (int i = 0; i < lines.Length; i += 2)
            {
                names.Add(lines[i].Replace(".arc", ""), lines[i + 1].Trim());
            }

            return names;
        }

        private ActorRecord[] ReadActorData(string file_path)
        {
            List<ActorRecord> records = new List<ActorRecord>();
            string[] lines = File.ReadAllLines(file_path);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] split_line = lines[i].Trim().Split(' ');
                string[] stage_room = split_line[5].Split('/');

                records.Add(new ActorRecord() { Name = split_line[0], Stage = stage_room[0], Room = stage_room[1] });
            }

            return records.ToArray();
        }
    }
}
