﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class npc_md
    {
        public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Medli");
        }
    }
}
