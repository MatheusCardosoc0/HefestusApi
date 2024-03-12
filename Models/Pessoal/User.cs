namespace HefestusApi.Models.Pessoal
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
