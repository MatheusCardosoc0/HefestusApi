namespace HefestusApi.DTOs.Pessoal
{
    public record struct PersonGroupDto(
        int Id,
        string Name
        );

    public record struct PersonGroupRequestDataDto(
       string Name
       );

    public record struct PersonGroupSimpleSearchDataDto(
        int Id,
        string Name
        );
}
