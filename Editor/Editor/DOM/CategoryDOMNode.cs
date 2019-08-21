using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public class CategoryDOMNode : WDOMNode
    {
        public string CategoryName { get; private set; }

        public CategoryDOMNode(string cat_name, WWorld world) : base(world)
        {
            CategoryName = cat_name;
            IsVisible = true;
            IsRendered = true;
            IsExpanded = true;
        }

        public override string ToString()
        {
            return CategoryName;
        }
    }
}
