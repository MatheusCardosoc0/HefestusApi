using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Materiais.Interfaces
{
    public interface IProductSubGroupRepository
    {
        Task<IEnumerable<ProductSubGroup>> GetAllProductSubGroupsAsync(string SystemLocationId);
        Task<ProductSubGroup?> GetProductSubGroupByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<ProductSubGroup>> SearchProductSubGroupByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddProductSubGroupAsync(ProductSubGroup productSubGroup);
        Task<bool> UpdateProductSubGroupAsync(ProductSubGroup productSubGroup);
        Task<bool> DeleteProductSubGroupAsync(ProductSubGroup productSubGroup);
    }
}
