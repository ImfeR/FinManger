using DAL._Enums_;
using DAL.LocaleConverters;
using System.Globalization;

namespace UI.View.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not CategoryTypes)
            {
                return string.Empty;
            }

            return CategoryTypeToRussianConverter.GetLocale((CategoryTypes)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string)
            {
                return CategoryTypes.Income;
            }

            if (Enum.TryParse((string)value, out CategoryTypes catetory))
            {
                return catetory;
            }

            return CategoryTypes.Income;
        }
    }
}
