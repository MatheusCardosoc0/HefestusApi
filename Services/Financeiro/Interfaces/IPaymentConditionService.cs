using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Financeiro.Interfaces
{
    public interface IPaymentConditionService
    {
        Task<ServiceResponse<IEnumerable<PaymentConditionDto>>> GetAllPaymentConditionsAsync(string SystemLocationId);
        Task<ServiceResponse<PaymentCondition>> GetPaymentConditionByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPaymentConditionByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<PaymentCondition>> CreatePaymentConditionAsync(PaymentConditionRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdatePaymentConditionAsync(int id, PaymentConditionRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeletePaymentConditionAsync(string SystemLocationId, int id);
    }
}
