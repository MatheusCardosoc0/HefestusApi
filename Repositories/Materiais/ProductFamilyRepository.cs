using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Data;
using HefestusApi.Repositories.Materiais.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Materiais
{
    public class ProductFamilyRepository : IProductFamilyRepository
    {
        private readonly DataContext _context;

        public ProductFamilyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductFamily>> GetAllProductFamiliesAsync()
        {
            return await _context.ProductFamily
                .ToListAsync();
        }

        public async Task<ProductFamily?> GetProductFamilyByIdAsync(int id)
        {
            return await _context.ProductFamily
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductFamily>> SearchProductFamilyByNameAsync(string searchTerm)
        {
            return await _context.ProductFamily
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<bool> AddProductFamilyAsync(ProductFamily productFamily)
        {
            _context.ProductFamily.Add(productFamily);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductFamilyAsync(ProductFamily productFamily)
        {
            _context.Entry(productFamily).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductFamilyAsync(ProductFamily productFamily)
        {
            _context.ProductFamily.Remove(productFamily);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
