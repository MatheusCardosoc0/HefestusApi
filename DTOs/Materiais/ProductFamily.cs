namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductFamilyDto(
        int Id,
        string Name
        );

    public record struct ProductFamilySearchTermDto(
        int Id,
        string Name
        );
}

