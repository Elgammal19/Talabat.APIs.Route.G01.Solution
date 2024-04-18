
namespace Talabat.APIs.Errors
{
	public class ApiResponse
	{
        public int Status { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int status , string? message = null) 
        {
            Status = status;
            Message = message ?? GetDefaultMessageForStatusCode(status);
        }

		private string? GetDefaultMessageForStatusCode(int status)
		{
			return status switch
			{
				400 => "Bad Request",
				401 => "UnAuthorized" ,
				404 => "Source Not Found",
				500 => "Server Error",
				_ => null
			};
		}
	}
}
