using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<object>> GetProductByIdAsync(int id, string detailLevel, int locationId);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductByNameAsync(string searchTerm, string detailLevel, int locationId);
        Task<ServiceResponse<Stock>> CreateProductAsync(ProductRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateProductAsync(int id, ProductRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    }
}
