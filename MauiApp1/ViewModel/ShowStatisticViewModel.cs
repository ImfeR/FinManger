using BL.Services.Statistics;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.View.Converters;

namespace UI.ViewModel
{
    public partial class ShowStatisticViewModel : ViewModelBase
    {
        private readonly IStatisticService _statisticService;

        private int _selectedMonth;

        [ObservableProperty]
        public string selectedMonthName;

        [ObservableProperty]
        public int selectedYear;

        [ObservableProperty]
        public ObservableCollection<ISeries> incomeSeries = new ObservableCollection<ISeries>();

        [ObservableProperty]
        public ObservableCollection<ISeries> expenditureSeries = new ObservableCollection<ISeries>();

        public ICommand IncrementMonth => new Command(() =>
        {
            _selectedMonth = _selectedMonth == 12 ? 1 : _selectedMonth + 1;

            SelectedMonthName = IntToMonthNameConverter.MonthToString(_selectedMonth);
        
            SetStatisticValue();
        });

        public ICommand DencrementMonth => new Command(() =>
        {
            _selectedMonth = _selectedMonth == 1 ? 12 : _selectedMonth - 1;

            SelectedMonthName = IntToMonthNameConverter.MonthToString(_selectedMonth);
        
            SetStatisticValue();
        });

        public ICommand IncrementYear => new Command(() =>
        {
            SelectedYear = SelectedYear == 2150 ? SelectedYear : SelectedYear + 1;
        
            SetStatisticValue();
        });

        public ICommand DencrementYear => new Command(() =>
        {
            SelectedYear = SelectedYear == 2000 ? SelectedYear : SelectedYear - 1;

            SetStatisticValue();
        });

        public ShowStatisticViewModel(IStatisticService statisticService) 
        {
            var now = DateTime.Now;
            _selectedMonth = now.Month;

            SelectedMonthName = IntToMonthNameConverter.MonthToString(_selectedMonth);
            SelectedYear = now.Year;

            _statisticService = statisticService;

            SetStatisticValue();
        }

        private void SetStatisticValue()
        {
            IncomeSeries.Clear();
            ExpenditureSeries.Clear();

            var expend = _statisticService.GetMonthStatisticByCategoryType(DAL._Enums_.CategoryTypes.Income, SelectedYear, _selectedMonth);
            var expenditure = _statisticService.GetMonthStatisticByCategoryType(DAL._Enums_.CategoryTypes.Expenditure, SelectedYear, _selectedMonth);

            expend.ForEach((income) =>
            {
                var series = new PieSeries<double>
                {
                    Values = new double[] { income.Amount },
                    Name = $"{income.Amount} {income.CustomCategoryName}",
                    MiniatureShapeSize = 20
                };

                IncomeSeries.Add(series);
            });


            expenditure.ForEach((expend) =>
            {
                var series = new PieSeries<double>
                {
                    Values = new double[] { expend.Amount },
                    Name = $"{expend.Amount} {expend.CustomCategoryName}",
                    MiniatureShapeSize = 20
                };

                ExpenditureSeries.Add(series);
            });
        }
    }
}
