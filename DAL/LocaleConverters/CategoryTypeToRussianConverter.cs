using DAL._Enums_;

namespace DAL.LocaleConverters
{
    public static class CategoryTypeToRussianConverter
    {
        private static readonly Dictionary<string, CategoryTypes> _dictToEnum = new()
        {
            {"Доходы", CategoryTypes.Income },
            {"Расходы", CategoryTypes.Expenditure }
        };

        private static readonly Dictionary<CategoryTypes, string> _dictToLocale = new()
        {
            {CategoryTypes.Income, "Доходы" },
            {CategoryTypes.Expenditure, "Расходы" }
        };

        public static string GetLocale(CategoryTypes type)
        {
            return _dictToLocale[type];
        }

        public static CategoryTypes GetEnum(string locale)
        {
            return _dictToEnum[locale];
        }
    }
}
