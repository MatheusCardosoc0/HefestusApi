using HefestusApi.DTOs.Produtos;

namespace HefestusApi.DTOs.Vendas
{
    public record struct OrderDto(
        OrderPersonDto Client,
        OrderPersonDto Responsible,
        List<OrderProductDto> OrderProducts,
        OrderPaymentConditionDto PaymentCondition,
        OrderPaymentOptionDto PaymentOption,
        decimal Value
        );

    public record struct OrderViewDto(
       OrderPersonViewDto Client,
       OrderPersonViewDto Responsible,
       List<OrderProductViewDto> OrderProducts,
       OrderPaymentConditionViewDto PaymentCondition,
       OrderPaymentOptionViewDto PaymentOption,
       decimal Value
       );

    public record struct OrderProductDto(
         int OrderId,
         int ProductId,
         decimal Price
        );

    public record struct OrderPersonDto(
        int Id
        );

    public record struct OrderPaymentConditionDto(
        int Id
        );

    public record struct OrderPaymentOptionDto(
       int Id
       );

    public record struct OrderProductViewDto(
         int OrderId,
         int ProductId,
         OrderProductDataViewDto Product,
         decimal Price
        );
    public record struct OrderProductDataViewDto(
        string Name
        );


    public record struct OrderPersonViewDto(
        int Id,
        string Name
        );

    public record struct OrderPaymentConditionViewDto(
        int Id,
        string Name
        );

    public record struct OrderPaymentOptionViewDto(
       int Id,
       string Name
       );
}
