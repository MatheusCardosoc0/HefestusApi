using HefestusApi.DTOs.Produtos;

namespace HefestusApi.DTOs.Vendas
{
    public record struct OrderDto(
        int Id,
        int ClientId,
        string ClientName,
        int ResponsibleId,
        string ResponsibleName,
        List<OrderProductDto> OrderProducts,
        int PaymentConditionId,
        string PaymentConditionName,
        int PaymentOptionId,
        string PaymentOptionName,
        int? InvoiceId,
        decimal TotalValue,
        decimal LiquidValue,
        decimal BruteValue,
        float Discount,
        string TypeOrder,
        float? CostOfFreight,
        string? TypeFreight,
        string DateOfCompletion
        );

    public record struct OrderPostOrPutDto(
        int Id,
        int ClientId,
        int ResponsibleId,
        List<OrderProductPostOrPutDto> OrderProducts,
        int PaymentConditionId,
        int PaymentOptionId,
        int? InvoiceId,
        decimal TotalValue,
        decimal LiquidValue,
        decimal BruteValue,
        float Discount,
        string TypeOrder,
        float? CostOfFreight,
        string? TypeFreight
        );

    public record struct OrderProductDto(
         int OrderId,
         int ProductId,
         string ProductName,
         decimal UnitPrice,
         float Amount,
         string? Batch,
         decimal TotalPrice
        );

    public record struct OrderProductPostOrPutDto(
         int OrderId,
         int ProductId,
         decimal UnitPrice,
         float Amount,
         decimal TotalPrice
        );
}
