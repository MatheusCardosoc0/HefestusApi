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

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<ProductDto>>();
            try
            {
                var products = await _productRepository.GetAllProductsAsync(SystemLocationId);

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

        public async Task<ServiceResponse<object>> GetProductByIdAsync(string SystemLocationId, int id, string detailLevel, int SubLocationId)
        {
            var response = new ServiceResponse<object>();
            try
            {
                var product = await _productRepository.GetProductByIdAsync(SystemLocationId, id);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = new ProductSimpleSearchDataDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        PriceSale = product.Stocks.FirstOrDefault(s => s.SubLocationId == SubLocationId).PriceSale,
                        WholesalePrice = product.Stocks.FirstOrDefault(s => s.SubLocationId == SubLocationId).WholesalePrice,
                    };

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = product;
                }
                else
                {
                    throw new ArgumentException("Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter os produto: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchProductByNameAsync(string searchTerm, string detailLevel, string SystemLocationId, int SubLocationId)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var products = await _productRepository.SearchProductByNameAsync(searchTerm.ToLower(), SystemLocationId);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = products.Select(c => new ProductSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        PriceSale = c.Stocks.FirstOrDefault(s => s.SubLocationId == SubLocationId).PriceSale,
                        WholesalePrice = c.Stocks.FirstOrDefault(s => s.SubLocationId == SubLocationId).WholesalePrice,
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

        public async Task<ServiceResponse<Stock>> CreateProductAsync(ProductRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<Stock>();
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    GTIN = request.GTIN,
                    NCM = request.NCM,
                    Batch = request.Batch,
                    GTINtrib = request.GTINtrib,
                    Reference = request.Reference,
                    UrlImage = request.UrlImage,
                    UnitOfMensuration = request.UnitOfMensuration,
                    GroupId = request.Group.Id,
                    FamilyId = request.Family.Id,
                    SubgroupId = request.SubGroup.Id,
                    SystemLocationId = SystemLocationId

                };
                await _productRepository.AddProductAsync(product);

                var stock = new Stock
                {
                    PriceSale = request.PriceSale,
                    LiquidCost = request.LiquidCost,
                    BruteCost = request.BruteCost,
                    AverageCost = request.AverageCost,
                    MinWholesalePrice = request.MinWholesalePrice,
                    PromotionalPrice = request.PromotionalPrice,
                    WholesalePrice = request.WholesalePrice,
                    MinPriceSale = request.MinPriceSale,
                    ProductId = product.Id,
                    MinStock = request.MinStock,
                    MaxStock = request.MaxStock,
                    CurrentStock = request.CurrentStock,
                    SubLocationId = request.SubLocationId,
                    LastStockUpdate = new DateTime(),
                    Location = request.Location
                };
                await _productRepository.AddStockAsync(stock);
                await _productRepository.SaveChangesAsync();

                response.Data = stock;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a produto: {ex}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateProductAsync(int id, ProductRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var product = await _productRepository.GetProductByIdAsync(SystemLocationId, id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = $"produto com o ID {id} não foi encontrado.";
                    return response;
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.GTIN = request.GTIN;
                product.NCM = request.NCM;
                product.Batch = request.Batch;
                product.GTINtrib = request.GTINtrib;
                product.Reference = request.Reference;
                product.UrlImage = request.UrlImage;
                product.UnitOfMensuration = request.UnitOfMensuration;
                product.GroupId = request.Group.Id;
                product.FamilyId = request.Family.Id;
                product.SubgroupId = request.SubGroup.Id;
                product.SystemLocationId = SystemLocationId;


                bool updateResultProduct = await _productRepository.UpdateProductAsync(product);

                var stock = await _productRepository.GetSelectedStockAsync(request.SubLocationId, id, SystemLocationId);
                if (stock == null)
                {
                    response.Success = false;
                    response.Message = $"Estoque relacionado com o producto com ID {id} não foi encontrado.";
                    return response;
                }

                stock.PriceSale = request.PriceSale;
                stock.LiquidCost = request.LiquidCost;
                stock.BruteCost = request.BruteCost;
                stock.AverageCost = request.AverageCost;
                stock.MinWholesalePrice = request.MinWholesalePrice;
                stock.PromotionalPrice = request.PromotionalPrice;
                stock.WholesalePrice = request.WholesalePrice;
                stock.MinPriceSale = request.MinPriceSale;
                stock.MaxStock = request.MaxStock;
                stock.MinStock = request.MinStock;
                stock.CurrentStock = request.CurrentStock;
                stock.LastStockUpdate = new DateTime();
                stock.Location = request.Location;

                bool updateResultStock = await _productRepository.UpdateStockAsync(stock);


                if (!updateResultProduct || !updateResultStock)
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

        public async Task<ServiceResponse<bool>> DeleteProductAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var product = await _productRepository.GetProductByIdAsync(SystemLocationId, id);

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
