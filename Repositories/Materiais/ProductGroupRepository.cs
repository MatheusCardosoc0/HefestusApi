using HefestusApi.Models.Data;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Materiais.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Materiais
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly DataContext _context;

        public ProductGroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductGroup>> GetAllProductGroupsAsync()
        {
            return await _context.ProductGroups
                .ToListAsync();
        }

        public async Task<ProductGroup?> GetProductGroupByIdAsync(int id)
        {
            return await _context.ProductGroups
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductGroup>> SearchProductGroupByNameAsync(string searchTerm)
        {
            return await _context.ProductGroups
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<bool> AddProductGroupAsync(ProductGroup productGroup)
        {
            _context.ProductGroups.Add(productGroup);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductGroupAsync(ProductGroup productGroup)
        {
            _context.Entry(productGroup).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductGroupAsync(ProductGroup productGroup)
        {
            _context.ProductGroups.Remove(productGroup);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
