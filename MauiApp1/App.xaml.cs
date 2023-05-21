using UI.Navigation;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace UI;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

        MainPage = new NavigationPage();
        navigationService.NavigateToMainPage();

        LiveCharts.Configure((config) =>
            config
                .AddSkiaSharp()
                .AddDefaultMappers()
                .AddLightTheme()
        );
    }
}
