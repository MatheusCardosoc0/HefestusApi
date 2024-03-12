using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<Product>> GetProductByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<Product>> CreateProductAsync(ProductRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateProductAsync(int id, ProductRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    }
}
