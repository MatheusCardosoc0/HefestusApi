using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HefestusApi.Repositories.Data;

namespace HefestusApi.Controllers.Financeiro
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class paymentConditionController : ControllerBase
    {
        private readonly DataContext _context;
        public paymentConditionController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<PaymentCondition>> GetPaymentCondition()
        {
            var paymentCondition = await _context.PaymentCondition.ToListAsync();

            return Ok(paymentCondition);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentCondition>> GetPaymentConditionById(int id)
        {
            var paymentCondition = await _context.PaymentCondition.FirstOrDefaultAsync(c => c.Id == id);

            if (paymentCondition == null)
            {
                return BadRequest($"Opção de pagamento com o id {id} não encontrado");
            }

            return Ok(paymentCondition);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentCondition>> PostPaymentCondition(PaymentConditionPostOrPutDto request)
        {
            var newPaymentCondition = new PaymentCondition
            {
                Name = request.Name,
                Installments = request.Interval,
                Interval = request.Interval,
            };

            _context.PaymentCondition.Add(newPaymentCondition);
            await _context.SaveChangesAsync();
            return Ok(newPaymentCondition);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentCondition>> PutPaymentCondition(int id, PaymentConditionPostOrPutDto request)
        {
            var PaymentCondition = await _context.PaymentCondition.FirstOrDefaultAsync(c => c.Id == id);

            if (PaymentCondition == null)
            {
                return BadRequest($"Opção de pagamento com o id {id} não encontrado");
            }

            PaymentCondition.Name = request.Name;
            PaymentCondition.Interval = request.Interval;
            PaymentCondition.Installments = request.Installments;

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
        public async Task<ActionResult<PaymentCondition>> DeletePaymentCondition(int id)
        {
            var PaymentCondition = await _context.PaymentCondition.FirstOrDefaultAsync(c => c.Id == id);

            if (PaymentCondition == null)
            {
                return BadRequest($"Opção de pagamento com o id {id} não encontrado");
            }

            _context.Remove(PaymentCondition);

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
        public async Task<ActionResult<IEnumerable<PaymentConditionSearchTermDto>>> GetPersonGroupBySearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var paymentConditions = await _context.PaymentCondition
                .Where(pg => pg.Name.ToLower().Contains(lowerCaseSearchTerm))
                .ToListAsync();

            return Ok(paymentConditions);
        }
    }
}
