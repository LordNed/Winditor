using System.Windows;
using System.Windows.Controls;

namespace WindEditor.ViewModel
{
    public class PropertyValueDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BooleanDataTemplate { get; set; }
        public DataTemplate ByteDataTemplate { get; set; }
        public DataTemplate StringDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            IPropertyValue val = item as IPropertyValue;
            if (val is TBoolPropertyValue)
                return BooleanDataTemplate;
            else if (val is BaseValueAggregate<string>)
                return StringDataTemplate;
            else if (val is BaseValueAggregate<byte>)
                return ByteDataTemplate;   

            return base.SelectTemplate(item, container);
        }
    }
}
