using DAL._Enums_;

namespace UI.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        public delegate void CategoryDeleteHandler(CategoryViewModel category);
        public event CategoryDeleteHandler DeleteCategoryEvent;

        public Command DeleteCategoryCommand => new(DeleteCategoryCommandHandler);

        public string Name { get; set; }
        
        public CategoryTypes Type { get; set; }

        private void DeleteCategoryCommandHandler()
        {
            DeleteCategoryEvent?.Invoke(this);
        }
    }
}
