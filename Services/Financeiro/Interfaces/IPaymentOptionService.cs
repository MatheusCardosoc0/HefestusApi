using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Financeiro.Interfaces
{
    public interface IPaymentOptionService
    {
        Task<ServiceResponse<IEnumerable<PaymentOptionDto>>> GetAllPaymentOptionsAsync(string SystemLocationId);
        Task<ServiceResponse<PaymentOptions>> GetPaymentOptionByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPaymentOptionByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<PaymentOptions>> CreatePaymentOptionAsync(PaymentOptionRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdatePaymentOptionAsync( int id, PaymentOptionRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeletePaymentOptionAsync(string SystemLocationId, int id);
    }
}
