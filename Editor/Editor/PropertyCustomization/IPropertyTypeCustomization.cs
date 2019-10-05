using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WindEditor.ViewModel;

namespace WindEditor.Editor
{
    public interface IPropertyTypeCustomization
    {
        /// <summary>
        /// Generates one or more WDetailSingleRowViewModels for the given property.
        /// </summary>
        /// <param name="property">The property to create property rows for</param>
        /// <param name="source">The object containing the property</param>
        /// <returns>A list of property rows to be added to the details panel</returns>
        List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source, SourceScene source_scene);

        void CustomizeChildren();
    }
}
