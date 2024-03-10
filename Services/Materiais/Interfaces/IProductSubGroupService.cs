using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Services.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductSubGroupService
    {
        Task<ServiceResponse<IEnumerable<ProductSubGroupDto>>> GetAllProductSubGroupsAsync();
        Task<ServiceResponse<ProductSubGroup>> GetProductSubGroupByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductSubGroupByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<ProductSubGroup>> CreateProductSubGroupAsync(ProductSubGroupRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateProductSubGroupAsync(int id, ProductSubGroupRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteProductSubGroupAsync(int id);
    }
}
