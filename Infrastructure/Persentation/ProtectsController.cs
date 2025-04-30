using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Services.Abstraction;

namespace Persentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProtectsController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpGet] //api
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await serviceManger.ProductService.GetAllProductsAsync();
            if (result is null) return BadRequest();
            return Ok(result);

        }
        [HttpGet("{id}" )]
        public async Task <IActionResult> GetProductById(int id)
        {
            var result = await serviceManger.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
