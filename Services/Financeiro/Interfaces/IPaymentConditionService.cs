using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Financeiro.Interfaces
{
    public interface IPaymentConditionService
    {
        Task<ServiceResponse<IEnumerable<PaymentConditionDto>>> GetAllPaymentConditionsAsync();
        Task<ServiceResponse<PaymentCondition>> GetPaymentConditionByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPaymentConditionByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<PaymentCondition>> CreatePaymentConditionAsync(PaymentConditionRequestDataDto request);
        Task<ServiceResponse<bool>> UpdatePaymentConditionAsync(int id, PaymentConditionRequestDataDto request);
        Task<ServiceResponse<bool>> DeletePaymentConditionAsync(int id);
    }
}
