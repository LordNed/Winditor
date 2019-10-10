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

    public struct WProperty
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

        public WProperty(string type, string prop, string cat, string disp, string tip = "", bool vis = true)
        {
            TypeName = type;
            PropertyName = prop;
            CategoryName = cat;
            DisplayName = disp;
            ToolTip = tip;
            Visible = vis;
        }
    }
}
