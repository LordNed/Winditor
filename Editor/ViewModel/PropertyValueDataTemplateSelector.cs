using System.Windows;
using System.Windows.Controls;

namespace WindEditor.ViewModel
{
    public class PropertyValueDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate BooleanDataTemplate { get; set; }
        public DataTemplate StringDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            IPropertyValue val = item as IPropertyValue;
            if(val is TBoolPropertyValue)
            {
                return BooleanDataTemplate;
            }
            else if(val is TStringValueAggregate)
            {
                return StringDataTemplate;
            }

            //return DefaultDataTemplate
            return base.SelectTemplate(item, container);
        }
    }
}
