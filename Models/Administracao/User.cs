using HefestusApi.Models.Pessoal;

namespace HefestusApi.Models.Administracao
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public int? PersonId { get; set; }
        public int? SystemLocationId { get; set; }
        public SystemLocation? DefaultLocation { get; set; }
        public Person? Person { get; set; }
    }
}
