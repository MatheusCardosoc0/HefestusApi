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
    }
}
