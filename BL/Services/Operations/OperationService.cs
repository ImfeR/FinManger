using BL.Services.Categories;
using DAL._Enums_;
using DAL.Models;
using DAL.Repositories;

namespace BL.Services.Operations
{
    public class OperationService : IOperationService
    {
        private readonly ICategoryService _categoryService;
        private readonly IRepository<Operation> _operationRepository;
        private readonly List<Operation> _currentOperationList = new ();

        public OperationService(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            _categoryService.CategoryDeletedEvent += CategoryDeletedEventHandler;
            //_currentOperationList = _operationRepository.LoadListDataAsync().Result;
        }

        public List<Operation> GetAllOperationByCategoryInMonth(CategoryTypes catType, int year, int month)
        {
            return _currentOperationList.Where(oper => 
                    oper.CategoryType == catType && 
                    oper.Date.Year == year && 
                    oper.Date.Month == month)
                .ToList();
        }

        public Guid AddOperation(Operation operation)
        {
            operation.Id = Guid.NewGuid();
            
            _currentOperationList.Add(operation);

            //_operationRepository.SaveListDataAsync(_currentOperationList);

            return operation.Id;
        }

        public void TryRemoveOperation(Guid id)
        {
            var operation = _currentOperationList.Find(oper => oper.Id == id);

            if (operation != null)
            {
                _currentOperationList.Remove(operation);
            }

            //_operationRepository.SaveListDataAsync(_currentOperationList);
        }

        private void CategoryDeletedEventHandler(Category category)
        {
            var operations = _currentOperationList.Where(oper => oper.Categories.Name == category.Name);

            foreach (var operation in operations)
            {
                operation.Categories = null;
            }

            //_operationRepository.SaveListDataAsync(_currentOperationList);
        }

        public List<Operation> GetLastOperation(int count)
        {
            return _currentOperationList.TakeLast(count).ToList();
        }
    }
}
