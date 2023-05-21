using BL.Services.Categories;
using BL.Services.Operations;
using DAL._Enums_;
using DAL.Models;
using UI.Navigation;

namespace UI.ViewModel
{
    public partial class AddOperationViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IOperationService _operationService;

        public double Amount = 0.0;

        public CategoryTypes SelectedCategoryType = CategoryTypes.Income;

        public string Comment = string.Empty;

        #nullable enable
        public Category? SelectedAdditionalCategory = null;

        public AddOperationViewModel(
            INavigationService navigationService,
            IOperationService operationService)
        {
            _navigationService = navigationService;
            _operationService = operationService;
        }

        public void OpenAddCategoryPageHandler()
        {
            _navigationService.NavigateToAddCategoryPage();
        }

        public Operation AddNewOpration()
        {
            var operation = new Operation()
            {
                Comment = Comment,
                Amount = Amount,
                CategoryType = SelectedCategoryType,
                Categories = SelectedAdditionalCategory,
            };

            var id = _operationService.AddOperation(operation);

            operation.Id = id;

            return operation;
        }
    }
}
