using System.Text.Json.Serialization;

namespace HefestusApi.Models.Administracao
{
    public class PersonGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Person> Persons { get; set; }
    }
}
