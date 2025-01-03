using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MainNode.Converters
{
    public class VisibleNotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (Visibility)value;
            if (val == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
