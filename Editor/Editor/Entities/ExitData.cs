using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class ExitData
    {
        public override string ToString()
        {
            return $"Map: { MapName }, Room: { RoomIndex }, Spawn ID: { SpawnID }";
        }
    }
}
