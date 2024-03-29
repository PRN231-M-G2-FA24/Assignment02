using BusinessObject;
using eStoreAPI.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using System.Text.Json.Serialization;

namespace eStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Category>("Categories");
            modelBuilder.EntitySet<Member>("Members");
            modelBuilder.EntitySet<Product>("Products");
            modelBuilder.EntitySet<Order>("Orders");
            modelBuilder.EntitySet<OrderDetail>("OrderDetails");

            builder.Services.AddControllers().AddOData(option =>
                option.Select().OrderBy().Filter().Count().Expand().SetMaxTop(100)
                .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

            builder.Services.AddDbContext<AppDbContext>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("v1/swagger.json", "eStoreAPI");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}