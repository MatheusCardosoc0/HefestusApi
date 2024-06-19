using HefestusApi.Models.Data;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Materiais.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Materiais
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string SystemLocationId)
        {
            return await _context.Product
                .Include(p => p.Subgroup)
                .Include(p => p.Group)
                .Include(p => p.Family)
                .Include(p => p.Stocks)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(string SystemLocationId, int id)
        {
            return await _context.Product
                .Include(p => p.Subgroup)
                .Include(p => p.Group)
                .Include(p => p.Family)
                .Include(p => p.Stocks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchProductByNameAsync(string searchTerm, string SystemLocationId)
        {
            return await _context.Product
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") && p.SystemLocationId == SystemLocationId)
                .Include(p => p.Subgroup)
                .Include(p => p.Group)
                .Include(p => p.Family)
                .Include(p => p.Stocks)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            _context.Product.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Product.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStockAsync(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
        }

        public async Task<Stock> GetSelectedStockAsync(int SubLocationId,  int ProductId, string SystemLocationId)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.ProductId == ProductId && s.SubLocationId == SubLocationId && s.Product.SystemLocationId == SystemLocationId);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
