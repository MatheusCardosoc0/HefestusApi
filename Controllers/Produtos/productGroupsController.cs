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
    public class productGroupsController : ControllerBase
    {
        private readonly DataContext _context;

        public productGroupsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ProductGroup>> GetProductGroups()
        {
            var productGroups = await _context.ProductGroups.ToListAsync();

            return Ok(productGroups);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductGroup>> ChangeProductGroups(int id, ProductGroupDto request )
        {
            var productGroups = await _context.ProductGroups.FindAsync(id);

            if(productGroups == null)
            {
                return BadRequest($"Grupo de produtos com o id {id} não existe");
            }

            productGroups.Name = request.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest($"Grupo de produtos com o id {id} não pode ser alterado");
            }

            return NoContent();
        }
    }
}
