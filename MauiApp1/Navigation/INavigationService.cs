namespace UI.Navigation
{
    public interface INavigationService
    {
        Task NavigateBack();

        Task NavigateToMainPage();

        Task NavigateToAddOperationPage();

        Task NavigateToAddCategoryPage();

        Task NavigateToShowStatisticPage();
    }
}
