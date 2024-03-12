using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais.Interfaces
{
    public interface IProductGroupService
    {
        Task<ServiceResponse<IEnumerable<ProductGroupDto>>> GetAllProductGroupsAsync();
        Task<ServiceResponse<ProductGroup>> GetProductGroupByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchProductGroupByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<ProductGroup>> CreateProductGroupAsync(ProductGroupRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateProductGroupAsync(int id, ProductGroupRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteProductGroupAsync(int id);
    }
}
