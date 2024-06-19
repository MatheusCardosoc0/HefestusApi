using HefestusApi.DTOs.Produtos;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Produtos
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class productSubGroupController : ControllerBase
    {
        private readonly IProductSubGroupService _productSubGroupService;

        public productSubGroupController(IProductSubGroupService productSubGroupService)
        {
            _productSubGroupService = productSubGroupService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<IActionResult> GetAllProductSubGroups(string SystemLocationId)
        {
            var serviceResponse = await _productSubGroupService.GetAllProductSubGroupsAsync( SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<IActionResult> GetProductSubGroupById(string SystemLocationId, int id)
        {
            var serviceResponse = await _productSubGroupService.GetProductSubGroupByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<IActionResult> CreateProductSubGroup([FromBody] ProductSubGroupRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productSubGroupService.CreateProductSubGroupAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<IActionResult> UpdateProductSubGroup(int id, [FromBody] ProductSubGroupRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _productSubGroupService.UpdateProductSubGroupAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<IActionResult> DeleteProductSubGroup(string SystemLocationId, int id)
        {
            var serviceResponse = await _productSubGroupService.DeleteProductSubGroupAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchProductSubGroupByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _productSubGroupService.SearchProductSubGroupByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
