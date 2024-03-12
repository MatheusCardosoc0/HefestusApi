using HefestusApi.Models.Financeiro;

namespace HefestusApi.Repositories.Financeiro.Interfaces
{
    public interface IPaymentOptionsRepository
    {
        Task<IEnumerable<PaymentOptions>> GetAllPaymentOptionsAsync();
        Task<PaymentOptions?> GetPaymentOptionByIdAsync(int id);
        Task<IEnumerable<PaymentOptions>> SearchPaymentOptionByNameAsync(string searchTerm);
        Task<bool> AddPaymentOptionAsync(PaymentOptions paymentOption);
        Task<bool> UpdatePaymentOptionAsync(PaymentOptions paymentOption);
        Task<bool> DeletePaymentOptionAsync(PaymentOptions paymentOption);
    }
}
