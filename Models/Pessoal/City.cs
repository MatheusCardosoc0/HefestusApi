using HefestusApi.Models.Interfaces;
using System.Text.Json.Serialization;

namespace HefestusApi.Models.Pessoal
{
    public class City : TimeTrail 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IBGENumber { get; set; }
        public string State { get; set; }
        public string SystemLocationId { get; set; }
        [JsonIgnore]
        public List<Person> Persons { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;
    }
}
