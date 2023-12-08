using System.Text.Json.Serialization;

namespace HefestusApi.Models.Administracao
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IBGENumber { get; set; }
        public string State { get; set; }
        [JsonIgnore]
        public List<Person> Persons { get; set; }
    }
}
