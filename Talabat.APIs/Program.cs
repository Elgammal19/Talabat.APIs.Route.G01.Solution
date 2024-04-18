
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configure Services

			// Add services to the container.

			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			///builder.Services.AddEndpointsApiExplorer();
			///builder.Services.AddSwaggerGen();

			builder.Services.AddSwaggerServices();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			///builder.Services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));
			///builder.Services.AddAutoMapper(typeof(MappingProfile));
			///builder.Services.Configure<ApiBehaviorOptions>(options =>
			///{
			///	options.InvalidModelStateResponseFactory = (ActionContext) =>
			///	{
			///		var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
			///											 .SelectMany(P => P.Value.Errors)
			///											 .Select(E => E.ErrorMessage)
			///											 .ToList();
			///
			///		var response = new ApiValidationErrorResponse()
			///		{
			///			Errors = errors
			///		};
			///		return new BadRequestObjectResult(response);
			///	};
			///});

			builder.Services.AddApplicationServices();


			#endregion

			var app = builder.Build();

			#region Update Database

			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _dbContext = services.GetRequiredService<StoreContext>();

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				// Update Database
				await _dbContext.Database.MigrateAsync();

				// Data Seeding
				await StoreContextSeed.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error has been occured during apply the migration");
			}

			#endregion

			#region Configure Kestrel Middlewares

			app.UseMiddleware<ExceptionMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				///app.UseSwagger();
				///app.UseSwaggerUI();

				app.UseSwaggerMiddlewares();
			}

			app.UseStatusCodePagesWithReExecute("/error/{0}");

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers(); 

			app.UseStaticFiles();

			#endregion

			app.Run();
		}
	}
}