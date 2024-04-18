namespace Talabat.APIs.Errors
{
	public class ApiExceptionResponse : ApiResponse
	{
        public string? Details { get; set; }

        public ApiExceptionResponse(int status, string? message = null , string? details = null): base(status , message)
        {
                Details = details;
        }

    }
}
