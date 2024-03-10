using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Financeiro
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class paymentOptionsController : ControllerBase
    {
        private readonly DataContext _context;
        public paymentOptionsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<PaymentOptions>> GetPaymentOptions()
        {
            var paymentOptions = await _context.PaymentOptions.ToListAsync();

            return Ok(paymentOptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentOptions>> GetPaymentOptionsById(int id)
        {
            var paymentOptions = await _context.PaymentOptions.FirstOrDefaultAsync(c => c.Id == id);

            if(paymentOptions == null)
            {
                return BadRequest($"Opção de pagamento com o id {id} não encontrado");
            }

            return Ok(paymentOptions);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentOptions>> PostPaymentOptions(PaymentOptionsPostOrPutDto request)
        {
            var newPaymentOption = new PaymentOptions
            {
                Name = request.Name,
                isUseCreditLimit = request.IsUseCreditLimit
            };

            _context.PaymentOptions.Add(newPaymentOption);
            await _context.SaveChangesAsync();
            return Ok(newPaymentOption);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentOptions>> PutPaymentOptions(int id, PaymentOptionsPostOrPutDto request)
        {
            var PaymentOption = await _context.PaymentOptions.FirstOrDefaultAsync(c => c.Id == id);

            if(PaymentOption == null)
            {
                return BadRequest($"Opção de pagamento com o id {id} não encontrado");
            }

            PaymentOption.Name = request.Name;
            PaymentOption.isUseCreditLimit = request.IsUseCreditLimit;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest($"Não foi possivel alterar o metodo de pagamento com o id {id}");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentOptionsDto>> DeletePaymentOption(int id)
        {
            var PaymentOption = await _context.PaymentOptions.FirstOrDefaultAsync(c => c.Id == id);

            if (PaymentOption == null)
            {
                return BadRequest($"Opção de pagamento com o id {id} não encontrado");
            }

            _context.Remove(PaymentOption);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest($"Não foi possivel remover o metodo de pagamento com o id {id}");
            }

            return NoContent();
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<PaymentOptionsSearchTermDto>>> GetPersonGroupBySearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var paymentOptions = await _context.PaymentOptions
                .Where(pg => pg.Name.ToLower().Contains(lowerCaseSearchTerm))
                .ToListAsync();

            return Ok(paymentOptions);
        }
    }
}
