using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Data;
using HefestusApi.Services.Materiais.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Produtos
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class productSubGroupController : ControllerBase
    {
        private readonly IProductSubGroupService _productSubGroupService;

        public productSubGroupController(IProductSubGroupService productSubGroupService)
        {
            _productSubGroupService = productSubGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductSubGroups()
        {
            var serviceResponse = await _productSubGroupService.GetAllProductSubGroupsAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductSubGroupById(int id)
        {
            var serviceResponse = await _productSubGroupService.GetProductSubGroupByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductSubGroup([FromBody] ProductSubGroupRequestDataDto request)
        {
            var serviceResponse = await _productSubGroupService.CreateProductSubGroupAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return CreatedAtAction(nameof(GetProductSubGroupById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductSubGroup(int id, [FromBody] ProductSubGroupRequestDataDto request)
        {
            var serviceResponse = await _productSubGroupService.UpdateProductSubGroupAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSubGroup(int id)
        {
            var serviceResponse = await _productSubGroupService.DeleteProductSubGroupAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchProductSubGroupByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _productSubGroupService.SearchProductSubGroupByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
