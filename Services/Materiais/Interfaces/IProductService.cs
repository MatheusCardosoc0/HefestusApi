using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync(string SystemLocationId);
        Task<ServiceResponse<object>> GetProductByIdAsync(string SystemLocationId, int id, string detailLevel, int SubLocationId);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductByNameAsync(string searchTerm, string detailLevel, string SystemLocationId, int SubLocationId);
        Task<ServiceResponse<Stock>> CreateProductAsync(ProductRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdateProductAsync(int id, ProductRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteProductAsync(string SystemLocationId, int id);
    }
}
