using UI.View;
using UI.ViewModel;

namespace UI.Extensions
{
    public static class RegisterViewExtension
    {
        public static IServiceCollection RegiseterViews(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MainPageView>();
            serviceCollection.AddTransient<MainPageViewModel>();
            
            serviceCollection.AddTransient<AddOperationView>();
            serviceCollection.AddTransient<AddOperationViewModel>();

            serviceCollection.AddTransient<AddCategoryView>();
            serviceCollection.AddTransient<AddCategoryViewModel>();

            serviceCollection.AddTransient<ShowStatisticPage>();
            serviceCollection.AddTransient<ShowStatisticViewModel>();

            return serviceCollection;
        }
    }
}
