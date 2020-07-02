using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace WindEditor.Editor.Events
{
    public class WEventActor : WDOMNode
    {
        /// <summary>
        /// Reads actor data from the given stream and grabs actions from the given list.
        /// </summary>
        /// <param name="world">World containing the event actor</param>
        /// <param name="reader">Stream to load actor data from</param>
        /// <param name="actors">List of actions to assign to this event</param>
        public WEventActor(WWorld world, EndianBinaryReader reader/*, List<EventActor> actors*/) : base(world)
        {
            Name = new string(reader.ReadChars(32)).Trim('\0');
        }
    }
}
