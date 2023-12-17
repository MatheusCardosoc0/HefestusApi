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
    public class productSubGroupController : ControllerBase
    {
        private readonly DataContext _context;

        public productSubGroupController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ProductSubGroup>> GetProductSubGroup()
        {
            var productSubGroup = await _context.ProductSubGroup.ToListAsync();

            return Ok(productSubGroup);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductSubGroup>> ChangeProductSubGroup(int id, ProductSubGroupDto request)
        {
            var productSubGroup = await _context.ProductSubGroup.FindAsync(id);

            if (productSubGroup == null)
            {
                return BadRequest($"Sub Grupo de produtos com o id {id} não existe");
            }

            productSubGroup.Name = request.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest($"Sub Grupo de produtos com o id {id} não pode ser alterado");
            }

            return NoContent();
        }
    }
}
