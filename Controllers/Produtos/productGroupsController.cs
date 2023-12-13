using HefestusApi.Models.Produtos;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Produtos
{
    [Route("api/[controller]")]
    [ApiController]
    public class productGroupsController : ControllerBase
    {
        private readonly DataContext _context;

        public productGroupsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ProductGroup>> GetProductGroups()
        {
            var productGroups = await _context.ProductGroups.ToListAsync();

            return Ok(productGroups);
        }
    }
}
