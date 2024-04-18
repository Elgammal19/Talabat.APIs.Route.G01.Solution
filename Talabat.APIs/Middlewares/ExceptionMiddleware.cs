using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
	// Middleware By Convension
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IWebHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger ,IWebHostEnvironment env)
        {
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try 
			{
				// Take an action with request

				await _next.Invoke(httpContext); // --> Go to next middleware

				// Take an action with request
			}
			catch (Exception e) 
			{
				// 1. Log Error
				_logger.LogError(e.Message);

				// 2. StatusCode
				httpContext.Response.StatusCode = 500;

				// 3. ContentType 
				httpContext.Response.ContentType = "application/json";

				// 4. Check enviroment
				var response = _env.IsDevelopment() ? new ApiExceptionResponse(500, e.Message, e.StackTrace) : new ApiExceptionResponse(500);

				// 5. Convert To Json with camelCase Naminng
				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var responseJson = JsonSerializer.Serialize(response , options);

				await httpContext.Response.WriteAsync(responseJson);

			}
		}
    }
}
