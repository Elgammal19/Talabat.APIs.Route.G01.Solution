using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;

		// Inject the dependency of Product class that implement the IGenericRepository interface
		public ProductController(IGenericRepository<Product> productRepo)
        {
			_productRepo = productRepo;
		}

		// 1. GetProducts
		[HttpGet] // BaseUrl/Api/Product --> GET method
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var products = await _productRepo.GetAllAsync();
			////JsonResult result = new JsonResult(products);
			//OkObjectResult result = new OkObjectResult(products);
			return Ok(products); // --> Helper Method 
		}

		// 2. GetProductById
		[HttpGet ("{id}")]
		public async Task<ActionResult<Product>> GetProductById (int id)
		{
			var product = await _productRepo.GetByIdAsync (id);

			if (product is null)
				return NotFound();

			return Ok(product);
		}
	}
}
