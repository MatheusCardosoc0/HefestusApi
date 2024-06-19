using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Vendas;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Vendas;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Vendas.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync(string SystemLocationId);
        Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchOrderByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<Order>> CreateOrderAsync(OrderRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdateOrderAsync(int id, OrderRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteOrderAsync(string SystemLocationId, int id);
    }
}
