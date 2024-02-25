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
    public class productGroupController : ControllerBase
    {
        private readonly DataContext _context;

        public productGroupController(DataContext context)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductGroup>> DeleteProductGroup(int id)
        {
            var verifyProdutFamily = await _context.ProductGroups.FindAsync(id);

            if (verifyProdutFamily == null)
            {
                return BadRequest($"Familia de produtos com o id {id} não encontrado");
            }

            try
            {
                _context.ProductGroups.Remove(verifyProdutFamily);
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest("Não foi possivel deletar familia de produtos");
            }

            return NoContent();
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<ProductGroup>>> GetPersonGroupBySearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var productSubGroup = await _context.ProductGroups
                .Where(pg => pg.Name.ToLower().Contains(lowerCaseSearchTerm))
                .ToListAsync();

            return Ok(productSubGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductGroup>> GetProductGroupById(int id)
        {
            var productSubGroup = await _context.ProductGroups
                .FirstOrDefaultAsync(c => c.Id == id);

            if (productSubGroup == null)
            {
                return NotFound($"Familia de produtos com o ID {id} não existe");
            }

            return Ok(productSubGroup);
        }
    }
}
