﻿using AutoMapper;
using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Repositories.Financeiro.Interfaces;
using HefestusApi.Services.Financeiro.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Financeiro
{
    public class PaymentConditionService : IPaymentConditionService
    {
        private readonly IPaymentConditionRepository _paymentConditionRepository;
        private readonly IMapper _mapper;

        public PaymentConditionService(IPaymentConditionRepository paymentConditionRepository, IMapper mapper)
        {
            _paymentConditionRepository = paymentConditionRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<PaymentConditionDto>>> GetAllPaymentConditionsAsync()
        {
            var response = new ServiceResponse<IEnumerable<PaymentConditionDto>>();
            try
            {
                var paymentConditions = await _paymentConditionRepository.GetAllPaymentConditionsAsync();

                var paymentConditionDtos = _mapper.Map<IEnumerable<PaymentConditionDto>>(paymentConditions);

                response.Data = paymentConditionDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as condições de pagamento: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<PaymentCondition>> GetPaymentConditionByIdAsync(int id)
        {
            var response = new ServiceResponse<PaymentCondition>();
            try
            {
                var paymentCondition = await _paymentConditionRepository.GetPaymentConditionByIdAsync(id);
                if (paymentCondition == null)
                {
                    response.Success = false;
                    response.Message = "Condição de pagamento não encontrada.";
                    return response;
                }

                response.Data = paymentCondition;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a condições de pagamento: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchPaymentConditionByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var paymentConditions = await _paymentConditionRepository.SearchPaymentConditionByNameAsync(searchTerm.ToLower());

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = paymentConditions.Select(c => new PaymentConditionSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = paymentConditions.Cast<object>().ToList();
                }
                else
                {
                    throw new ArgumentException("Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.");
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

        public async Task<ServiceResponse<PaymentCondition>> CreatePaymentConditionAsync(PaymentConditionRequestDataDto request)
        {
            var response = new ServiceResponse<PaymentCondition>();
            try
            {
                var paymentCondition = new PaymentCondition
                {
                    Name = request.Name,
                    Installments = request.Installments,
                    Interval = request.Interval,
                };

                await _paymentConditionRepository.AddPaymentConditionAsync(paymentCondition);

                response.Data = paymentCondition;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a condições de pagamento: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdatePaymentConditionAsync(int id, PaymentConditionRequestDataDto request)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var paymentCondition = await _paymentConditionRepository.GetPaymentConditionByIdAsync(id);
                if (paymentCondition == null)
                {
                    response.Success = false;
                    response.Message = $"Condição de pagamento com o ID {id} não foi encontrada.";
                    return response;
                }

                paymentCondition.Name = request.Name;
                paymentCondition.Installments = request.Installments;
                paymentCondition.Interval = request.Interval;

                bool updateResult = await _paymentConditionRepository.UpdatePaymentConditionAsync(paymentCondition);
                if (!updateResult)
                {
                    throw new Exception("A atualização da condições de pagamento falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a condições de pagamento: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeletePaymentConditionAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var paymentCondition = await _paymentConditionRepository.GetPaymentConditionByIdAsync(id);

                if (paymentCondition == null)
                {
                    response.Success = false;
                    response.Message = $"Condição de pagamento com o ID {id} não existe.";
                    return response;
                }

                bool deleted = await _paymentConditionRepository.DeletePaymentConditionAsync(paymentCondition);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a condições de pagamento devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a condições de pagamento: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
