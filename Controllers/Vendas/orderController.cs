using HefestusApi.DTOs.Vendas;
using HefestusApi.Models.Administracao;
using HefestusApi.Services.Vendas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Vendas
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public orderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<ActionResult> GetAllOrders(string SystemLocationid)
        {
            var serviceResponse = await _orderService.GetAllOrdersAsync(SystemLocationid);
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<ActionResult> GetOrderById(string SystemLocationId, int id)
        {
            var serviceResponse = await _orderService.GetOrderByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchOrderByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _orderService.SearchOrderByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<ActionResult> CreateOrder([FromBody] OrderRequestDataDto request, string SystemLocationid)
        {
            var serviceResponse = await _orderService.CreateOrderAsync(request, SystemLocationid);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] OrderRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _orderService.UpdateOrderAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<ActionResult> DeleteOrder(string SystemLocationId, int id)
        {
            var serviceResponse = await _orderService.DeleteOrderAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
