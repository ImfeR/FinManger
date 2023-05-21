using DAL._Enums_;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Operation
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("categoryType")]
        /// <summary>
        /// Тип операции (Доходы/Расходы)
        /// </summary>
        public CategoryTypes CategoryType { get; set; }

        [JsonPropertyName("amount")]
        /// <summary>
        /// Сумма в рублях
        /// </summary>
        public double Amount { get; set; }

        [JsonPropertyName("category")]
        /// <summary>
        /// Список дополнительных категорий
        /// </summary>
        #nullable enable
        public Category? Categories { get; set; }

        [JsonPropertyName("date")]
        /// <summary>
        /// Дата операции
        /// </summary>
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [JsonPropertyName("comment")]
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; } = string.Empty;
    }
}
