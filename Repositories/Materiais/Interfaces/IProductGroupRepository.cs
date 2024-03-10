using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Materiais.Interfaces
{
    public interface IProductGroupRepository
    {
        Task<IEnumerable<ProductGroup>> GetAllProductGroupsAsync();
        Task<ProductGroup?> GetProductGroupByIdAsync(int id);
        Task<IEnumerable<ProductGroup>> SearchProductGroupByNameAsync(string searchTerm);
        Task<bool> AddProductGroupAsync(ProductGroup productGroup);
        Task<bool> UpdateProductGroupAsync(ProductGroup productGroup);
        Task<bool> DeleteProductGroupAsync(ProductGroup productGroup);
    }
}
