namespace HefestusApi.DTOs.Administracao
{
    public record struct SubLocationRequestDataDto(
        string Name,
        int PersonId
        );

    public record struct SubLocationSimpleSearchDataDto(
        string Id,
        string Name
        );

    public record struct SubLocationDto(
       int Id,
       string PersonName,
       string SystemLocationName
       );
}
