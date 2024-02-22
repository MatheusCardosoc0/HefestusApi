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

        [HttpPost]
        public async Task<ActionResult<ProductFamily>> CreateProductFamily(ProductFamilyDto request)
        {
            var verifyProductFamily = await _context.ProductFamily.FirstOrDefaultAsync(pf => pf.Name == request.Name);

            if (verifyProductFamily != null)
            {
                BadRequest($"Já existe uma familia de produtos com esse nome");
            }

            var newProductFamily = new ProductFamily
            {
                Name = request.Name,
            };

            try
            {
                _context.ProductFamily.Add(newProductFamily);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Erro ao Criar familia de produto");
            }

            return Ok(newProductFamily);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductFamily>> DeleteProductFamily(int id)
        {
            var verifyProdutFamily = await _context.ProductFamily.FindAsync(id);

            if(verifyProdutFamily == null)
            {
                return BadRequest($"Familia de produtos com o id {id} não encontrado");
            }

            try
            {
                _context.ProductFamily.Remove(verifyProdutFamily);
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest("Não foi possivel deletar familia de produtos");
            }

            return NoContent();
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<ProductFamily>>> GetPersonGroupBySearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var productFamily = await _context.ProductFamily
                .Where(pg => pg.Name.ToLower().Contains(lowerCaseSearchTerm))
                .ToListAsync();

            return Ok(productFamily);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductFamily>> GetProductFamilyById(int id)
        {
            var productFamily = await _context.ProductFamily
                .FirstOrDefaultAsync(c => c.Id == id);

            if (productFamily == null)
            {
                return NotFound($"Familia de produtos com o ID {id} não existe");
            }

            return Ok(productFamily);
        }
    }
}
