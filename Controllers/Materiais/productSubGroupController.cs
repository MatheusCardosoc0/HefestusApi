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

        [HttpPost]
        public async Task<ActionResult<PersonGroup>> CreateProductSubGroup(ProductSubGroupDto request)
        {
            var existingProductSubGroup = await _context.ProductSubGroup.FirstOrDefaultAsync(p => p.Name == request.Name);

            if (existingProductSubGroup != null)
            {
                return BadRequest($"Já existe um grupo de produtos com o nome {request.Name} cadastrado");
            }


            var newProductSubGroup = new ProductSubGroup
            {
                Name = request.Name
            };

            _context.ProductSubGroup.Add(newProductSubGroup);
            await _context.SaveChangesAsync();

            return Ok(newProductSubGroup);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductSubGroup>> DeleteProductSubGroup(int id)
        {
            var verifyProdutFamily = await _context.ProductSubGroup.FindAsync(id);

            if (verifyProdutFamily == null)
            {
                return BadRequest($"Familia de produtos com o id {id} não encontrado");
            }

            try
            {
                _context.ProductSubGroup.Remove(verifyProdutFamily);
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest("Não foi possivel deletar familia de produtos");
            }

            return NoContent();
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<ProductSubGroup>>> GetPersonGroupBySearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var productSubGroup = await _context.ProductSubGroup
                .Where(pg => pg.Name.ToLower().Contains(lowerCaseSearchTerm))
                .ToListAsync();

            return Ok(productSubGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSubGroup>> GetProductSubGroupById(int id)
        {
            var productSubGroup = await _context.ProductSubGroup
                .FirstOrDefaultAsync(c => c.Id == id);

            if (productSubGroup == null)
            {
                return NotFound($"Familia de produtos com o ID {id} não existe");
            }

            return Ok(productSubGroup);
        }
    }
}
