using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductFamilyService
    {
        Task<ServiceResponse<IEnumerable<ProductFamilyDto>>> GetAllProductFamiliesAsync(string SystemLocationId);
        Task<ServiceResponse<ProductFamily>> GetProductFamilyByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductFamilyByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<ProductFamily>> CreateProductFamilyAsync(ProductFamilyRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdateProductFamilyAsync( int id, ProductFamilyRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteProductFamilyAsync(string SystemLocationId, int id);
    }
}
