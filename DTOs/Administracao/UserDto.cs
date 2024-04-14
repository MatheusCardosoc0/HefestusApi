namespace HefestusApi.DTOs.Administracao
{
    public record struct UserPersonViewDto(
        int Id,
        string Name,
        string UrlImage,
        int SystemLocationId
        );
    public record struct UserRequestDataDto(
        string Name,
        string Password,
        int PersonId,
        int? SystemLocationId
        );

    public record struct UserSimpleSearchDataDto(
        int Id,
        string Name
        );

    public record struct UserDto(
       int ID,
       string Name,
       UserPersonViewDto Person,
       SystemLocationSimpleSearchDataDto DefaultLocation
       );
}

