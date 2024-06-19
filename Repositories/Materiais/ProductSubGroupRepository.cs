using HefestusApi.Models.Data;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Materiais.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Materiais
{
    public class ProductSubGroupRepository: IProductSubGroupRepository
    {
        private readonly DataContext _context;

        public ProductSubGroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductSubGroup>> GetAllProductSubGroupsAsync(string SystemLocationId)
        {
            return await _context.ProductSubGroup
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<ProductSubGroup?> GetProductSubGroupByIdAsync(string SystemLocationId, int id)
        {
            return await _context.ProductSubGroup
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductSubGroup>> SearchProductSubGroupByNameAsync(string searchTerm, string SystemLocationId)
        {
            return await _context.ProductSubGroup
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") && p.SystemLocationId == SystemLocationId)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<bool> AddProductSubGroupAsync(ProductSubGroup productSubGroup)
        {
            _context.ProductSubGroup.Add(productSubGroup);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductSubGroupAsync(ProductSubGroup productSubGroup)
        {
            _context.Entry(productSubGroup).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductSubGroupAsync(ProductSubGroup productSubGroup)
        {
            _context.ProductSubGroup.Remove(productSubGroup);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
