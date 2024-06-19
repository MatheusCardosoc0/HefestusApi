using System.Text.Json.Serialization;

namespace HefestusApi.Models.Produtos
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
        public string SystemLocationId { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;
    }
}
