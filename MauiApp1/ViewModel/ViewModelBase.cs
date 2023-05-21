using CommunityToolkit.Mvvm.ComponentModel;

namespace UI.ViewModel
{
    public class ViewModelBase : ObservableObject
    {
        public virtual Task OnNavigatingTo(object parameter)
            => Task.CompletedTask;

        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;

        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;
    }
}
