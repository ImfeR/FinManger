using DAL._Enums_;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Category
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("categoryType")]
        public CategoryTypes Type { get; set; }
    }
}
