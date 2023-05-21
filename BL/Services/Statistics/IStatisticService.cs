using DAL._Enums_;
using DAL.Models;

namespace BL.Services.Statistics
{
    public interface IStatisticService
    {
        List<Statistic> GetMonthStatisticByCategoryType(CategoryTypes type, int year, int month);
    }
}
