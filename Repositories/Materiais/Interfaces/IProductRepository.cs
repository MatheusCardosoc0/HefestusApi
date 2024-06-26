﻿
using HefestusApi.Models.Produtos;

namespace HefestusApi.Repositories.Materiais.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string SystemLocationId);
        Task<Product?> GetProductByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<Product>> SearchProductByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        Task AddStockAsync(Stock stock);
        Task<bool> UpdateStockAsync(Stock stock);
        Task<bool> SaveChangesAsync();
        Task<Stock> GetSelectedStockAsync(int SubLocationId, int ProductId, string SystemLocationid);
    }
}
