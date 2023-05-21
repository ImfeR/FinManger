using DAL.Models;
using System.Text.Json;

namespace DAL.Repositories
{
    public class OperationRepository : IRepository<Operation>
    {
        private const string Key = "operations";

        public async Task<List<Operation>> LoadListDataAsync()
        {
            try
            {
                var result = await SecureStorage.GetAsync(Key);
                return JsonSerializer.Deserialize<List<Operation>>(result);
            }
            catch (Exception ex) 
            {
                return new List<Operation>(); 
            }
        }

        public async Task SaveListDataAsync(List<Operation> datas)
        {
            var result = JsonSerializer.Serialize(datas);

            await SecureStorage.SetAsync(Key, result);
        }
    }
}
