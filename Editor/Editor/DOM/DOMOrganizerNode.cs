using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.a
{
    public class WDOMOrganizerNode : WDOMNode
    {
        public Type ContainingType { get; protected set; }

        public WDOMOrganizerNode(WWorld world, Type contained_type, string header) : base(world)
        {
            Name = header;
            ContainingType = contained_type;
        }

        #region Overrides
        public override void Copy()
        {
            base.Copy();
        }

        public override void Cut()
        {
            base.Cut();
        }

        public override void Paste()
        {
            base.Paste();
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}