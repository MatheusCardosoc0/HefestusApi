namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductDto(
        string Name,
        string Description,
        int PriceSale,
        int PriceTotal,
        int GroupId,
        ProductGroupDto Group,
        int FamilyId,
        ProductFamilyDto Family,
        int SubGroupId,
        ProductSubGroupDto Subgroup
        );
}
