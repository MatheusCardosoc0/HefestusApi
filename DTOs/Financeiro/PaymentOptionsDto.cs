namespace HefestusApi.DTOs.Financeiro
{
    public record struct PaymentOptionDto(
        int Id,
        string Name,
        bool IsUseCreditLimit 
        );

    public record struct PaymentOptionRequestDataDto(
        string Name,
        bool IsUseCreditLimit
        );

    public record struct PaymentOptionSimpleSearchDataDto(
        int Id,
        string Name
        );
}
