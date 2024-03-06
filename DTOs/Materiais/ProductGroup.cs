namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductGroupDto(
        int Id,
        string Name
        );

    public record struct ProductGroupSearchTermDto(
        int Id,
        string Name
        );
}

