using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace WindEditor.Events
{
    public class EventActorDefinition
    {
        public string ActorName { get; set; }

        public EventActionDefinition[] Actions { get; set; }
    }

    public class EventActionDefinition
    {
        public string ActionName { get; set; }
        public string ReadableName { get; set; }
        public EventPropertyDefinition[] Properties { get; set; }
    }

    public class EventPropertyDefinition
    {
        public string PropertyName { get; set; }
        public string ReadableName { get; set; }
        public string Type { get; set; }
    }

    public class EventDefinitionManager
    {
        private static Dictionary<string, EventActorDefinition> m_EventActorDefinitions;

        static EventDefinitionManager()
        {
            m_EventActorDefinitions = new Dictionary<string, EventActorDefinition>();

            string jsonData = File.ReadAllText("resources/EventDefinitionDatabase.json");

            EventActorDefinition[] allDescriptors = JsonConvert.DeserializeObject<EventActorDefinition[]>(jsonData);
            foreach (var descriptor in allDescriptors)
            {
                if (string.IsNullOrEmpty(descriptor.ActorName))
                    continue;

                m_EventActorDefinitions.Add(descriptor.ActorName, descriptor);
            }
        }

        public static Dictionary<string, string> GetActionsForActor(string actor_name)
        {
            Dictionary<string, string> cut_names = new Dictionary<string, string>();

            EventActorDefinition def = m_EventActorDefinitions[actor_name];

            if (def == null)
                return cut_names;

            for (int i = 0; i < def.Actions.Length; i++)
            {
                cut_names.Add(def.Actions[i].ActionName, def.Actions[i].ReadableName != "" ? def.Actions[i].ReadableName : def.Actions[i].ActionName);
            }

            return cut_names;
        }

        public static Dictionary<string, string> GetPropertiesForAction(string actor_name, string action_name)
        {
            Dictionary<string, string> prop_names = new Dictionary<string, string>();

            EventActorDefinition def = m_EventActorDefinitions[actor_name];

            if (def == null)
                return prop_names;

            for (int i = 0; i < def.Actions.Length; i++)
            {
                if (def.Actions[i].ActionName == action_name)
                {
                    foreach (EventPropertyDefinition p in def.Actions[i].Properties)
                    {
                        prop_names.Add(p.PropertyName, p.ReadableName != "" ? p.ReadableName : p.PropertyName);
                    }

                    break;
                }
            }


            return prop_names;
        }

        public static SubstanceType GetPropertyType(string actor_name, string action_name, string prop_name)
        {
            SubstanceType sub = SubstanceType.Float;

            EventActorDefinition def = m_EventActorDefinitions[actor_name];

            if (def == null)
                return sub;

            for (int i = 0; i < def.Actions.Length; i++)
            {
                if (def.Actions[i].ActionName == action_name)
                {
                    foreach (EventPropertyDefinition p in def.Actions[i].Properties)
                    {
                        if (p.PropertyName == prop_name)
                        {
                            switch (p.Type)
                            {
                                case "Integer":
                                    sub = SubstanceType.Int;
                                    break;
                                case "Float":
                                    sub = SubstanceType.Float;
                                    break;
                                case "Vector3":
                                    sub = SubstanceType.Vec3;
                                    break;
                                case "String":
                                    sub = SubstanceType.String;
                                    break;
                            }
                        }
                    }

                    break;
                }
            }

            return sub;
        }

        public static string GetCutDisplayName(string actor_name, string cut_name)
        {
            EventActorDefinition def = m_EventActorDefinitions[actor_name];
            string disp_name = cut_name;

            if (def == null)
                return disp_name;

            foreach (EventActionDefinition action in def.Actions)
            {
                if (action.ActionName == cut_name)
                {
                    disp_name = action.ReadableName != "" ? action.ReadableName : cut_name;
                }
            }

            return disp_name;
        }
    }
}
