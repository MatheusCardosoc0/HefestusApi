namespace HefestusApi.DTOs.Financeiro
{
    public record struct PaymentOptionsDto(
        int Id,
        string Name,
        bool IsUseCreditLimit 
        );

    public record struct PaymentOptionsPostOrPutDto(
        string Name,
        bool IsUseCreditLimit
        );

    public record struct PaymentOptionsSearchTermDto(
        int Id,
        string Name
        );
}
