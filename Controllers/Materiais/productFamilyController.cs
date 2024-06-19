using HefestusApi.DTOs.Produtos;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Produtos
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class productFamilyController : ControllerBase
    {
        private readonly IProductFamilyService _productFamilyService;

        public productFamilyController(IProductFamilyService productFamilyService)
        {
            _productFamilyService = productFamilyService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<IActionResult> GetAllProductFamilys(string SystemLocationId)
        {
            var serviceResponse = await _productFamilyService.GetAllProductFamiliesAsync(SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<IActionResult> GetProductFamilyById(string SystemLocationId, int id)
        {
            var serviceResponse = await _productFamilyService.GetProductFamilyByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<IActionResult> CreateProductFamily([FromBody] ProductFamilyRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productFamilyService.CreateProductFamilyAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<IActionResult> UpdateProductFamily(int id, [FromBody] ProductFamilyRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productFamilyService.UpdateProductFamilyAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<IActionResult> DeleteProductFamily(string SystemLocationId, int id)
        {
            var serviceResponse = await _productFamilyService.DeleteProductFamilyAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchProductFamilyByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _productFamilyService.SearchProductFamilyByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
