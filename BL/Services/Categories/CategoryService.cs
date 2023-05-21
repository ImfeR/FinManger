using DAL.Models;
using DAL.Repositories;

namespace BL.Services.Categories
{
    /// <inheritdoc cref="ICategoryService" />
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _localCategories = new();
        private readonly IRepository<Category> _categoryRepository;

        public event ICategoryService.CategoryAdded CategoryAddedEvent;
        public event ICategoryService.CategoryAdded CategoryDeletedEvent;

        public CategoryService() 
        {
            _categoryRepository = new CategoryRepository();

            //_localCategories = _categoryRepository.LoadListDataAsync().Result;
        }

        /// <inheritdoc />
        public bool AddCategory(Category category)
        {
            if (_localCategories.Find(cat => category.Name == cat.Name) != null)
                return false;

            _localCategories.Add(category);

            CategoryAddedEvent?.Invoke(category);

            //_categoryRepository.SaveListDataAsync(_localCategories);

            return true;
        }

        /// <inheritdoc />
        public List<Category> GetAllCategories()
        {
            return _localCategories;
        }

        /// <inheritdoc />
        public void RemoveCategory(string name)
        {
            var category = _localCategories.FirstOrDefault(category => category.Name == name);

            if (category != null)
            {
                _localCategories.Remove(category);
            }

            CategoryDeletedEvent?.Invoke(category);
        
            //_categoryRepository.SaveListDataAsync(_localCategories);
        }
    }
}
