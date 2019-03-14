using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public class WProperty : System.Attribute
    {
        private string Category;
        private string DisplayName;
        private string ToolTip;
        private bool IsEditable;

        public WProperty(string category, string name, bool editable, string tip="")
        {
            Category = category;
            DisplayName = name;
            IsEditable = editable;
            ToolTip = tip;
        }
    }

    public class HideCategoriesAttribute : System.Attribute
    {
        private string[] HiddenCategories;

        public HideCategoriesAttribute()
        {
            HiddenCategories = new string[1];
        }

        public HideCategoriesAttribute(string[] categories)
        {
            HiddenCategories = categories;
        }

        public bool CategoryHidden(string cat)
        {
            return HiddenCategories.Contains(cat);
        }
    }
}
