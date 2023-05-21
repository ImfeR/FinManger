using DAL.Models;
using System.Text.Json;

namespace DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private const string Key = "categories";

        public async Task<List<Category>> LoadListDataAsync()
        {
            try
            {
                string result = await SecureStorage.GetAsync(Key);
                return JsonSerializer.Deserialize<List<Category>>(result);
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }

        }

        public async Task SaveListDataAsync(List<Category> datas)
        {
            var result = JsonSerializer.Serialize(datas);

            await SecureStorage.SetAsync(Key, result);
        }
    }
}
