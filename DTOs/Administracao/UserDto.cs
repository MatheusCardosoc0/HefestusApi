namespace HefestusApi.DTOs.Administracao
{
    public record struct PersonRequiredData(
        int Id,
        string UrlImage
        );
    public record struct UserDto(
        string Name,
        string Password,
        PersonRequiredData Person
        );
}

