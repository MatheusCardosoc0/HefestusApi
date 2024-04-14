using HefestusApi.DTOs.Vendas;
using HefestusApi.Services.Vendas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Vendas
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public orderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            var serviceResponse = await _orderService.GetAllOrdersAsync();
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{detailLevel}/{id}/{locationId}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            var serviceResponse = await _orderService.GetOrderByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchOrderByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _orderService.SearchOrderByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderRequestDataDto request)
        {
            var serviceResponse = await _orderService.CreateOrderAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return CreatedAtAction(nameof(GetOrderById), new { id = serviceResponse?.Data?.Id }, serviceResponse?.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] OrderRequestDataDto request)
        {
            var serviceResponse = await _orderService.UpdateOrderAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var serviceResponse = await _orderService.DeleteOrderAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
