using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Financeiro.Interfaces
{
    public interface IPaymentConditionRepository
    {
        Task<IEnumerable<PaymentCondition>> GetAllPaymentConditionsAsync(string SystemLocationId);
        Task<PaymentCondition?> GetPaymentConditionByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<PaymentCondition>> SearchPaymentConditionByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddPaymentConditionAsync(PaymentCondition paymentCondition);
        Task<bool> UpdatePaymentConditionAsync(PaymentCondition paymentCondition);
        Task<bool> DeletePaymentConditionAsync(PaymentCondition paymentCondition);
    }
}
