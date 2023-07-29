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
            return $"[{Parent.Children.IndexOf(this)}] Map: { MapName }, Room: { RoomIndex }, Spawn ID: { SpawnID }";
        }
    }
}
