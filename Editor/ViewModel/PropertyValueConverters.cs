using System;
using System.Globalization;
using System.Windows.Data;

namespace WindEditor.ViewModel
{
    public class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte val = (byte)value;
            return val.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            byte output;
            if (byte.TryParse(val, out output))
                return output;

            Console.WriteLine("Failed to convert back!");
            return null;
        }
    }
}
