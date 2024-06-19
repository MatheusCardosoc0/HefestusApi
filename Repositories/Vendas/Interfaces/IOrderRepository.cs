using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;

namespace HefestusApi.Repositories.Vendas.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(string SystemLocationId);
        Task<Order?> GetOrderByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<Order>> SearchOrderByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Order order);
        Task AddOrderInstallMentsAsync(OrderInstallment orderInstallment);
        Task AddOrderProductAsync(OrderProduct orderProduct);
        Task RemoveOrderProductAsync(IEnumerable<OrderProduct> orderProducts);
        Task RemoveOrderInstallmentsAsync(IEnumerable<OrderInstallment> orderInstallments);
        Task<bool> SaveCahngesAsymc();
    }
}
