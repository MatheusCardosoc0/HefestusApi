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

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Product
                .Include(p => p.Subgroup)
                .Include(p => p.Group)
                .Include(p => p.Family)
                .Include(p => p.Stocks)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Product
                .Include(p => p.Subgroup)
                .Include(p => p.Group)
                .Include(p => p.Family)
                .Include(p => p.Stocks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchProductByNameAsync(string searchTerm)
        {
            return await _context.Product
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .Include(p => p.Subgroup)
                .Include(p => p.Group)
                .Include(p => p.Family)
                .Include(p => p.Stocks)
                .ToListAsync();
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

        public async Task<Stock> GetSelectedStockAsync(int SystemLocationId,  int ProductId)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.ProductId == ProductId && s.SystemLocationId == SystemLocationId);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
