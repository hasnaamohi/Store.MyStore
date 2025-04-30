
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstraction;
using AssemblyManger = Services.AssemblyReference;

namespace Store.MyStore.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddDbContext<StoreDBContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

                });
            //1
           
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();//allow DI (Depantancy Injection)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyManger).Assembly);
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            var app = builder.Build();

            #region Seeding
            //2
            using var scope = app.Services.CreateScope();
            //3
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // ask clr create object
                                                                                            //4
            await dbInitializer.InitializeAsync(); 
            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
