namespace HefestusApi.DTOs.Produtos
{
    public record struct ProductDto(
        int Id,
        string Name,
        string? Description,
        float MinPriceSale,
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
        decimal MinPriceSale,
        decimal AverageCost,
        decimal PromotionalPrice,
        decimal PriceSale,
        decimal BruteCost,
        decimal LiquidCost,
        decimal WholesalePrice,
        decimal MinWholesalePrice,
        string? UrlImage,
        int NCM,
        string GTIN,
        string? GTINtrib,
        ProductFamilySimpleSearchDataDto Family,
        ProductGroupSimpleSearchDataDto Group,
        ProductSubGroupSimpleSearchDataDto SubGroup,
        string? Reference,
        string? Batch,
        string UnitOfMensuration,
        int SystemLocationId,
        float MinStock,
        float MaxStock,
        float CurrentStock,
        string? Location
    );

    public record struct ProductSimpleSearchDataDto(
        int Id,
        string Name,
        decimal PriceSale,
        decimal WholesalePrice
    );
}

