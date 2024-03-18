namespace HefestusApi.DTOs.Financeiro
{
    public record struct PaymentConditionDto(
        int Id,
        string Name,
        int Installments,
        int Interval
        );

    public record struct PaymentConditionRequestDataDto(
        string Name,
        int Installments,
        int Interval
        );

    public record struct PaymentConditionSimpleSearchDataDto(
        int Id,
        string Name,
        int Installments,
        int Interval
        );
}
