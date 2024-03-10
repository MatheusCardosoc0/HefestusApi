namespace HefestusApi.DTOs.Pessoal
{
    public record struct UserPersonViewDto(
        int Id,
        string Name,
        string UrlImage
        );
    public record struct UserRequestDataDto(
        string Name,
        string Password,
        int PersonId
        );

    public record struct UserSimpleSearchDataDto(
        int Id,
        string Name
        );

    public record struct UserDto(
       int ID,
       string Name,
       UserPersonViewDto Person
       );
}

