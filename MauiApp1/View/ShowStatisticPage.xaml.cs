using UI.ViewModel;

namespace UI.View;

public partial class ShowStatisticPage : ContentPage
{
	private readonly ShowStatisticViewModel _viewModel;

	public ShowStatisticPage(ShowStatisticViewModel viewModel)
	{
		_viewModel = viewModel;

		InitializeComponent();

		BindingContext = viewModel;
	}
}