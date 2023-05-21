using UI.ViewModel;

namespace UI.View;

public partial class MainPageView : ContentPage
{
    public MainPageView(MainPageViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
	}
}

