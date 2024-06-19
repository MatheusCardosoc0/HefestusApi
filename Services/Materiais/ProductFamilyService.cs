using AutoMapper;
using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Interfaces;
using HefestusApi.Repositories.Materiais.Interfaces;
using HefestusApi.Services.Interfaces;
using HefestusApi.Services.Materiais.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais
{
    public class ProductFamilyService : IProductFamilyService
    {
        private readonly IProductFamilyRepository _productFamilyRepository;
        private readonly IMapper _mapper;

        public ProductFamilyService(IProductFamilyRepository productFamilyRepository, IMapper mapper)
        {
            _productFamilyRepository = productFamilyRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ProductFamilyDto>>> GetAllProductFamiliesAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<ProductFamilyDto>>();
            try
            {
                var productFamilies = await _productFamilyRepository.GetAllProductFamiliesAsync(SystemLocationId);
                var productFamilyDtos = _mapper.Map<IEnumerable<ProductFamilyDto>>(productFamilies);
                response.Data = productFamilyDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as famílias de produtos: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<ProductFamily>> GetProductFamilyByIdAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<ProductFamily>();
            try
            {
                var productFamily = await _productFamilyRepository.GetProductFamilyByIdAsync(SystemLocationId, id);
                if (productFamily == null)
                {
                    response.Success = false;
                    response.Message = "Família de produtos não encontrada.";
                    return response;
                }

                response.Data = productFamily;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a família de produtos: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<object>>> SearchProductFamilyByNameAsync(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var productFamilies = await _productFamilyRepository.SearchProductFamilyByNameAsync(searchTerm.ToLower(), SystemLocationId);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = productFamilies.Select(c => new ProductFamilySimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = productFamilies.Cast<object>().ToList();
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

        public async Task<ServiceResponse<ProductFamily>> CreateProductFamilyAsync(ProductFamilyRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<ProductFamily>();
            try
            {
                var productFamily = new ProductFamily
                {
                    Name = request.Name,
                    SystemLocationId = SystemLocationId
                };

                await _productFamilyRepository.AddProductFamilyAsync(productFamily);
                response.Data = productFamily;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a família de produtos: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateProductFamilyAsync(int id, ProductFamilyRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productFamily = await _productFamilyRepository.GetProductFamilyByIdAsync(SystemLocationId, id);
                if (productFamily == null)
                {
                    response.Success = false;
                    response.Message = $"Família de produtos com o ID {id} não foi encontrada.";
                    return response;
                }

                productFamily.Name = request.Name;
                productFamily.SystemLocationId = SystemLocationId;

                bool updateResult = await _productFamilyRepository.UpdateProductFamilyAsync(productFamily);
                if (!updateResult)
                {
                    throw new Exception("A atualização da família de produtos falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a família de produtos: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProductFamilyAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productFamily = await _productFamilyRepository.GetProductFamilyByIdAsync(SystemLocationId, id);

                if (productFamily == null)
                {
                    response.Success = false;
                    response.Message = $"Família de produtos com o ID {id} não existe.";
                    return response;
                }

                if (productFamily.Products.Any())
                {
                    response.Success = false;
                    response.Message = $"Família de produtos não pode ser excluída, pois está relacionada a produtos.";
                    return response;
                }

                bool deleted = await _productFamilyRepository.DeleteProductFamilyAsync(productFamily);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a família de produtos devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a família de produtos: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
