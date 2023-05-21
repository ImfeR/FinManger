using DAL.Models;

namespace BL.Services.Categories
{
    /// <summary>
    /// Сервис, отвечающий за работу с категориями.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Получить все категории.
        /// </summary>
        /// <returns>Списко категорий.</returns>
        List<Category> GetAllCategories();

        /// <summary>
        /// Добавить категорию.
        /// </summary>
        /// <param name="category">Добавляемая категория.</param>
        bool AddCategory(Category category);

        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <param name="name">Название категории.</param>
        void RemoveCategory(string name);

        public delegate void CategoryDeleted(Category category);
        public event CategoryAdded CategoryAddedEvent;

        public delegate void CategoryAdded(Category category);
        public event CategoryAdded CategoryDeletedEvent;
    }
}
