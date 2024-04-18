using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	[Route("error/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Errors(int code)
		{
			if (code == 401)
				return Unauthorized(new ApiResponse(401));
			else if (code == 404)
				return NotFound(new ApiResponse(404));
			else return StatusCode(code);
		}
	}
}
