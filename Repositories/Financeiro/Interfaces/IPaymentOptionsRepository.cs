using HefestusApi.Models.Financeiro;

namespace HefestusApi.Repositories.Financeiro.Interfaces
{
    public interface IPaymentOptionsRepository
    {
        Task<IEnumerable<PaymentOptions>> GetAllPaymentOptionsAsync(string SystemLocationId);
        Task<PaymentOptions?> GetPaymentOptionByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<PaymentOptions>> SearchPaymentOptionByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddPaymentOptionAsync(PaymentOptions paymentOption);
        Task<bool> UpdatePaymentOptionAsync(PaymentOptions paymentOption);
        Task<bool> DeletePaymentOptionAsync(PaymentOptions paymentOption);
    }
}
