using HefestusApi.DTOs.Produtos;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HefestusApi.Controllers.Produtos
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly IProductService _productService;

        public productController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var serviceResponse = await _productService.GetAllProductsAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{detailLevel}/{locationId}/{id}")]
        public async Task<IActionResult> GetProductById(int id, string detailLevel, int locationId)
        {
            var serviceResponse = await _productService.GetProductByIdAsync(id, detailLevel, locationId);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDataDto request)
        {
            var serviceResponse = await _productService.CreateProductAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            
            return CreatedAtAction(nameof(GetProductById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDataDto request)
        {
            var serviceResponse = await _productService.UpdateProductAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var serviceResponse = await _productService.DeleteProductAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{detailLevel}/{searchTerm}/{locationId}")]
        public async Task<IActionResult> SearchProductByName(string searchTerm, string detailLevel, int locationId)
        {
            var serviceResponse = await _productService.SearchProductByNameAsync(searchTerm, detailLevel, locationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
