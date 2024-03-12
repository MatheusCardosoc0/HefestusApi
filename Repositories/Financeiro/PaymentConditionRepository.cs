using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Data;
using HefestusApi.Repositories.Financeiro.Interfaces;
using HefestusApi.Repositories.Materiais.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Financeiro
{
    public class PaymentConditionRepository : IPaymentConditionRepository
    {
        private readonly DataContext _context;

        public PaymentConditionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentCondition>> GetAllPaymentConditionsAsync()
        {
            return await _context.PaymentCondition
                .ToListAsync();
        }

        public async Task<PaymentCondition?> GetPaymentConditionByIdAsync(int id)
        {
            return await _context.PaymentCondition
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PaymentCondition>> SearchPaymentConditionByNameAsync(string searchTerm)
        {
            return await _context.PaymentCondition
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<bool> AddPaymentConditionAsync(PaymentCondition paymentCondition)
        {
            _context.PaymentCondition.Add(paymentCondition);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePaymentConditionAsync(PaymentCondition paymentCondition)
        {
            _context.Entry(paymentCondition).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePaymentConditionAsync(PaymentCondition paymentCondition)
        {
            _context.PaymentCondition.Remove(paymentCondition);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
