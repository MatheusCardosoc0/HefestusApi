using AutoMapper;
using HefestusApi.DTOs.Administracao;
using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Administracao;
using HefestusApi.Models.Produtos;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace HefestusApi.Controllers.Produtos
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public productController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProduct()
        {
            var product = await _context.Product
                .Include(c => c.Group)
                .Include(c => c.Subgroup)
                .Include(c => c.Family)
                .ToListAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(product);

            return Ok(productDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Product
                .Include(c => c.Group)
                .Include(c => c.Subgroup)
                .Include(c => c.Family)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(product == null)
            {
                return NotFound($"Produto com o id {id} não existe");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductPostOrPutDto request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                PriceSale = request.PriceSale,
                FamilyId = request.FamilyId,
                GroupId = request.GroupId,
                SubgroupId = request.SubGroupId,
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
                Group = new ProductGroup(),
                Family = new ProductFamily(),
                Subgroup = new ProductSubGroup(),
            };


            var existingProductGroup = await _context.ProductGroups
                    .FindAsync(request.GroupId);

            if (existingProductGroup != null)
            {
                newProduct.Group = existingProductGroup;
                newProduct.GroupId = existingProductGroup.Id;
            }
            else
            {
                return BadRequest($"Grupo de produtos {request.GroupId} não encontrada");
            }


            var existingProductFamily = await _context.ProductFamily
                   .FindAsync(request.FamilyId);

            if (existingProductFamily != null)
            {
                newProduct.Family = existingProductFamily;
                newProduct.FamilyId = existingProductFamily.Id;
            }
            else
            {
                return BadRequest($"Familia de produtos {request.FamilyId} não encontrada");
            }


            var existingProductSubGroup = await _context.ProductSubGroup
                   .FindAsync(request.SubGroupId);

            if (existingProductSubGroup != null)
            {
                newProduct.Subgroup = existingProductSubGroup;
                newProduct.SubgroupId = existingProductSubGroup.Id;
            }
            else
            {
                return BadRequest($"Sub Grupo de produtos {request.SubGroupId} não encontrada");
            }


            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductPostOrPutDto request)
        {
            var product = await _context.Product
                .Include(c => c.Group)
                .Include(c => c.Subgroup)
                .Include(c => c.Family)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (product == null)
            {
                return NotFound($"Produto com o id {id} não existe");
            }

                product.Name = request.Name;
                product.Description = request.Description;
                product.PriceSale = request.PriceSale;
                product.FamilyId = request.FamilyId;
                product.GroupId = request.GroupId;
                product.SubgroupId = request.SubGroupId;
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

            var existingProductGroup = await _context.ProductGroups
                    .FindAsync(request.GroupId);

            if (existingProductGroup != null)
            {
                product.Group = existingProductGroup;
                product.GroupId = existingProductGroup.Id;
            }
            else
            {
                return BadRequest($"Grupo de produtos {request.GroupId} não encontrada");
            }


            var existingProductFamily = await _context.ProductFamily
                   .FindAsync(request.FamilyId);

            if (existingProductFamily != null)
            {
                product.Family = existingProductFamily;
                product.FamilyId = existingProductFamily.Id;
            }
            else
            {
                return BadRequest($"Familia de produtos {request.FamilyId} não encontrada");
            }


            var existingProductSubGroup = await _context.ProductSubGroup
                   .FindAsync(request.SubGroupId);

            if (existingProductSubGroup != null)
            {
                product.Subgroup = existingProductSubGroup;
                product.SubgroupId = existingProductSubGroup.Id;
            }
            else
            {
                return BadRequest($"Sub Grupo de produtos {request.SubGroupId} não encontrada");
            }

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product
                .FirstOrDefaultAsync(Product => Product.Id == id);

            if(product == null)
            {
                return BadRequest($"O usuátio com o id {id} não existe");
            }

            _context.Product.Remove(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server error while deleting the product.");
            }

            return NoContent();
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<ProductSearchTermDto>>> GetProductSearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var product = await _context.Product
                .Where(pg => pg.Name.ToLower().Contains(lowerCaseSearchTerm))
                .ToListAsync();

            return Ok(product);
        }
    }
}
