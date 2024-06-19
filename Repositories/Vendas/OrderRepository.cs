using HefestusApi.Models.Data;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;
using HefestusApi.Repositories.Vendas.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Vendas
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrderAsync(Order order)
        {
            await _context.Order.AddAsync(order);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteOrderAsync(Order order)
        {
            _context.Order.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(string SystemLocationId)
        {
            return await _context.Order
                .Include(o => o.Client)
                .Include(o => o.Responsible)
                .Include(o => o.OrderProducts)
                  .ThenInclude(op => op.Product)
                .Include(o => o.PaymentOption)
                .Include(o => o.PaymentCondition)
                .Include(o => o.OrderInstallments)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(string SystemLocationId, int id)
        {
            return await _context.Order
                .Include(o => o.Client)
                .Include(o => o.Responsible)
                .Include(o => o.OrderProducts)
                  .ThenInclude(op => op.Product)
                .Include(o => o.PaymentOption)
                .Include(o => o.PaymentCondition)
                .Include(o => o.OrderInstallments)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> SearchOrderByNameAsync(string searchTerm, string SystemLocationId)
        {
            return await _context.Order
                .Where(p => EF.Functions.Like(p.Client.Name.ToLower(), $"%{searchTerm}%") )
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task AddOrderProductAsync(OrderProduct orderProduct)
        {
            await _context.OrderProduct.AddAsync(orderProduct);
        }

        public async Task AddOrderInstallMentsAsync(OrderInstallment orderInstallment)
        {
            await _context.OrderInstallment.AddAsync(orderInstallment);
        }

        public async Task RemoveOrderInstallmentsAsync(IEnumerable<OrderInstallment> orderInstallment)
        {
            _context.OrderInstallment.RemoveRange(orderInstallment);
        }

        public async Task RemoveOrderProductAsync(IEnumerable<OrderProduct> orderProduct)
        {
            _context.OrderProduct.RemoveRange(orderProduct);
        }

        public async Task<bool> SaveCahngesAsymc()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
