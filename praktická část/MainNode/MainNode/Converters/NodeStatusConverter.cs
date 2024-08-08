using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MainNode.Converters
{
    public class NodeStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fails = (short)value;
            if (fails == 0) { return Brushes.Green; }
            if (fails < 3) { return Brushes.Yellow; }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
