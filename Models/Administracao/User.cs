using HefestusApi.Models.Pessoal;

namespace HefestusApi.Models.Administracao
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string Password { get; set; }
        public int PersonId { get; set; }
        public string SystemLocationId { get; set; }
        public SystemLocation SystemLocation { get; set; }
        public Person Person { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;
    }
}
