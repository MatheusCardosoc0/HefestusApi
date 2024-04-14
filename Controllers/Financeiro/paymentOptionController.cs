using HefestusApi.DTOs.Financeiro;
using HefestusApi.Services.Financeiro.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Financeiro
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class paymentOptionController : ControllerBase
    {
        private readonly IPaymentOptionService _paymentOptionsService;

        public paymentOptionController(IPaymentOptionService paymentOptionsService)
        {
            _paymentOptionsService = paymentOptionsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentOptions()
        {
            var serviceResponse = await _paymentOptionsService.GetAllPaymentOptionsAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{detailLevel}/{id}/{locationId}")]
        public async Task<IActionResult> GetPaymentOptionById(int id)
        {
            var serviceResponse = await _paymentOptionsService.GetPaymentOptionByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentOption([FromBody] PaymentOptionRequestDataDto request)
        {
            var serviceResponse = await _paymentOptionsService.CreatePaymentOptionAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
           
            return CreatedAtAction(nameof(GetPaymentOptionById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentOption(int id, [FromBody] PaymentOptionRequestDataDto request)
        {
            var serviceResponse = await _paymentOptionsService.UpdatePaymentOptionAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentOption(int id)
        {
            var serviceResponse = await _paymentOptionsService.DeletePaymentOptionAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchPaymentOptionByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _paymentOptionsService.SearchPaymentOptionByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}
