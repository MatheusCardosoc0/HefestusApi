
using HefestusApi.DTOs.Produtos;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace HefestusApi.Controllers.Produtos
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class productGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;

        public productGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductGroups()
        {
            var serviceResponse = await _productGroupService.GetAllProductGroupsAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{detailLevel}/{id}/{locationId}")]
        public async Task<IActionResult> GetProductGroupById(int id)
        {
            var serviceResponse = await _productGroupService.GetProductGroupByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductGroup([FromBody] ProductGroupRequestDataDto request)
        {
            var serviceResponse = await _productGroupService.CreateProductGroupAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return CreatedAtAction(nameof(GetProductGroupById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductGroup(int id, [FromBody] ProductGroupRequestDataDto request)
        {
            var serviceResponse = await _productGroupService.UpdateProductGroupAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            var serviceResponse = await _productGroupService.DeleteProductGroupAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchProductGroupByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _productGroupService.SearchProductGroupByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
