using SomeManufacturer.Services;
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

			builder.Services.AddScoped<IOrderItemService, OrderItemService>()
				.AddScoped<ICategoryService, CategoryService>()
				.AddScoped<IClientService, ClientService>()
				.AddScoped<IEmployeeService, EmployeeService>()
				.AddScoped<IManufacturerService, ManufacturerService>()
				.AddScoped<IOrderService, OrderService>()
				.AddScoped<IOrderItemService, OrderItemService>()
				.AddScoped<IOrderStatusService, OrderStatusService>()
				.AddScoped<IPaymentService, PaymentService>()
				.AddScoped<IPaymentProviderService, PaymentProviderService>()
				.AddScoped<IPaymentStatusService, PaymentStatusService>()
				.AddScoped<IProductService, ProductService>()
				.AddScoped<IShopService, ShopService>()
				.AddScoped<IShoppingSessionService, ShoppingSessionService>()
				.AddScoped<IShopStorageService, ShopStorageService>();


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


			app.MapControllers();

			app.Run();
		}
	}
}