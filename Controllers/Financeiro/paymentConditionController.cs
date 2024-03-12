using HefestusApi.DTOs.Financeiro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HefestusApi.Services.Financeiro.Interfaces;

namespace HefestusApi.Controllers.Financeiro
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class paymentConditionController : ControllerBase
    {
        private readonly IPaymentConditionService _paymentConditionService;

        public paymentConditionController(IPaymentConditionService paymentConditionService)
        {
            _paymentConditionService = paymentConditionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentCondition()
        {
            var serviceResponse = await _paymentConditionService.GetAllPaymentConditionsAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentConditionById(int id)
        {
            var serviceResponse = await _paymentConditionService.GetPaymentConditionByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentCondition([FromBody] PaymentConditionRequestDataDto request)
        {
            var serviceResponse = await _paymentConditionService.CreatePaymentConditionAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return CreatedAtAction(nameof(GetPaymentConditionById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentCondition(int id, [FromBody] PaymentConditionRequestDataDto request)
        {
            var serviceResponse = await _paymentConditionService.UpdatePaymentConditionAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentCondition(int id)
        {
            var serviceResponse = await _paymentConditionService.DeletePaymentConditionAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchPaymentConditionByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _paymentConditionService.SearchPaymentConditionByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
