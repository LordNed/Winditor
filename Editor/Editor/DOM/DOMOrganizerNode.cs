using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.a
{
    public class DOMOrganizerNode<T> : WDOMNode
        where T : WDOMNode
    {
        public override string Name { get; set; }

        public DOMOrganizerNode(WWorld world, string header) : base(world)
        {
            Name = header;
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