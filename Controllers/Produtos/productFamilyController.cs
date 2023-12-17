using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Produtos
{
    [Route("api/[controller]")]
    [ApiController]
    public class productFamilyController : ControllerBase
    {
        private readonly DataContext _context;

        public productFamilyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ProductFamily>> GetProductFamily()
        {
            var productFamily = await _context.ProductFamily.ToListAsync();

            return Ok(productFamily);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductFamily>> ChangeProductFamily(int id, ProductFamilyDto request)
        {
            var productFamily = await _context.ProductFamily.FindAsync(id);

            if (productFamily == null)
            {
                return BadRequest($"Familia de produtos com o id {id} não existe");
            }

            productFamily.Name = request.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest($"Familia de produtos com o id {id} não pode ser alterado");
            }

            return NoContent();
        }
    }
}
