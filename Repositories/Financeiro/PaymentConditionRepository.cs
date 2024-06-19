using HefestusApi.Models.Data;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
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

        public async Task<IEnumerable<PaymentCondition>> GetAllPaymentConditionsAsync(string SystemLocationId)
        {
            return await _context.PaymentCondition
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<PaymentCondition?> GetPaymentConditionByIdAsync(string SystemLocationId, int id)
        {
            return await _context.PaymentCondition
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PaymentCondition>> SearchPaymentConditionByNameAsync(string searchTerm, string SystemLocationId)
        {
            return await _context.PaymentCondition
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") && p.SystemLocationId == SystemLocationId)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
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
