using HefestusApi.DTOs.Produtos;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Produtos
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class productFamilyController : ControllerBase
    {
        private readonly IProductFamilyService _productFamilyService;

        public productFamilyController(IProductFamilyService productFamilyService)
        {
            _productFamilyService = productFamilyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductFamilys()
        {
            var serviceResponse = await _productFamilyService.GetAllProductFamiliesAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductFamilyById(int id)
        {
            var serviceResponse = await _productFamilyService.GetProductFamilyByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductFamily([FromBody] ProductFamilyRequestDataDto request)
        {
            var serviceResponse = await _productFamilyService.CreateProductFamilyAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            
            return CreatedAtAction(nameof(GetProductFamilyById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductFamily(int id, [FromBody] ProductFamilyRequestDataDto request)
        {
            var serviceResponse = await _productFamilyService.UpdateProductFamilyAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFamily(int id)
        {
            var serviceResponse = await _productFamilyService.DeleteProductFamilyAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchProductFamilyByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _productFamilyService.SearchProductFamilyByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
