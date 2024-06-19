using HefestusApi.DTOs.Financeiro;
using HefestusApi.Services.Financeiro.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Financeiro
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class paymentOptionController : ControllerBase
    {
        private readonly IPaymentOptionService _paymentOptionsService;

        public paymentOptionController(IPaymentOptionService paymentOptionsService)
        {
            _paymentOptionsService = paymentOptionsService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<IActionResult> GetAllPaymentOptions(string SystemLocationId)
        {
            var serviceResponse = await _paymentOptionsService.GetAllPaymentOptionsAsync(SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<IActionResult> GetPaymentOptionById(string SystemLocationId, int id)
        {
            var serviceResponse = await _paymentOptionsService.GetPaymentOptionByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<IActionResult> CreatePaymentOption([FromBody] PaymentOptionRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _paymentOptionsService.CreatePaymentOptionAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<IActionResult> UpdatePaymentOption(int id, [FromBody] PaymentOptionRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _paymentOptionsService.UpdatePaymentOptionAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<IActionResult> DeletePaymentOption(string SystemLocationId, int id)
        {
            var serviceResponse = await _paymentOptionsService.DeletePaymentOptionAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchPaymentOptionByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _paymentOptionsService.SearchPaymentOptionByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
