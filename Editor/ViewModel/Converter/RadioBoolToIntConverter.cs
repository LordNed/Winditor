using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static WindEditor.ViewModel.KeysMenuViewModel;

namespace WindEditor.ViewModel
{
    [ValueConversion(typeof(KeyInputProfile), typeof(bool))]
    public class RadioBoolToIntConverter : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // viewmodel ---> view
            // value = active value, show as enum
            // parameter = the element to check 
            string theParameter = parameter as string;
            KeyInputProfile theValue = (KeyInputProfile) value;

            if (string.IsNullOrEmpty(theParameter) || !Enum.IsDefined(typeof(KeyInputProfile), theValue) || !parameter.Equals(theValue.ToString()))
            {
                return false;
            }
            else if (parameter.Equals(theValue.ToString()))
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // viewmodel <--- view
            // value = active value, boolean
            // parameter = the enum active value to set
            string theParameter = parameter as string;
            bool theValue = System.Convert.ToBoolean(value);
            if (!string.IsNullOrEmpty(theParameter) || theValue)
            {
                return parameter;
            }

            return KeyInputProfile.NotAnProfile;
        }
    }
}
