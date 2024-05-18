namespace HefestusApi.DTOs.Pessoal
{
    public record struct PersonGroupDto(
        int Id,
        string Name,
        DateTime CreatedAt,
        DateTime LastModifiedAt
        );

    public record struct PersonGroupRequestDataDto(
       string Name,
       DateTime CreatedAt,
       DateTime LastModifiedAt
       );

    public record struct PersonGroupSimpleSearchDataDto(
        int Id,
        string Name,
        DateTime CreatedAt,
        DateTime LastModifiedAt
        );
}
