using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class LargeRock
    {
        public override void PostLoad()
        {
            GetPropertiesFromParameters();

            switch(Name)
            {
                case "Ebrock":
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Bombable Rock");
                    break;
                case "Eskban":
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Bombable Rock 2");
                    break;
                case "Ebrock2":
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Bombable Rock");
                    break;
                case "Ekao":
                    m_actorMeshes = WResourceManager.LoadActorResource("Headstone");
                    break;
            }
        }
    }
}
