using JStudio.J3D;
using Newtonsoft.Json;
using OpenTK;
using System;

namespace WindEditor
{
	public partial class Actor
    {
        [JsonIgnore]
        [WProperty("Entity", "English Name", false, "", SourceScene.Room)]
        new public string EnglishName
        {
            get
            {
                if (WResourceManager.GetActorDescriptors().ContainsKey(Name))
                {
                    return WResourceManager.GetActorDescriptors()[Name].Description;
                }
                return "";
            }
        }

        public override string ToString()
		{
			return Name;
		}

        public override void PostLoad()
        {
            base.PostLoad();
        }

        public override void PopulateDefaultProperties()
        {
            base.PopulateDefaultProperties();
            EnemyNumber = -1;
        }
    }
}
