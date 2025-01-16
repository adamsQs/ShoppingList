using Microsoft.Maui.Controls;
using System.Globalization;

namespace ShoppingList.Converters
{
    public class BoolToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isBought && isBought)
                return new Style(typeof(Label))
                {
                    Setters = {
                        new Setter { Property = Label.TextColorProperty, Value = Colors.Gray }
                    }
                };
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}