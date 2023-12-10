using HefestusApi.DTOs.Produtos;
using HefestusApi.Models.Administracao;
using HefestusApi.Models.Produtos;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Produtos
{
    [Route("api/[controller]")]
    [ApiController]
    public class product : ControllerBase
    {
        private readonly DataContext _context;
        public product(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct()
        {
            var product = await _context.Product
                .Include(c => c.Group)
                .Include(c => c.Subgroup)
                .Include(c => c.Family)
                .ToListAsync();

            return Ok(product);
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
        public async Task<ActionResult<Product>> CreateProduct(ProductDto request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                PriceTotal = request.PriceTotal,
                PriceSale = request.PriceSale,
                FamilyId = request.FamilyId,
                GroupId = request.GroupId,
                SubgroupId = request.SubGroupId,
                Group = new ProductGroup(),
                Family = new ProductFamily(),
                Subgroup = new ProductSubGroup(),
            };

            if (request.Group != null)
            {
                var existingGroup = await _context.ProductGroups
                    .FirstOrDefaultAsync(pg => pg.Name == request.Group.Name);

                if (existingGroup != null)
                {
                    newProduct.Group = existingGroup;
                    newProduct.GroupId = existingGroup.Id; 
                }
                else
                {
                    var newGroup = new ProductGroup { Name = request.Group.Name };
                    _context.ProductGroups.Add(newGroup);
                    await _context.SaveChangesAsync();
                    newProduct.Group = newGroup;
                    newProduct.GroupId = newGroup.Id;
                }
            }

            if (request.Family != null)
            {
                var existingFamily = await _context.ProductFamily
                    .FirstOrDefaultAsync(pf => pf.Name == request.Family.Name);

                if (existingFamily != null)
                {
                    newProduct.Family = existingFamily;
                    newProduct.FamilyId = existingFamily.Id; 
                }
                else
                {
                    var newFamily = new ProductFamily { Name = request.Family.Name };
                    _context.ProductFamily.Add(newFamily);
                    await _context.SaveChangesAsync();
                    newProduct.Family = newFamily;
                    newProduct.FamilyId = newFamily.Id;
                }
            }

            if (request.Subgroup != null)
            {
                var existingSubgroup = await _context.ProductSubGroup
                    .FirstOrDefaultAsync(psg => psg.Name == request.Subgroup.Name);

                if (existingSubgroup != null)
                {
                    newProduct.Subgroup = existingSubgroup;
                    newProduct.SubgroupId = existingSubgroup.Id; 
                }
                else
                {
                    var newSubgroup = new ProductSubGroup { Name = request.Subgroup.Name };
                    _context.ProductSubGroup.Add(newSubgroup);
                    await _context.SaveChangesAsync();
                    newProduct.Subgroup = newSubgroup;
                    newProduct.SubgroupId = newSubgroup.Id;
                }
            }

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }
    }
}
