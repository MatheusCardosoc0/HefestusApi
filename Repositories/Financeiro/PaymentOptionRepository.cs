using HefestusApi.Models.Data;
using HefestusApi.Models.Financeiro;
using HefestusApi.Repositories.Financeiro.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Financeiro
{
    public class PaymentOptionsRepository : IPaymentOptionsRepository
    {
        private readonly DataContext _context;

        public PaymentOptionsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentOptions>> GetAllPaymentOptionsAsync()
        {
            return await _context.PaymentOptions
                .ToListAsync();
        }

        public async Task<PaymentOptions?> GetPaymentOptionByIdAsync(int id)
        {
            return await _context.PaymentOptions
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PaymentOptions>> SearchPaymentOptionByNameAsync(string searchTerm)
        {
            return await _context.PaymentOptions
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<bool> AddPaymentOptionAsync(PaymentOptions paymentOption)
        {
            _context.PaymentOptions.Add(paymentOption);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePaymentOptionAsync(PaymentOptions paymentOption)
        {
            _context.Entry(paymentOption).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePaymentOptionAsync(PaymentOptions paymentOption)
        {
            _context.PaymentOptions.Remove(paymentOption);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
