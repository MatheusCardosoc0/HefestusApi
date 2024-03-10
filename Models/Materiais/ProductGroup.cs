using System.Text.Json.Serialization;

namespace HefestusApi.Models.Produtos
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
