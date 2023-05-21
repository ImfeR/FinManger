using UI.Navigation;

namespace UI.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public Command GoToAddOperationPage
            => new(async () => await _navigationService.NavigateToAddOperationPage());

        public Command GoToAddCategoryPage
            => new(async () => await _navigationService.NavigateToAddCategoryPage());

        public Command GoToShowStatisticPage 
            => new(async () => await _navigationService.NavigateToShowStatisticPage());

        public MainPageViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
        }
    }
}
