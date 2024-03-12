using AutoMapper;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Materiais.Interfaces;
using HefestusApi.Services.Materiais.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais
{
    public class ProductSubGroupService : IProductSubGroupService
    {
        private readonly IProductSubGroupRepository _productSubGroupRepository;
        private readonly IMapper _mapper;

        public ProductSubGroupService(IProductSubGroupRepository productSubGroupRepository, IMapper mapper)
        {
            _productSubGroupRepository = productSubGroupRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ProductSubGroupDto>>> GetAllProductSubGroupsAsync()
        {
            var response = new ServiceResponse<IEnumerable<ProductSubGroupDto>>();
            try
            {
                var productSubGroups = await _productSubGroupRepository.GetAllProductSubGroupsAsync();

                var productSubGroupDtos = _mapper.Map<IEnumerable<ProductSubGroupDto>>(productSubGroups);

                response.Data = productSubGroupDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as sub grupo de produtos: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<ProductSubGroup>> GetProductSubGroupByIdAsync(int id)
        {
            var response = new ServiceResponse<ProductSubGroup>();
            try
            {
                var productSubGroup = await _productSubGroupRepository.GetProductSubGroupByIdAsync(id);
                if (productSubGroup == null)
                {
                    response.Success = false;
                    response.Message = "Sub grupo de produtos não encontrada.";
                    return response;
                }

                response.Data = productSubGroup;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a sub grupo de produtos: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchProductSubGroupByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var productSubGroups = await _productSubGroupRepository.SearchProductSubGroupByNameAsync(searchTerm.ToLower());

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = productSubGroups.Select(c => new ProductSubGroupSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = productSubGroups.Cast<object>().ToList();
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

        public async Task<ServiceResponse<ProductSubGroup>> CreateProductSubGroupAsync(ProductSubGroupRequestDataDto request)
        {
            var response = new ServiceResponse<ProductSubGroup>();
            try
            {
                var productSubGroup = new ProductSubGroup
                {
                    Name = request.Name,
                };

                await _productSubGroupRepository.AddProductSubGroupAsync(productSubGroup);

                response.Data = productSubGroup;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a sub grupo de produtos: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateProductSubGroupAsync(int id, ProductSubGroupRequestDataDto request)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productSubGroup = await _productSubGroupRepository.GetProductSubGroupByIdAsync(id);
                if (productSubGroup == null)
                {
                    response.Success = false;
                    response.Message = $"Sub grupo de produtos com o ID {id} não foi encontrada.";
                    return response;
                }

                productSubGroup.Name = request.Name;

                bool updateResult = await _productSubGroupRepository.UpdateProductSubGroupAsync(productSubGroup);
                if (!updateResult)
                {
                    throw new Exception("A atualização da sub grupo de produtos falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a sub grupo de produtos: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProductSubGroupAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productSubGroup = await _productSubGroupRepository.GetProductSubGroupByIdAsync(id);

                if (productSubGroup == null)
                {
                    response.Success = false;
                    response.Message = $"Sub grupo de produtos com o ID {id} não existe.";
                    return response;
                }

                if (productSubGroup.Products.Any())
                {
                    response.Success = false;
                    response.Message = $"Sub grupo de produtos não pode ser excluida, pois está relacionado a produtos.";
                    return response;
                }

                bool deleted = await _productSubGroupRepository.DeleteProductSubGroupAsync(productSubGroup);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a sub grupo de produtos devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a sub grupo de produtos: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
