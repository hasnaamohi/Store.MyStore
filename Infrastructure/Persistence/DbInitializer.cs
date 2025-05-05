using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDBContext _context;

        public DbInitializer(StoreDBContext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {
            try{
                // create data base
                if (_context.Database.GetPendingMigrations().Any())// لو فيه ماجريشن متعملهاش ابلاى
                {
                    // apply to any pending migrations
                    await _context.Database.MigrateAsync();
                }

                // data seeting
                //seeding productType from json file

                if (!_context.ProductTypes.Any())
                {

                    //1-read data from json file as string
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding(jsonFile)\types.json");

                    //2-transform string to c# object[list <product types>]// 
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    //3-add list of product to database
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }

                }

                //seeding productBrand from json file
                if (!_context.ProductBrands.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding(jsonFile)\brands.json");
                    var types = JsonSerializer.Deserialize<List<ProductBrand>>(typesData);
                    if (types is not null && types.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }

                }
                //seeding products from json file
                if (!_context.Products.Any())
                {
                    var datatypes = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding(jsonFile)\products.json");
                    var data = JsonSerializer.Deserialize<List<Product>>(datatypes);
                    if (data is not null && data.Any())
                    {
                        await _context.Products.AddRangeAsync(data);
                        await _context.SaveChangesAsync();
                    }

                }
            }
            catch(Exception)
            {
                throw;

            }
        }
    }
    //D:\route project .net\repos\Store.MyStore\Infrastructure\Persistence\DbInitializer.cs
    //D:\route project .net\repos\Store.MyStore\Infrastructure\Persistence\Data\Seeding(jsonFile)\brands.json

}
