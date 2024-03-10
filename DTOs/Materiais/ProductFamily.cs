namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductFamilyDto(
        int Id,
        string Name
        );

    public record struct ProductFamilySimpleSearchDataDto(
        int Id,
        string Name
        );

    public record struct ProductFamilyRequestDataDto(
       string Name
       );
}

