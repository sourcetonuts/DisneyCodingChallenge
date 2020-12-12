using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CodingChallenge.ValueConverters
{
    public class BoolToValueConverter<T> 
        : IValueConverter
    {
        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return FalseValue;
            else
                return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
    }


    public class BoolToVisibilityConverter : BoolToValueConverter<Visibility> { }
    public class BoolToIntConverter : BoolToValueConverter<int> { }
}
