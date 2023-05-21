namespace DAL.Repositories
{
    public interface IRepository<T>
    {
        Task SaveListDataAsync(List<T> datas);

        Task<List<T>> LoadListDataAsync();
    }
}
