namespace HefestusApi.DTOs.Administracao
{
    public record struct UserPersonRequiredDto(
        int Id
        );
    public record struct UserPersonViewDto(
        int Id,
        string Name,
        string UrlImage
        );
    public record struct UserDto(
        string Name,
        string Password,
        UserPersonRequiredDto Person
        );

    public record struct UserViewDto(
       int ID,
       string Name,
       UserPersonViewDto Person
       );
}

