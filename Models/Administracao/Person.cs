using System.Text.Json.Serialization;

namespace HefestusApi.Models.Administracao
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; }
        public int Age { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string IBGE { get; set; }
        public string Razao { get; set; }
        public string InscricaoEstadual { get; set; }
        public string CEP { get; set; }
        public string? UrlImage { get; set; } = string.Empty;
        public bool? IsBlocked { get; set; } = false;
        public string? MaritalStatus { get; set; } = string.Empty;
        public string? Habilities { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public int CityId { get; set; }
        public City City { get; set; }
        public List<PersonGroup> PersonGroups { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}