namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductGroupDto(
        int Id,
        string Name
        );

    public record struct ProductGroupSimpleSearchDataDto(
        int Id,
        string Name
        );

    public record struct ProductGroupRequestDataDto(
       string Name
       );
}

