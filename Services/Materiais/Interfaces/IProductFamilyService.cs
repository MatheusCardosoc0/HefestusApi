using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductFamilyService
    {
        Task<ServiceResponse<IEnumerable<ProductFamilyDto>>> GetAllProductFamiliesAsync();
        Task<ServiceResponse<ProductFamily>> GetProductFamilyByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductFamilyByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<ProductFamily>> CreateProductFamilyAsync(ProductFamilyRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateProductFamilyAsync(int id, ProductFamilyRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteProductFamilyAsync(int id);
    }
}
