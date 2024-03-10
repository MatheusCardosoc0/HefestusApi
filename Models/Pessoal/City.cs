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
        [JsonIgnore]
        public List<Person> Persons { get; set; }
    }
}
