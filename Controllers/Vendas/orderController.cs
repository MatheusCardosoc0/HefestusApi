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
                .Include(o => o.OrderInstallments)
                .ToListAsync();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderPostOrPutDto request)
        {
            if (request == null || request.OrderProducts == null || !request.OrderProducts.Any())
            {
                return BadRequest("Dados do pedido inválidos ou incompletos.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

           
                var newOrder = new Order
                {
                    ClientId = request.ClientId,
                    ResponsibleId = request.ResponsibleId,
                    PaymentConditionId = request.PaymentConditionId,
                    PaymentOptionId = request.PaymentOptionId,
                    InvoiceId = request.InvoiceId,
                    TotalValue = request.TotalValue,
                    LiquidValue = request.LiquidValue,
                    BruteValue = request.BruteValue,
                    Discount = request.Discount,
                    TypeOrder = request.TypeOrder,
                    CostOfFreight = request.CostOfFreight ?? 0,
                    TypeFreight = request.TypeFreight,
                    OrderProducts = new List<OrderProduct>(),
                    OrderInstallments = new List<OrderInstallment>()
                };

                _context.Order.Add(newOrder);
                await _context.SaveChangesAsync();

                foreach (var orderProductDto in request.OrderProducts)
                {
                    var product = await _context.Product.FindAsync(orderProductDto.ProductId);
                    if (product == null)
                    {
                        return BadRequest($"Produto com ID {orderProductDto.ProductId} não encontrado.");
                    }

                    var orderProduct = new OrderProduct
                    {
                        OrderId = newOrder.Id,
                        ProductId = product.Id,
                        Amount = orderProductDto.Amount,
                        UnitPrice = orderProductDto.UnitPrice,
                        TotalPrice = orderProductDto.TotalPrice
                    };

                    _context.OrderProduct.Add(orderProduct);
                }

                foreach (var orderInstallmentDto in request.OrderInstallments)
                {
                    var paymentCondition = await _context.PaymentCondition.FindAsync(orderInstallmentDto.PaymentOptionId);
                    if (paymentCondition == null)
                    {
                        return BadRequest($"Metodo de pagamento com ID {orderInstallmentDto.PaymentOptionId} não encontrado.");
                    }

                    var orderProduct = new OrderInstallment
                    {
                        OrderId = newOrder.Id,
                        InstallmentNumber = orderInstallmentDto.InstallmentNumber,
                        PaymentOptionId = paymentCondition.Id,
                        Maturity = orderInstallmentDto.Maturity,
                        Value = orderInstallmentDto.Value,
                    };

                    _context.OrderInstallment.Add(orderProduct);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(newOrder);
        }

    }
}
