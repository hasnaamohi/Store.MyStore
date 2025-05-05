using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Domain.Models;
using Shared;

namespace Services.Abstraction
{
    public interface IProductService
    {
        // get all product
        Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters SpecParams);
        //get protect by
        Task<ProductResultDto?> GetProductByIdAsync(int id);
        //get all types
        Task <IEnumerable<TypeResultDto>> GetAllTypesAsync();

        //get all brands
        Task <IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        
    }
}
