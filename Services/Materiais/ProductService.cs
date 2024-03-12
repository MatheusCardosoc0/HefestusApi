using AutoMapper;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Produtos;
using HefestusApi.Repositories.Materiais.Interfaces;
using HefestusApi.Services.Materiais.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Materiais
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<IEnumerable<ProductDto>>();
            try
            {
                var products = await _productRepository.GetAllProductsAsync();

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                response.Data = productDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas os produtos: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<Product>();
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "produto não encontrado.";
                    return response;
                }

                response.Data = product;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter os produto: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchProductByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var products = await _productRepository.SearchProductByNameAsync(searchTerm.ToLower());

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = products.Select(c => new ProductSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        PriceSale = c.PriceSale,
                        WholesalePrice = c.WholesalePrice,
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = products.Cast<object>().ToList();
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

        public async Task<ServiceResponse<Product>> CreateProductAsync(ProductRequestDataDto request)
        {
            var response = new ServiceResponse<Product>();
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    PriceSale = request.PriceSale,
                    LiquidCost = request.LiquidCost,
                    BruteCost = request.BruteCost,
                    GTIN = request.GTIN,
                    NCM = request.NCM,
                    AverageCost = request.AverageCost,
                    Batch = request.Batch,
                    GTINtrib = request.GTINtrib,
                    MinWholesalePrice = request.MinWholesalePrice,
                    PromotionalPrice = request.PromotionalPrice,
                    Reference = request.Reference,
                    UrlImage = request.UrlImage,
                    WholesalePrice = request.WholesalePrice,
                    MinPriceSale = request.MinPriceSale,
                    UnitOfMensuration = request.UnitOfMensuration,
                    GroupId = request.Group.Id,
                    FamilyId = request.Family.Id,
                    SubgroupId = request.SubGroup.Id,
                };

                await _productRepository.AddProductAsync(product);

                response.Data = product;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a produto: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateProductAsync(int id, ProductRequestDataDto request)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = $"produto com o ID {id} não foi encontrado.";
                    return response;
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.PriceSale = request.PriceSale;
                product.LiquidCost = request.LiquidCost;
                product.BruteCost = request.BruteCost;
                product.GTIN = request.GTIN;
                product.NCM = request.NCM;
                product.AverageCost = request.AverageCost;
                product.Batch = request.Batch;
                product.GTINtrib = request.GTINtrib;
                product.MinWholesalePrice = request.MinWholesalePrice;
                product.PromotionalPrice = request.PromotionalPrice;
                product.Reference = request.Reference;
                product.UrlImage = request.UrlImage;
                product.WholesalePrice = request.WholesalePrice;
                product.MinPriceSale = request.MinPriceSale;
                product.UnitOfMensuration = request.UnitOfMensuration;
                product.GroupId = request.Group.Id;
                product.FamilyId = request.Family.Id;
                product.SubgroupId = request.SubGroup.Id;


                bool updateResult = await _productRepository.UpdateProductAsync(product);
                if (!updateResult)
                {
                    throw new Exception("A atualização da produto falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar o produto: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);

                if (product == null)
                {
                    response.Success = false;
                    response.Message = $"produto com o ID {id} não existe.";
                    return response;
                }

                bool deleted = await _productRepository.DeleteProductAsync(product);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a produto devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a produto: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
