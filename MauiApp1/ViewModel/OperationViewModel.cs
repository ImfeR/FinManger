using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace UI.ViewModel
{
    public partial class OperationViewModel : ViewModelBase
    {
        public delegate void OperationDeleteHandler(OperationViewModel category);
        public event OperationDeleteHandler DeleteOperationEvent;

        public delegate void OpenAboutOperationHandler(OperationViewModel category);
        public event OpenAboutOperationHandler OpenAboutOperationEvent;

        public ICommand DeleteOperation => new Command(() => DeleteOperationEvent?.Invoke(this));

        public ICommand OpenAboutOperation => new Command(() => OpenAboutOperationEvent?.Invoke(this));

        public Guid Id { get; set; }

        [ObservableProperty]
        public string categoryType;

        [ObservableProperty]
        public double amount;

        [ObservableProperty]
        public string customCategoryName;

        [ObservableProperty]
        public string comment;
    }
}
