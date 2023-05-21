using BL.Services.Categories;
using BL.Services.Operations;
using DAL._Enums_;
using DAL.LocaleConverters;
using DAL.Models;
using System.Collections.ObjectModel;
using UI.ViewModel;

namespace UI.View;

public partial class AddOperationView : ContentPage
{
    private bool isNavigationClicked = false;
	private readonly ICategoryService _categoryService;
    private readonly IOperationService _operationService;
	private readonly AddOperationViewModel _viewModel;
	private readonly List<string> _categoriesList = new() { "Доходы", "Расходы" };

    public ObservableCollection<Category> categories = new();
    public ObservableCollection<OperationViewModel> operations = new();

    public AddOperationView(AddOperationViewModel viewModel, 
        ICategoryService categoryService,
        IOperationService operationService)
	{
		_categoryService = categoryService;
        _operationService = operationService;
        _viewModel = viewModel;

        _categoryService.CategoryAddedEvent += CategoryAddedHandler;
        _categoryService.CategoryDeletedEvent += CategoryDeleteHandler;

        InitializeComponent();

        _viewModel.Amount = 1000;
        _viewModel.Comment = string.Empty;
        _viewModel.SelectedCategoryType = CategoryTypeToRussianConverter.GetEnum(_categoriesList.FirstOrDefault());
        _categoryService.GetAllCategories()
            .Where(cat => cat.Type ==_viewModel.SelectedCategoryType)
            .ToList()
            .ForEach(category => categories.Add(category));

        CategoryPicker.ItemsSource = _categoriesList;
		CategoryPicker.SelectedItem = CategoryTypeToRussianConverter.GetLocale(_viewModel.SelectedCategoryType);

		AmountEntry.Text = _viewModel.Amount.ToString();
		CommentEditor.Text = _viewModel.Comment;

		CustomCategoryPicker.ItemsSource = categories;
		CustomCategoryPicker.ItemDisplayBinding = new Binding("Name");

        CustomCategoryPicker.SelectedItem = null;

        OpeartionCollectionView.ItemsSource = operations;

        var operationList = _operationService.GetLastOperation(50);

        operationList.Reverse();
        operationList.ForEach(oper =>
        {
            var operationVM = new OperationViewModel()
            {
                Id = oper.Id,
                Amount = oper.Amount,
                Comment = oper.Comment,
                CategoryType = CategoryTypeToRussianConverter.GetLocale(oper.CategoryType),
                CustomCategoryName = oper.Categories != null ? oper.Categories.Name : string.Empty,
            };

            operationVM.DeleteOperationEvent += OnOperationDeleteHandler;
            operationVM.OpenAboutOperationEvent += OnOpenAboutOperationHandler;

            operations.Add(operationVM);
        });
    }

    private void GoToAddCategoryButtonClicked(object sender, EventArgs e)
    {
        isNavigationClicked = true;
		_viewModel.OpenAddCategoryPageHandler();
    }

    private async void AddOperationButtonClicked(object sender, EventArgs e)
    {
        if (_viewModel.Amount == 0.0)
        {
            await DisplayAlert("Уведомление", "Введены некорректные данные", "ОK");
            return;
        }

        var operation = _viewModel.AddNewOpration();

        var operationVM = new OperationViewModel()
        {
            Id = operation.Id,
            Amount = operation.Amount,
            Comment = operation.Comment,
            CategoryType = CategoryTypeToRussianConverter.GetLocale(operation.CategoryType),
            CustomCategoryName = operation.Categories != null ? operation.Categories.Name : string.Empty,
        };

        operationVM.DeleteOperationEvent += OnOperationDeleteHandler;
        operationVM.OpenAboutOperationEvent += OnOpenAboutOperationHandler;

        operations.Insert(0, operationVM);

        ResetToDefaultValues();
    }

    private void ResetToDefaultValues()
    {
        _viewModel.Amount = 1000;
        _viewModel.Comment = string.Empty;
        _viewModel.SelectedCategoryType = CategoryTypeToRussianConverter.GetEnum(_categoriesList.FirstOrDefault());
        _viewModel.SelectedAdditionalCategory = null;

		CategoryPicker.SelectedItem = CategoryTypeToRussianConverter.GetLocale(_viewModel.SelectedCategoryType);
        AmountEntry.Text = _viewModel.Amount.ToString();
        CommentEditor.Text = _viewModel.Comment;
        CustomCategoryPicker.SelectedItem = null;
    }

    private void OnOperationDeleteHandler(OperationViewModel operationViewModel)
    {
        _operationService.TryRemoveOperation(operationViewModel.Id);

        operations.Remove(operationViewModel);

        operationViewModel.DeleteOperationEvent -= OnOperationDeleteHandler;
        operationViewModel.OpenAboutOperationEvent -= OnOpenAboutOperationHandler;
    }

    private async void OnOpenAboutOperationHandler(OperationViewModel operationViewModel)
    {
        _operationService.TryRemoveOperation(operationViewModel.Id);

        await DisplayAlert("Информация об операции", $"Тип: {operationViewModel.CategoryType}\n" 
            + $"Сумма: {operationViewModel.Amount}\n" 
            + $"Комментарий: {operationViewModel.Comment}\n" 
            + $"Подкатегория: {operationViewModel.CustomCategoryName}", "Заркыть");
    }

    private void AmountEditorValueChanged(object sender, TextChangedEventArgs e)
    {
		var entry = ((Entry)sender);

		if (!double.TryParse(e.NewTextValue, out var newValue))
		{
            _viewModel.Amount = 0.0;
            return;
		}

		if (newValue < 0)
		{
            entry.Text = e.OldTextValue;
            return;
        }

		_viewModel.Amount = newValue;
	}

    private void CommentEditorTextChanged(object sender, TextChangedEventArgs e)
    {
		_viewModel.Comment = e.NewTextValue;
    }

    private void CategoryPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = ((Picker)sender);

        var newValue = CategoryTypeToRussianConverter.GetEnum(picker.SelectedItem.ToString());

        if (_viewModel.SelectedCategoryType != newValue)
        {
            UpdateCustomCutegoryList(newValue, true);
        }

        _viewModel.SelectedCategoryType = newValue;
    }

    private void UpdateCustomCutegoryList(CategoryTypes categoryType, bool needToResetPickerValue)
    {
        categories.Clear();

        _categoryService.GetAllCategories()
            .Where(cat => cat.Type == categoryType)
            .ToList()
            .ForEach(category => categories.Add(category));

        if (needToResetPickerValue)
        {
            CustomCategoryPicker.SelectedItem = null;
            _viewModel.SelectedAdditionalCategory = null;
        }
    }

    private void CustomCategoryPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        Picker picker = ((Picker)sender);

        _viewModel.SelectedAdditionalCategory = (Category)picker.SelectedItem;
    }

    private void CategoryAddedHandler(Category category)
    {
        categories.Add(category);

        UpdateCustomCutegoryList(_viewModel.SelectedCategoryType, false);
    }

    private void CategoryDeleteHandler(Category category)
    {
        categories.Remove(category);

        UpdateCustomCutegoryList(_viewModel.SelectedCategoryType, false);
    }

    private void ContentPageDisappearing(object sender, EventArgs e)
    {
        if (!isNavigationClicked)
        {
            _categoryService.CategoryDeletedEvent -= CategoryDeleteHandler;
            _categoryService.CategoryAddedEvent -= CategoryAddedHandler;
        }
    }
}