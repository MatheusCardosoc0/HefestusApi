namespace HefestusApi.DTOs.Financeiro
{
    public record struct PaymentOptionsDto(
        string Name,
        bool isUseCreditLimit 
        );
}
