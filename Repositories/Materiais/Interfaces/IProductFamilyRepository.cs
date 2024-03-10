using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Materiais.Interfaces
{
    public interface IProductFamilyRepository
    {
        Task<IEnumerable<ProductFamily>> GetAllProductFamiliesAsync();
        Task<ProductFamily?> GetProductFamilyByIdAsync(int id);
        Task<IEnumerable<ProductFamily>> SearchProductFamilyByNameAsync(string searchTerm);
        Task<bool> AddProductFamilyAsync(ProductFamily productFamily);
        Task<bool> UpdateProductFamilyAsync(ProductFamily productFamily);
        Task<bool> DeleteProductFamilyAsync(ProductFamily productFamily);
    }
}
