using DAL._Enums_;
using DAL.Models;

namespace BL.Services.Operations
{
    public interface IOperationService
    {
        Guid AddOperation(Operation operation);

        void TryRemoveOperation(Guid id);

        List<Operation> GetAllOperationByCategoryInMonth(CategoryTypes catType, int year, int month);

        List<Operation> GetLastOperation(int count);
    }
}
