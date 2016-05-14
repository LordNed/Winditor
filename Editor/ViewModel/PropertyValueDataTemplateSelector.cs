using System.Windows;
using System.Windows.Controls;

namespace WindEditor.ViewModel
{
    public class PropertyValueDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BooleanDataTemplate { get; set; }
        public DataTemplate ByteDataTemplate { get; set; }
        public DataTemplate ShortDataTemplate { get; set; }
        public DataTemplate IntDataTemplate { get; set; }
        public DataTemplate StringDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            IPropertyValueAggregate val = item as IPropertyValueAggregate;
            if (val is BaseValueAggregate<bool>)
                return BooleanDataTemplate;
            else if (val is BaseValueAggregate<byte>)
                return ByteDataTemplate;
            else if (val is BaseValueAggregate<short>)
                return ShortDataTemplate;
            else if (val is BaseValueAggregate<int>)
                return IntDataTemplate;
            else if (val is BaseValueAggregate<string>)
                return StringDataTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
