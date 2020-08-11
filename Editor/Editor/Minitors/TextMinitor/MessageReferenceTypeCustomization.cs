using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WindEditor.ViewModel;
using WindEditor.Editor;

namespace WindEditor.Minitors.Text
{
    class MessageReferenceTypeCustomization : IPropertyTypeCustomization
    {
        private TextMinitor m_TextEditorRef;

        public MessageReferenceTypeCustomization(TextMinitor editor_ref)
        {
            m_TextEditorRef = editor_ref;
        }

        public void CustomizeChildren()
        {
            throw new NotImplementedException();
        }

        public List<WDetailSingleRowViewModel> CustomizeHeader(PropertyInfo property, string display_name, bool is_editable, object source)
        {
            WDetailSingleRowViewModel text_row = new WDetailSingleRowViewModel(display_name);

            TextReferenceControl textref = new TextReferenceControl();
            textref.IsEnabled = is_editable;
            textref.DoLookup = m_TextEditorRef.OnUserRequestOpenReference;

            MessageReference thing = property.GetValue(source) as MessageReference;

            Binding tbind = new Binding("MessageID")
            {
                Source = thing,
                Mode = is_editable ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            textref.SetBinding(TextReferenceControl.MessageIDProperty, tbind);

            text_row.PropertyControl = textref;

            return new List<WDetailSingleRowViewModel>() { text_row };
        }
    }
}
