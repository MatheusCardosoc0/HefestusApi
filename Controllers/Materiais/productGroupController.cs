
using HefestusApi.DTOs.Produtos;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace HefestusApi.Controllers.Produtos
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class productGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;

        public productGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<IActionResult> GetAllProductGroups(string SystemLocationId)
        {
            var serviceResponse = await _productGroupService.GetAllProductGroupsAsync(SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<IActionResult> GetProductGroupById(string SystemLocationId, int id, string detailLevel )
        {
            var serviceResponse = await _productGroupService.GetProductGroupByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<IActionResult> CreateProductGroup([FromBody] ProductGroupRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productGroupService.CreateProductGroupAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<IActionResult> UpdateProductGroup(int id, [FromBody] ProductGroupRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productGroupService.UpdateProductGroupAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<IActionResult> DeleteProductGroup(string SystemLocationId, int id)
        {
            var serviceResponse = await _productGroupService.DeleteProductGroupAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchProductGroupByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _productGroupService.SearchProductGroupByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
