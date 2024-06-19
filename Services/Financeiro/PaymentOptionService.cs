using AutoMapper;
using HefestusApi.DTOs.Financeiro;
using HefestusApi.Models.Financeiro;
using HefestusApi.Repositories.Financeiro.Interfaces;
using HefestusApi.Services.Financeiro.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Financeiro
{
    public class PaymentOptionService : IPaymentOptionService
    {
        private readonly IPaymentOptionsRepository _paymentOptionRepository;
        private readonly IMapper _mapper;

        public PaymentOptionService(IPaymentOptionsRepository paymentOptionRepository, IMapper mapper)
        {
            _paymentOptionRepository = paymentOptionRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<PaymentOptionDto>>> GetAllPaymentOptionsAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<PaymentOptionDto>>();
            try
            { 
                var paymentOptions = await _paymentOptionRepository.GetAllPaymentOptionsAsync(SystemLocationId);

                var paymentOptionDtos = _mapper.Map<IEnumerable<PaymentOptionDto>>(paymentOptions);

                response.Data = paymentOptionDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as opções de pagamento: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<PaymentOptions>> GetPaymentOptionByIdAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<PaymentOptions>();
            try
            {
                var paymentOption = await _paymentOptionRepository.GetPaymentOptionByIdAsync(SystemLocationId, id);
                if (paymentOption == null)
                {
                    response.Success = false;
                    response.Message = "Opção de pagamento não encontrada.";
                    return response;
                }

                response.Data = paymentOption;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a opções de pagamento: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchPaymentOptionByNameAsync(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var paymentOptions = await _paymentOptionRepository.SearchPaymentOptionByNameAsync(searchTerm.ToLower(), SystemLocationId);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = paymentOptions.Select(c => new PaymentOptionSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = paymentOptions.Cast<object>().ToList();
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

        public async Task<ServiceResponse<PaymentOptions>> CreatePaymentOptionAsync(PaymentOptionRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<PaymentOptions>();
            try
            {
                var paymentOption = new PaymentOptions
                {
                    Name = request.Name,
                    isUseCreditLimit = request.IsUseCreditLimit,
                    SystemLocationId = SystemLocationId
                };

                await _paymentOptionRepository.AddPaymentOptionAsync(paymentOption);

                response.Data = paymentOption;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a opções de pagamento: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdatePaymentOptionAsync(int id, PaymentOptionRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var paymentOption = await _paymentOptionRepository.GetPaymentOptionByIdAsync(SystemLocationId, id);
                if (paymentOption == null)
                {
                    response.Success = false;
                    response.Message = $"Opção de pagamento com o ID {id} não foi encontrada.";
                    return response;
                }

                paymentOption.Name = request.Name;
                paymentOption.isUseCreditLimit = request.IsUseCreditLimit;
                paymentOption.SystemLocationId = SystemLocationId;

                bool updateResult = await _paymentOptionRepository.UpdatePaymentOptionAsync(paymentOption);
                if (!updateResult)
                {
                    throw new Exception("A atualização da opções de pagamento falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a opções de pagamento: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeletePaymentOptionAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var paymentOption = await _paymentOptionRepository.GetPaymentOptionByIdAsync(SystemLocationId, id);

                if (paymentOption == null)
                {
                    response.Success = false;
                    response.Message = $"Opção de pagamento com o ID {id} não existe.";
                    return response;
                }

                bool deleted = await _paymentOptionRepository.DeletePaymentOptionAsync(paymentOption);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a opções de pagamento devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a opções de pagamento: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
