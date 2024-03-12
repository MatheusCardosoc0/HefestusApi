using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Financeiro.Interfaces
{
    public interface IPaymentOptionService
    {
        Task<ServiceResponse<IEnumerable<PaymentOptionDto>>> GetAllPaymentOptionsAsync();
        Task<ServiceResponse<PaymentOptions>> GetPaymentOptionByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPaymentOptionByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<PaymentOptions>> CreatePaymentOptionAsync(PaymentOptionRequestDataDto request);
        Task<ServiceResponse<bool>> UpdatePaymentOptionAsync(int id, PaymentOptionRequestDataDto request);
        Task<ServiceResponse<bool>> DeletePaymentOptionAsync(int id);
    }
}
