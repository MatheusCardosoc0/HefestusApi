using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Materiais.Interfaces
{
    public interface IProductGroupRepository
    {
        Task<IEnumerable<ProductGroup>> GetAllProductGroupsAsync(string SystemLocationId);
        Task<ProductGroup?> GetProductGroupByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<ProductGroup>> SearchProductGroupByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddProductGroupAsync(ProductGroup productGroup);
        Task<bool> UpdateProductGroupAsync(ProductGroup productGroup);
        Task<bool> DeleteProductGroupAsync(ProductGroup productGroup);
    }
}
