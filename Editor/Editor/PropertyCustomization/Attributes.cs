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

    public class WProperty_new
    {
        /// <summary>
        /// The name of the type of the property to be bound to the UI.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// The name of the property from the containing class to be bound.
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// The name of the category that this property should be put into in the property grid.
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// The name that should be displayed for this property in the property grid.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The tooltip that should be used for this property in the property grid.
        /// </summary>
        public string ToolTip { get; set; }
        /// <summary>
        /// Determines if the property's control in the property grid is visible to the user.
        /// </summary>
        public bool Visible { get; set; }

        public WProperty_new(string type, string prop, string cat, string disp, string tip = "", bool vis = true)
        {
            TypeName = type;
            PropertyName = prop;
            CategoryName = cat;
            DisplayName = disp;
            ToolTip = tip;
            Visible = vis;
        }
    }

    public class WIntProperty : WProperty_new
    {
        public bool IsSigned { get; set; }
        public int Increment { get; set; }

        public WIntProperty(string type, string prop, string cat, string disp, int increment, string tip = "", bool vis = true)
            : base(type, prop, cat, disp, tip, vis)
        {
            Increment = increment;
        }
    }

    public class WActorReferenceProperty : WProperty_new
    {
        public SourceScene SourceScene { get; set; }

        public WActorReferenceProperty(string type, string prop, string cat, string disp, SourceScene src, string tip = "", bool vis = true)
            : base(type, prop, cat, disp, tip, vis)
        {
            SourceScene = src;
        }
    }
}
