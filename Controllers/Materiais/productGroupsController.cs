using HefestusApi.DTOs.Administracao;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Administracao;
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

        [HttpPost]
        public async Task<ActionResult<PersonGroup>> CreateProductGroup(ProductGroupDto request)
        {
            var existingProductGroup = await _context.ProductGroups.FirstOrDefaultAsync(p => p.Name == request.Name);

            if (existingProductGroup != null)
            {
                return BadRequest($"Já existe um grupo de produtos com o nome {request.Name} cadastrado");
            }


            var newProductGroup = new ProductGroup
            {
                Name = request.Name
            };

            _context.ProductGroups.Add(newProductGroup);
            await _context.SaveChangesAsync();

            return Ok(newProductGroup);
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
