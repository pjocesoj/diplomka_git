using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MainNode.Converters
{
    internal class TypeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = (value as Type);

            if(type==typeof(int))
            {
                return Brushes.Blue;
            }
            else if (type == typeof(float))
            {
                return Brushes.Orange;
            }
            else if (type == typeof(bool))
            {
                return Brushes.Green;
            }
            else
            {
                return Brushes.Gray;
            }
            /*
            switch (value)
            {
                case FlowResult<int>:
                    return Brushes.Blue;
                case FlowResult<float>:
                    return Brushes.Orange;
                case FlowResult<bool>:
                    return Brushes.Green;
                default:
                    return Brushes.Gray;
            }
            */
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
