using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SomeShop.Middleware;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services;
using SomeShop.Services.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

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


			builder.Services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(x =>
			{
				x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme.",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Name = "Authorization",
				});
				x.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							 Reference = new OpenApiReference
							 {
								 Type = ReferenceType.SecurityScheme,
								 Id = "Bearer"
							 }
						},
						new string[] {}
					}
				});
			});

			builder.Services.AddAuthorization();
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = builder.Configuration["Jwt:Issuer"],
						ValidAudience = builder.Configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]))
					};
				});

			builder.Services.AddHttpLogging( options =>
			{
				options.LoggingFields = HttpLoggingFields.All;
			});

			builder.Host.ConfigureLogging(logging =>
			{
				logging.ClearProviders();
				logging.AddConsole();
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
			app.UseMiddleware<LoggingMiddleware>();

			app.MapControllers();

			app.Run();
		}
	}
}