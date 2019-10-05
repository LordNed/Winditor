using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public enum SourceScene
    {
        Room,
        Stage
    }

    public class WProperty : System.Attribute
    {
        private string Category;
        private string DisplayName;
        private string ToolTip;
        private bool IsEditable;
        private SourceScene SourceScene;

        public WProperty(string category, string name, bool editable, string tip="", SourceScene source=SourceScene.Room)
        {
            Category = category;
            DisplayName = name;
            IsEditable = editable;
            ToolTip = tip;
            SourceScene = source;
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
