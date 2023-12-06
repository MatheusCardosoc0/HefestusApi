namespace HefestusApi.DTOs.Administracao
{
    public record struct PersonRequiredData(
        int Id
        );
    public record struct UserDto(
        string Name,
        string Password,
        PersonRequiredData Person
        );
}

