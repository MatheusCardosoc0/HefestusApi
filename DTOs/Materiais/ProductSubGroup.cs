namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductSubGroupDto(
        int Id,
        string Name
        );

    public record struct ProductSubGroupSearchTermDto(
        int Id,
        string Name
        );
}
