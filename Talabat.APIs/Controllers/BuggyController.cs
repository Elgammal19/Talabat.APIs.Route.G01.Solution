using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
	public class BuggyController : BaseApiController
	{
		private readonly StoreContext _context;

		public BuggyController(StoreContext context)
		{
			_context = context;
		}

		[HttpGet("NotFound")]
		public ActionResult GetNotFoundError()
		{
			var product = _context.Products.Find(100);
			if (product is null) 
				return NotFound(new ApiResponse(404));
				//return NotFound(new {Message = "Source Not Found" , Status = 404 });

			return Ok(product);
		}

		[HttpGet("ServerError")]
		public ActionResult GetServerError() 
		{
			var product = _context.Products.Find(100);
			var productToReturn = product.ToString();   // Through Exeption [NullReferenceError]
			return Ok(productToReturn);
		}

		[HttpGet("BadRequest")]
		public ActionResult GetBadRquestError()
		{
			return BadRequest( new ApiResponse(400));
		}

		[HttpGet("BadRequest/{id}")]
		public ActionResult GetBadRquestError(int id)
		{
			return Ok();
		}

		[HttpGet("UnAuthorized")]
		public ActionResult GetUnAuthorizedError()
		{
			return Unauthorized(new ApiResponse(401));
		}


	}
}
