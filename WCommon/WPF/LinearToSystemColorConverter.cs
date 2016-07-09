using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WindEditor.WPF
{
    public class LinearToSystemColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WLinearColor color = (WLinearColor)value;
            return Color.FromArgb((byte)(color.A*255f), (byte)(color.R * 255f), (byte)(color.G * 255f), (byte)(color.B * 255f));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            return new WLinearColor(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }
    }
}
