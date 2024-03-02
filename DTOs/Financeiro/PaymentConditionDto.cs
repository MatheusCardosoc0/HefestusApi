namespace HefestusApi.DTOs.Financeiro
{
    public record struct PaymentConditionDto(
        int Id,
        string Name,
        int Installments,
        int Interval
        );

    public record struct PaymentConditionPostOrPutDto(
        string Name,
        int Installments,
        int Interval
        );

    public record struct PaymentConditionSearchTermDto(
        int Id,
        string Name
        );
}
