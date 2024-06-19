using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Administracao
{
    public class SystemLocation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Password { get; set; } 
 
        [JsonIgnore]
        public List<User>? Users { get; set; }

        [JsonIgnore]
        public List<SubLocation>? SubLocation { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;
    }
}
