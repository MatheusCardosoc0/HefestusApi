namespace HefestusApi.DTOs.Administracao
{
    public record struct UserPersonViewDto(
        int Id,
        string Name,
        string UrlImage
        );
    public record struct UserDto(
        string Name,
        string Password,
        int PersonId
        );

    public record struct UserViewDto(
       int ID,
       string Name,
       UserPersonViewDto Person
       );
}

