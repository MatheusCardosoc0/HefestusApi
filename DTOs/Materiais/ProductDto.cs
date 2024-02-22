namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductDto(
        string Name,
        string Description,
        float PriceSale,
        float PriceTotal,
        int GroupId,
        ProductGroupDto Group,
        int FamilyId,
        ProductFamilyDto Family,
        int SubGroupId,
        ProductSubGroupDto Subgroup,
        float MinPriceSale,
        float MaxPriceSale,
        float BruteCost,
        float LiquidCost
        );
}
