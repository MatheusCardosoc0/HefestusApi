﻿using HefestusApi.Models.Data;
using HefestusApi.Models.Produtos;
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

        public async Task<IEnumerable<ProductFamily>> GetAllProductFamiliesAsync(string SystemLocationId)
        {
            return await _context.ProductFamily
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<ProductFamily?> GetProductFamilyByIdAsync(string SystemLocationId, int id)
        {
            return await _context.ProductFamily
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductFamily>> SearchProductFamilyByNameAsync(string searchTerm, string SystemLocationId)
        {
            return await _context.ProductFamily
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") && p.SystemLocationId == SystemLocationId)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
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
