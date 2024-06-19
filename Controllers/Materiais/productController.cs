using HefestusApi.DTOs.Produtos;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HefestusApi.Controllers.Produtos
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly IProductService _productService;

        public productController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<IActionResult> GetAllProducts(string SystemLocationId)
        {
            var serviceResponse = await _productService.GetAllProductsAsync(SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{SubLocationId}/{detailLevel}/{id}")]
        public async Task<IActionResult> GetProductById(string SystemLocationId, int id, string detailLevel, int SubLocationId)
        {
            var serviceResponse = await _productService.GetProductByIdAsync(SystemLocationId, id, detailLevel, SubLocationId);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productService.CreateProductAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productService.UpdateProductAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<IActionResult> DeleteProduct(string SystemLocationId, int id)
        {
            var serviceResponse = await _productService.DeleteProductAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{SystemLocationId}/{SubLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchProductByName(string searchTerm, string detailLevel, string SystemLocationId, int SubLocationId)
        {
            var serviceResponse = await _productService.SearchProductByNameAsync(searchTerm, detailLevel, SystemLocationId, SubLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
