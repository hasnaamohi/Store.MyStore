using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork ,IMapper mapper) : IProductService
    {
       public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            //get all product through product repositry
           var products=await unitOfWork.GetRepositry<Product, int>().GetAllAsync();

            //mapping IEnumerable<product> to <IEnumerable<ProtectResultDto>>: auto mapper
             var result =mapper.Map<IEnumerable<ProductResultDto>>(products);
             return result;
        }
       public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var product= await unitOfWork.GetRepositry<Product,int>().GetAsync(id);
            if (product is null) { return null; }
             var result =mapper.Map<ProductResultDto>(product);
            return result;

        }

       public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepositry < ProductType, int>().GetAllAsync();
            var result =mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }
       public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepositry<ProductBrand, int>().GetAllAsync();
            var result=mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }


    }
}
