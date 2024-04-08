using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Vendas;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Vendas;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Vendas.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync();
        Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchOrderByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<Order>> CreateOrderAsync(OrderRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateOrderAsync(int id, OrderRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteOrderAsync(int id);
    }
}
