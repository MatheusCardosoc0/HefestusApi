using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Administracao
{
    public class SubLocation
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string SystemLocationId { get;}
        [JsonIgnore]
        public SystemLocation SystemLocation { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;

        [JsonIgnore]
        public List<Stock> Stocks { get; set; }
    }
}
