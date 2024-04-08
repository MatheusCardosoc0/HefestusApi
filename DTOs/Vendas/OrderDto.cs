using HefestusApi.DTOs.Financeiro;
using HefestusApi.DTOs.Pessoal;

namespace HefestusApi.DTOs.Vendas
{
    public record struct OrderDto(
        int Id,
        int ClientId,
        string ClientName,
        int ResponsibleId,
        string ResponsibleName,
        PersonDto Client,
        PersonDto Responsible,
        List<OrderProductDto> OrderProducts,
        List<OrderInstallmentDto> OrderInstallments,
        PaymentConditionDto PaymentCondition,
        PaymentOptionDto PaymentOption,
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
        int TypeFreight,
        string DateOfCompletion
        );

    public record struct OrderRequestDataDto(
        PersonSimpleSearchDataDto Client,
        PersonSimpleSearchDataDto Responsible,
        List<OrderProductRequestdataDto> OrderProducts,
        List<OrderInstallmentRequestDataDto> OrderInstallments,
        PaymentConditionSimpleSearchDataDto PaymentCondition,
        PaymentOptionSimpleSearchDataDto PaymentOption,
        int? InvoiceId,
        decimal TotalValue,
        decimal LiquidValue,
        decimal BruteValue,
        float Discount,
        string TypeOrder,
        float? CostOfFreight,
        int TypeFreight
        );

    public record struct OrderInstallmentDto(
        int OrderId,
        int InstallmentNumber, 
        int PaymentOptionId, 
        string PaymentOptionName, 
        string Maturity, 
        decimal Value,
        string ClientName,
        string DateOfCompletion
        );

    public record struct OrderInstallmentRequestDataDto(
       int OrderId,
       int InstallmentNumber,
       int PaymentOptionId,
       string Maturity,
       decimal Value
       );

    public record struct OrderProductDto(
         int Id,
         int OrderId,
         int ProductId,
         string ProductName,
         decimal UnitPrice,
         float Amount,
         string? Batch,
         decimal TotalPrice
        );

    public record struct OrderProductRequestdataDto(
         int OrderId,
         int ProductId,
         decimal UnitPrice,
         float Amount,
         decimal TotalPrice
        );

    public record struct OrderSimpleSearchDataDto(
        int Id,
        string Name
        );
}