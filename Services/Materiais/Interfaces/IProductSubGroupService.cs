using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductSubGroupService
    {
        Task<ServiceResponse<IEnumerable<ProductSubGroupDto>>> GetAllProductSubGroupsAsync(string SystemLocationId);
        Task<ServiceResponse<ProductSubGroup>> GetProductSubGroupByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductSubGroupByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<ProductSubGroup>> CreateProductSubGroupAsync(ProductSubGroupRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdateProductSubGroupAsync(int id, ProductSubGroupRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteProductSubGroupAsync(string SystemLocationId, int id);
    }
}
