using BL.Services.Operations;
using DAL._Enums_;
using DAL.Models;

namespace BL.Services.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly IOperationService _operationService;

        public StatisticService(IOperationService operationService) 
        {
            _operationService = operationService;
        }

        public List<Statistic> GetMonthStatisticByCategoryType(CategoryTypes type, int year, int month)
        {
            return _operationService.GetAllOperationByCategoryInMonth(type, year, month)
                .GroupBy((opration) => opration.Categories)
                .Select((category) => new Statistic
                {
                    CustomCategoryName = category.Key == null ? "Общее" : category.Key.Name,
                    Amount = category.Sum(x => x.Amount),
                })
                .ToList();
        }
    }
}
