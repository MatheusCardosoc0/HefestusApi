using System.Text.Json.Serialization;

namespace HefestusApi.Models.Pessoal
{
    public class PersonGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Person> Persons { get; set; }
        public string CreatedAt { get; private set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string LastModifiedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
