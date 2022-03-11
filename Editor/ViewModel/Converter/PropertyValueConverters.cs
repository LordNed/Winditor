using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace WindEditor.ViewModel
{
    public class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "--";

            byte val = (byte)value;
            return val.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            byte output;
            if (byte.TryParse(val, out output))
                return output;

            return new ValidationResult(false, "Not a byte");
        }
    }

    public class ShortToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "--";

            short val = (short)value;
            return val.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            short output;
            if (short.TryParse(val, out output))
                return output;

            return new ValidationResult(false, "Not a short");
        }
    }

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "--";

            int val = (int)value;
            return val.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            int output;
            if (int.TryParse(val, out output))
                return output;

            return new ValidationResult(false, "Not a int");
        }
    }
}
