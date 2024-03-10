using AutoMapper;
using HefestusApi.DTOs.Pessoal;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Pessoal;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Interfaces;
using HefestusApi.Repositories.Materiais.Interfaces;
using HefestusApi.Services.functions;
using HefestusApi.Services.Interfaces;
using HefestusApi.Services.Materiais.Interfaces;

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

        public async Task<ServiceResponse<IEnumerable<ProductFamilyDto>>> GetAllProductFamiliesAsync()
        {
            var response = new ServiceResponse<IEnumerable<ProductFamilyDto>>();
            try
            {
                var productFamilies = await _productFamilyRepository.GetAllProductFamiliesAsync();

                var productFamilyDtos = _mapper.Map<IEnumerable<ProductFamilyDto>>(productFamilies);

                response.Data = productFamilyDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as familia de produtos: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<ProductFamily>> GetProductFamilyByIdAsync(int id)
        {
            var response = new ServiceResponse<ProductFamily>();
            try
            {
                var productFamily = await _productFamilyRepository.GetProductFamilyByIdAsync(id);
                if (productFamily == null)
                {
                    response.Success = false;
                    response.Message = "Familia de produtos não encontrada.";
                    return response;
                }

                response.Data = productFamily;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a familia de produtos: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchProductFamilyByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var productFamilies = await _productFamilyRepository.SearchProductFamilyByNameAsync(searchTerm.ToLower());

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

        public async Task<ServiceResponse<ProductFamily>> CreateProductFamilyAsync(ProductFamilyRequestDataDto request)
        {
            var response = new ServiceResponse<ProductFamily>();
            try
            {
                var productFamily = new ProductFamily
                {
                    Name = request.Name,
                };

                await _productFamilyRepository.AddProductFamilyAsync(productFamily);

                response.Data = productFamily;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a familia de produtos: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateProductFamilyAsync(int id, ProductFamilyRequestDataDto request)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productFamily = await _productFamilyRepository.GetProductFamilyByIdAsync(id);
                if (productFamily == null)
                {
                    response.Success = false;
                    response.Message = $"Familia de produtos com o ID {id} não foi encontrada.";
                    return response;
                }

                productFamily.Name = request.Name;

                bool updateResult = await _productFamilyRepository.UpdateProductFamilyAsync(productFamily);
                if (!updateResult)
                {
                    throw new Exception("A atualização da familia de produtos falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a familia de produtos: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProductFamilyAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productFamily = await _productFamilyRepository.GetProductFamilyByIdAsync(id);

                if (productFamily == null)
                {
                    response.Success = false;
                    response.Message = $"Familia de produtos com o ID {id} não existe.";
                    return response;
                }

                if (productFamily.Products.Any())
                {
                    response.Success = false;
                    response.Message = $"Familia de produtos não pode ser excluida, pois está relacionado a produtos.";
                    return response;
                }

                bool deleted = await _productFamilyRepository.DeleteProductFamilyAsync(productFamily);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a familia de produtos devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a familia de produtos: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
