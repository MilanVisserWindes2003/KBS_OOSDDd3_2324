using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Skepta.Presentation.View;

public class BoolToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool condition && condition)
        {
            return Brushes.Blue;
        }
        else
        {
            return Brushes.Black;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
