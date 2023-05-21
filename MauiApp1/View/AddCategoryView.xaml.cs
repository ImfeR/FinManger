
using DAL.LocaleConverters;
using UI.ViewModel;

namespace UI.View;

public partial class AddCategoryView : ContentPage
{
    private readonly AddCategoryViewModel _viewModel;

	public AddCategoryView(AddCategoryViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
		BindingContext = viewModel;
	}

    void OnCategoryChanged(object sender, EventArgs e)
    {
        Picker picker = ((Picker)sender);

        _viewModel.SelectedCategoryType = (string)picker.SelectedItem;
    }
}