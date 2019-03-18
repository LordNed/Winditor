using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class stone2
    {
        public override void PostLoad()
        {
            switch(Name)
            {
                case "Ebrock":
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Bombable Rock");
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
