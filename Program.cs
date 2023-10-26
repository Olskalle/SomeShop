using SomeShop.Middleware;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services;
using SomeShop.Services.Interfaces;

namespace SomeShop
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddScoped<IShopContext, ShopContext>();
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			builder.Services.Scan(scan => 
				scan.FromCallingAssembly()
				.AddClasses()
				.AsMatchingInterface()
				.WithScopedLifetime()
			);


			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

			app.MapControllers();

			app.Run();
		}
	}
}