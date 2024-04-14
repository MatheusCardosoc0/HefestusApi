
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Administracao
{
    public class SystemLocation
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int PersonId { get; set; }
        public required Person Person { get; set; }
        [JsonIgnore]
        public List<Stock>? Stocks { get; set; }
        [JsonIgnore]
        public List<User>? Users { get; set; }
    }
}
