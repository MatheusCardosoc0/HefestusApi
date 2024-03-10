namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductSubGroupDto(
        int Id,
        string Name
        );

    public record struct ProductSubGroupSimpleSearchDataDto(
        int Id,
        string Name
        );

    public record struct ProductSubGroupRequestDataDto(
       string Name
       );
}
