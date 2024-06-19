using AutoMapper;
using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Vendas;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Vendas;
using HefestusApi.Repositories.Pessoal.Interfaces;
using HefestusApi.Repositories.Vendas.Interfaces;
using HefestusApi.Services.Vendas.Interfaces;
using HefestusApi.Utilities.functions;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Services.Vendas
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<OrderDto>>();
            try
            {
                var order = await _orderRepository.GetAllOrdersAsync(SystemLocationId);

                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(order);

                response.Data = orderDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as orderm: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<OrderDto>();
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(SystemLocationId, id);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Usuário não encontrada.";
                    return response;
                }

                var orderDtos = _mapper.Map<OrderDto>(order);

                response.Data = orderDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a orderm: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchOrderByNameAsync(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var order = await _orderRepository.SearchOrderByNameAsync(searchTerm.ToLower(), SystemLocationId);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = order.Select(c => new OrderSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Client.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = _mapper.Map<IEnumerable<OrderDto>>(order).Cast<object>().ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao processar a busca: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<Order>> CreateOrderAsync(OrderRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<Order>();
            try
            {
                var order = new Order
                {
                    ClientId = request.Client.Id,
                    ResponsibleId = request.Responsible.Id,
                    PaymentConditionId = request.PaymentCondition.Id,
                    PaymentOptionId = request.PaymentOption.Id,
                    InvoiceId = request.InvoiceId,
                    TotalValue = request.TotalValue,
                    LiquidValue = request.LiquidValue,
                    BruteValue = request.BruteValue,
                    Discount = request.Discount,
                    TypeOrder = request.TypeOrder,
                    CostOfFreight = request.CostOfFreight ?? 0,
                    TypeFreight = request.TypeFreight,
                    OrderProducts = new List<OrderProduct>(),
                    OrderInstallments = new List<OrderInstallment>(),
                    SystemLocationId = SystemLocationId
                };

                await _orderRepository.AddOrderAsync(order);

                foreach (var orderProductDto in request.OrderProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = orderProductDto.ProductId,
                        Amount = orderProductDto.Amount,
                        UnitPrice = orderProductDto.UnitPrice,
                        TotalPrice = orderProductDto.TotalPrice
                    };

                    await _orderRepository.AddOrderProductAsync(orderProduct);
                }

                foreach (var orderInstallmentDto in request.OrderInstallments)
                {
                    var orderInstallment = new OrderInstallment
                    {
                        OrderId = order.Id,
                        InstallmentNumber = orderInstallmentDto.InstallmentNumber,
                        PaymentOptionId = orderInstallmentDto.PaymentOptionId,
                        Maturity = orderInstallmentDto.Maturity,
                        Value = orderInstallmentDto.Value,
                    };

                    await _orderRepository.AddOrderInstallMentsAsync(orderInstallment);
                }

                await _orderRepository.SaveCahngesAsymc();
                response.Data = order;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a orderm: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateOrderAsync( int id, OrderRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var orderToUpdate = await _orderRepository.GetOrderByIdAsync(SystemLocationId, id);

                if (orderToUpdate == null)
                {
                    response.Success = false;
                    response.Message = $"Usuário com o ID {id} não encontrado.";
                    return response;
                }


                orderToUpdate.ClientId = request.Client.Id;
                orderToUpdate.ResponsibleId = request.Responsible.Id;
                orderToUpdate.PaymentConditionId = request.PaymentCondition.Id;
                orderToUpdate.PaymentOptionId = request.PaymentOption.Id;
                orderToUpdate.InvoiceId = request.InvoiceId;
                orderToUpdate.TotalValue = request.TotalValue;
                orderToUpdate.LiquidValue = request.LiquidValue;
                orderToUpdate.BruteValue = request.BruteValue;
                orderToUpdate.Discount = request.Discount;
                orderToUpdate.TypeOrder = request.TypeOrder;
                orderToUpdate.CostOfFreight = request.CostOfFreight ?? 0;
                orderToUpdate.TypeFreight = request.TypeFreight;
                orderToUpdate.SystemLocationId = SystemLocationId;


                await _orderRepository.RemoveOrderProductAsync(orderToUpdate.OrderProducts);
                foreach (var op in request.OrderProducts)
                {
                    orderToUpdate.OrderProducts.Add(new OrderProduct
                    {
                        OrderId = id,
                        ProductId = op.ProductId,
                        Amount = op.Amount,
                        UnitPrice = op.UnitPrice,
                        TotalPrice = op.TotalPrice 
                    });
                }


                await _orderRepository.RemoveOrderInstallmentsAsync(orderToUpdate.OrderInstallments);
                foreach (var oi in request.OrderInstallments)
                {
                    orderToUpdate.OrderInstallments.Add(new OrderInstallment
                    {
                        OrderId = id,
                        InstallmentNumber = oi.InstallmentNumber,
                        PaymentOptionId = oi.PaymentOptionId,
                        Maturity = oi.Maturity,
                        Value = oi.Value
                    });
                }

                bool updateResult = await _orderRepository.UpdateOrderAsync(orderToUpdate);
                if (!updateResult)
                {
                    throw new Exception("A atualização do orderm falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a orderm: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteOrderAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(SystemLocationId, id);

                if (order == null)
                {
                    response.Success = false;
                    response.Message = $"Usuário com o ID {id} não existe.";
                    return response;
                }

                bool deleted = await _orderRepository.DeleteOrderAsync(order);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a orderm devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a orderm: " + ex.Message;
            }

            return response;
        }
    }
}
