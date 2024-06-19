using HefestusApi.DTOs.Financeiro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HefestusApi.Services.Financeiro.Interfaces;

namespace HefestusApi.Controllers.Financeiro
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class paymentConditionController : ControllerBase
    {
        private readonly IPaymentConditionService _paymentConditionService;

        public paymentConditionController(IPaymentConditionService paymentConditionService)
        {
            _paymentConditionService = paymentConditionService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<IActionResult> GetAllPaymentCondition(string SystemLocationId)
        {
            var serviceResponse = await _paymentConditionService.GetAllPaymentConditionsAsync(SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<IActionResult> GetPaymentConditionById(string SystemLocationId, int id)
        {
            var serviceResponse = await _paymentConditionService.GetPaymentConditionByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<IActionResult> CreatePaymentCondition([FromBody] PaymentConditionRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _paymentConditionService.CreatePaymentConditionAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<IActionResult> UpdatePaymentCondition(int id, [FromBody] PaymentConditionRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _paymentConditionService.UpdatePaymentConditionAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<IActionResult> DeletePaymentCondition(string SystemLocationId, int id)
        {
            var serviceResponse = await _paymentConditionService.DeletePaymentConditionAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchPaymentConditionByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _paymentConditionService.SearchPaymentConditionByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
