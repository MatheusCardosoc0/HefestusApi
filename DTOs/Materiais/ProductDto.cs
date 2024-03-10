namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductDto(
        int Id,
        string Name,
        string? Description,
        float MinPriceSale,
        decimal AverageCost,
        float PromotionalPrice,
        float PriceSale,
        float BruteCost,
        float LiquidCost,
        float WholesalePrice,
        float MinWholesalePrice,
        string? UrlImage,
        int NCM,
        string GTIN,
        string? GTINtrib,
        int FamilyId,
        int GroupId,
        int SubGroupId,
        string? Reference,
        string? Batch,
        string? GroupName,
        string? SubGroupName,
        string? FamilyName,
        string UnitOfMensuration
    );

    public record struct ProductRequestDataDto(
        int Id,
        string Name,
        string? Description,
        float MinPriceSale,
        decimal AverageCost,
        float PromotionalPrice,
        float PriceSale,
        float BruteCost,
        float LiquidCost,
        float WholesalePrice,
        float MinWholesalePrice,
        string? UrlImage,
        int NCM,
        string GTIN,
        string? GTINtrib,
        ProductFamilySimpleSearchDataDto Family,
        ProductGroupSimpleSearchDataDto Group,
        ProductSubGroupSimpleSearchDataDto SubGroup,
        string? Reference,
        string? Batch,
        string UnitOfMensuration
    );

    public record struct ProductSimpleSearchDataDto (
        int Id,
        string Name,
        float PriceSale,
        float WholesalePrice
    );
}

