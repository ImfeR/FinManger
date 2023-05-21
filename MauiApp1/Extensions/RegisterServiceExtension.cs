using UI.Navigation;
using BL.Services.Categories;
using BL.Services.Operations;
using BL.Services.Statistics;

namespace UI.Extensions
{
    public static class RegisterServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<ICategoryService, CategoryService>();
            serviceCollection.AddSingleton<IOperationService, OperationService>();
            serviceCollection.AddSingleton<IStatisticService, StatisticService>();

            return serviceCollection;
        }
    }
}
