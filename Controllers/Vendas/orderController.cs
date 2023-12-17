using AutoMapper;
using HefestusApi.DTOs.Administracao;
using HefestusApi.DTOs.Vendas;
using HefestusApi.Models.Administracao;
using HefestusApi.Models.Financeiro;
using HefestusApi.Models.Produtos;
using HefestusApi.Models.Vendas;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Vendas
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public orderController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            var orders = await _context.Order
                .Include(o => o.Client)
                .Include(o => o.Responsible)
                .Include(o => o.OrderProducts)
                  .ThenInclude(op => op.Product)
                .Include(o => o.PaymentOption)
                .Include(o => o.PaymentCondition)
                .ToListAsync();

            var orderDtos = _mapper.Map<IEnumerable<OrderViewDto>>(orders);
            return Ok(orderDtos);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto request)
        {
            if (request == null || request.OrderProducts == null || !request.OrderProducts.Any())
            {
                return BadRequest("Dados do pedido inválidos ou incompletos.");
            }

            var newOrder = new Order
            {
                Value = request.Value,
                OrderProducts = new List<OrderProduct>()
            };

            var client = await _context.Person.FindAsync(request.Client.Id);
            if (client == null)
            {
                return BadRequest("Cliente não encontrado.");
            }
            newOrder.ClientId = client.Id;
            newOrder.Client = client;

            var responsible = await _context.Person.FindAsync(request.Responsible.Id);
            if (responsible == null)
            {
                return BadRequest("Responsável não encontrado.");
            }
            newOrder.ResponsibleId = responsible.Id;
            newOrder.Responsible = responsible;

            var paymentCondition = await _context.PaymentCondition.FindAsync(request.PaymentCondition.Id);
            if (paymentCondition == null)
            {
                return BadRequest("Condição de pagamento não encontrada.");
            }
            newOrder.PaymentConditionId = paymentCondition.Id;
            newOrder.PaymentCondition = paymentCondition;

            var paymentOption = await _context.PaymentOptions.FindAsync(request.PaymentCondition.Id);
            if (paymentOption == null)
            {
                return BadRequest("Opção de pagamento não encontrada.");
            }
            newOrder.PaymentOptionId = paymentOption.Id;
            newOrder.PaymentOption = paymentOption;

            foreach (var orderProductDto in request.OrderProducts)
            {
                var product = await _context.Product.FindAsync(orderProductDto.ProductId);
                if (product == null)
                {
                    return BadRequest($"Produto com ID {orderProductDto.ProductId} não encontrado.");
                }

                var orderProduct = new OrderProduct
                {
                    Product = product,
                    Price = orderProductDto.Price
                };
                newOrder.OrderProducts.Add(orderProduct);
            }

            _context.Order.Add(newOrder);
            await _context.SaveChangesAsync();

            
            //var orderDto = /* Método para converter Order em OrderDto */;
            return Ok(newOrder);
        }

    }
}
