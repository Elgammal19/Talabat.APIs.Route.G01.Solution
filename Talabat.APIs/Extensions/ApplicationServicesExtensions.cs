using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Application.AuthService;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOfWork));

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			//services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfile));
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (ActionContext) =>
				{
					var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
														 .SelectMany(P => P.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToList();

					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			});


			return services;

		}

		public static IServiceCollection AddAuthServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
							.AddJwtBearer(options =>
							{
								options.TokenValidationParameters = new TokenValidationParameters()
								{
									ValidateIssuer = true,
									ValidIssuer = configuration["JWT:ValidIssure"],
									ValidateAudience = true,
									ValidAudience = configuration["JWT:ValidAudience"],
									ValidateIssuerSigningKey = true,
									IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
									ValidateLifetime = true,
									ClockSkew = TimeSpan.Zero
								};
							});


			return services;
		}

	}
}
