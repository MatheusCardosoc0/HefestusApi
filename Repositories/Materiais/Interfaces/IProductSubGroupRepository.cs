using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Materiais.Interfaces
{
    public interface IProductSubGroupRepository
    {
        Task<IEnumerable<ProductSubGroup>> GetAllProductSubGroupsAsync();
        Task<ProductSubGroup?> GetProductSubGroupByIdAsync(int id);
        Task<IEnumerable<ProductSubGroup>> SearchProductSubGroupByNameAsync(string searchTerm);
        Task<bool> AddProductSubGroupAsync(ProductSubGroup productSubGroup);
        Task<bool> UpdateProductSubGroupAsync(ProductSubGroup productSubGroup);
        Task<bool> DeleteProductSubGroupAsync(ProductSubGroup productSubGroup);
    }
}
