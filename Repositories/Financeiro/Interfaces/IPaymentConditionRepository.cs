using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Financeiro.Interfaces
{
    public interface IPaymentConditionRepository
    {
        Task<IEnumerable<PaymentCondition>> GetAllPaymentConditionsAsync();
        Task<PaymentCondition?> GetPaymentConditionByIdAsync(int id);
        Task<IEnumerable<PaymentCondition>> SearchPaymentConditionByNameAsync(string searchTerm);
        Task<bool> AddPaymentConditionAsync(PaymentCondition paymentCondition);
        Task<bool> UpdatePaymentConditionAsync(PaymentCondition paymentCondition);
        Task<bool> DeletePaymentConditionAsync(PaymentCondition paymentCondition);
    }
}
