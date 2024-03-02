namespace HefestusApi.DTOs.Administracao
{
    public record struct PersonGroupDto(
        int Id,
        string Name
        );

    public record struct PersonGroupPostOrPutDto(
       string Name
       );

    public record struct PersonGroupSearchTermDto(
        int Id,
        string Name
        );
}
