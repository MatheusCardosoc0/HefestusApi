using HefestusApi.Models.Pessoal;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Produtos
{
    public class ProductSubGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
