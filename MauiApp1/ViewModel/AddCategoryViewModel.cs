using BL.Services.Categories;
using CommunityToolkit.Mvvm.ComponentModel;
using DAL.LocaleConverters;
using DAL.Models;

using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public partial class AddCategoryViewModel : ViewModelBase
    {
        private readonly ICategoryService _categoryService;

        [ObservableProperty]
        public string categoryName;

        [ObservableProperty]
        public string selectedCategoryType = "Доходы";

        [ObservableProperty]
        public ObservableCollection<CategoryViewModel> categoryList = new ();

        [ObservableProperty]
        public ObservableCollection<string> categoryTypeList = new() { "Доходы", "Расходы" };

        public Command AddCategoryCommand => new(AddCategoryHandler);

        public AddCategoryViewModel(ICategoryService categoryService) 
        {
            _categoryService = categoryService;

            var categories = categoryService.GetAllCategories();

            categories.ForEach(cat =>
            {
                var category = new CategoryViewModel
                {
                    Name = cat.Name,
                    Type = cat.Type
                };
                category.DeleteCategoryEvent += DeleteCategoryHandler;

                categoryList.Add(category);
            });
        }

        private void AddCategoryHandler()
        {
            if (CategoryName == string.Empty)
            {
                return;
            }

            var category = new Category()
            {
                Name = CategoryName,
                Type = CategoryTypeToRussianConverter.GetEnum(SelectedCategoryType),
            };

            if (!_categoryService.AddCategory(category))
            {
                return;
            }

            var categoryVm = new CategoryViewModel
            {
                Name = category.Name,
                Type = category.Type
            };
            categoryVm.DeleteCategoryEvent += DeleteCategoryHandler;

            CategoryList.Add(categoryVm);
        }

        private void DeleteCategoryHandler(CategoryViewModel category)
        {
            _categoryService.RemoveCategory(category.Name);

            CategoryList.Remove(category);
            category.DeleteCategoryEvent -= DeleteCategoryHandler;
        }
    }
}
