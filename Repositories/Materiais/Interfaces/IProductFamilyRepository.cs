using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Materiais.Interfaces
{
    public interface IProductFamilyRepository
    {
        Task<IEnumerable<ProductFamily>> GetAllProductFamiliesAsync(string SystemLocationId);
        Task<ProductFamily?> GetProductFamilyByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<ProductFamily>> SearchProductFamilyByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddProductFamilyAsync(ProductFamily productFamily);
        Task<bool> UpdateProductFamilyAsync(ProductFamily productFamily);
        Task<bool> DeleteProductFamilyAsync(ProductFamily productFamily);
    }
}
