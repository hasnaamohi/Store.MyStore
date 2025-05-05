using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Services.Abstraction;
using Shared;

namespace Persentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManger serviceManger) : ControllerBase
    {
        // sort [name asc (default),name desc,price asc , price desc]
        [HttpGet] //api
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationParameters SpecParams)
        {
            var result = await serviceManger.ProductService.GetAllProductsAsync(SpecParams);
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
        [HttpGet("brands")] //Get:api/products/brands
        public async Task<IActionResult> GetAllBrands()
        {
           var result= await serviceManger.ProductService.GetAllBrandsAsync();
            if (result is null) return NotFound();
            return Ok(result);

        }
        [HttpGet("types")] //Get:api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManger.ProductService.GetAllTypesAsync();
            if (result is null) return NotFound();
            return Ok(result);

        }

    }
}
