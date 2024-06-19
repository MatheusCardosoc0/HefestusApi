namespace HefestusApi.DTOs.Administracao
{
    public record struct UserPersonViewDto(
        string Id,
        string Name,
        string UrlImage,
        string SystemLocationId
        );
    public record struct UserRequestDataDto(
        string Name,
        string Password,
        int PersonId,
        string SystemLocationId
        );

    public record struct UserSimpleSearchDataDto(
        string Id,
        string Name
        );

    public record struct UserDto(
       string ID,
       string Name,
       UserPersonViewDto Person
       );
}

