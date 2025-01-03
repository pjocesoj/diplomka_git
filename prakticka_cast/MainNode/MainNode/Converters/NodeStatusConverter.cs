using MainNode.Logic;
using MainNode.Logic.Enums;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MainNode.Converters
{
    public class NodeStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (ConnectionStatus)value;
            switch (status.Status)
            {
                case ConnectionStatusEnum.GOOD:
                    return Brushes.Green;
                case ConnectionStatusEnum.WITH_PROBLEMS:
                    return Brushes.Yellow;
                case ConnectionStatusEnum.LOST:
                    return Brushes.Red;
                case ConnectionStatusEnum.RECOVERING:
                    return Brushes.Yellow;
                default:
                    return Brushes.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
