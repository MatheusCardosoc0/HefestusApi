using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductGroupService
    {
        Task<ServiceResponse<IEnumerable<ProductGroupDto>>> GetAllProductGroupsAsync(string SystemLocationId);
        Task<ServiceResponse<ProductGroup>> GetProductGroupByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductGroupByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<ProductGroup>> CreateProductGroupAsync(ProductGroupRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdateProductGroupAsync(int id, ProductGroupRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteProductGroupAsync(string SystemLocationId, int id);
    }
}
