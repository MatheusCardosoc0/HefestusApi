namespace HefestusApi.DTOs.Financeiro
{
    public record struct PaymentConditionDto(
        string Name,
        int Installments,
        int Interval
        );
}
