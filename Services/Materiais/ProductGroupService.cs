using AutoMapper;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Materiais.Interfaces;
using HefestusApi.Services.Materiais.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;

        public ProductGroupService(IProductGroupRepository productGroupRepository, IMapper mapper)
        {
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ProductGroupDto>>> GetAllProductGroupsAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<ProductGroupDto>>();
            try
            {
                var productGroups = await _productGroupRepository.GetAllProductGroupsAsync(SystemLocationId);

                var productGroupDtos = _mapper.Map<IEnumerable<ProductGroupDto>>(productGroups);

                response.Data = productGroupDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as grupo de produtos: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<ProductGroup>> GetProductGroupByIdAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<ProductGroup>();
            try
            {
                var productGroup = await _productGroupRepository.GetProductGroupByIdAsync(SystemLocationId, id);
                if (productGroup == null)
                {
                    response.Success = false;
                    response.Message = "Grupo de produtos não encontrada.";
                    return response;
                }

                response.Data = productGroup;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a grupo de produtos: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchProductGroupByNameAsync(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var productGroups = await _productGroupRepository.SearchProductGroupByNameAsync(searchTerm.ToLower(), SystemLocationId);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = productGroups.Select(c => new ProductGroupSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = productGroups.Cast<object>().ToList();
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

        public async Task<ServiceResponse<ProductGroup>> CreateProductGroupAsync(ProductGroupRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<ProductGroup>();
            try
            {
                var productGroup = new ProductGroup
                {
                    Name = request.Name,
                    SystemLocationId = SystemLocationId
                };

                await _productGroupRepository.AddProductGroupAsync(productGroup);

                response.Data = productGroup;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a grupo de produtos: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateProductGroupAsync( int id, ProductGroupRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productGroup = await _productGroupRepository.GetProductGroupByIdAsync(SystemLocationId, id);
                if (productGroup == null)
                {
                    response.Success = false;
                    response.Message = $"Grupo de produtos com o ID {id} não foi encontrada.";
                    return response;
                }

                productGroup.Name = request.Name;

                bool updateResult = await _productGroupRepository.UpdateProductGroupAsync(productGroup);
                if (!updateResult)
                {
                    throw new Exception("A atualização da grupo de produtos falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a grupo de produtos: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProductGroupAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var productGroup = await _productGroupRepository.GetProductGroupByIdAsync(SystemLocationId, id);

                if (productGroup == null)
                {
                    response.Success = false;
                    response.Message = $"Grupo de produtos com o ID {id} não existe.";
                    return response;
                }

                if (productGroup.Products.Any())
                {
                    response.Success = false;
                    response.Message = $"Grupo de produtos não pode ser excluida, pois está relacionado a produtos.";
                    return response;
                }

                bool deleted = await _productGroupRepository.DeleteProductGroupAsync(productGroup);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a grupo de produtos devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a grupo de produtos: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
