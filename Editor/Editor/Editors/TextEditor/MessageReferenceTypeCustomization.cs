using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WindEditor.Editor;

namespace WindEditor.Editors.Text
{
    class MessageReferenceTypeCustomization : IPropertyTypeCustomization
    {
        private TextEditor m_TextEditorRef;

        public MessageReferenceTypeCustomization(TextEditor editor_ref)
        {
            m_TextEditorRef = editor_ref;
        }

        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            throw new NotImplementedException();
        }
    }
}
