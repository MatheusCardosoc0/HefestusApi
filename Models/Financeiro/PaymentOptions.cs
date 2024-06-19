using HefestusApi.Models.Vendas;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Financeiro
{
    public class PaymentOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isUseCreditLimit { get; set; }
        [JsonIgnore]
        public List<Order> Orders { get; set; }
        public string SystemLocationId { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;
    }
}
