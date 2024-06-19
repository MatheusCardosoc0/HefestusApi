using HefestusApi.Models.Pessoal;

namespace HefestusApi.Models.Administracao
{
    public class UserAdmin
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Password { get; set; }
        public required string LastPassword { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        // Métodos para atualizar datas
        public void UpdateLastModified() => LastModifiedAt = DateTime.UtcNow;
    }
}
