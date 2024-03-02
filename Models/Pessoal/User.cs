using System.Text.Json.Serialization;

namespace HefestusApi.Models.Administracao
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
