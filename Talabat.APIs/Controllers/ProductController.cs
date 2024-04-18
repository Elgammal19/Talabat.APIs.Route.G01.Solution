using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IMapper _mapper;

		// Inject the dependency of Product class that implement the IGenericRepository interface
		public ProductController(IGenericRepository<Product> productRepo , IMapper	mapper)
        {
			_productRepo = productRepo;
			_mapper = mapper;
		}

		// 1. GetProducts
		[HttpGet] // BaseUrl/Api/Product --> GET method
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();

			var products = await _productRepo.GetAllWithSpecAsync(spec);

			////JsonResult result = new JsonResult(products);
			//OkObjectResult result = new OkObjectResult(products);

			return Ok(_mapper.Map<IEnumerable<Product> , IEnumerable<ProductToReturnDto>>(products)); // --> Helper Method 
		}

		// 2. GetProductById
		[HttpGet ("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDto), 200)]
		[ProducesResponseType(typeof(ApiResponse) ,404 /*StatusCodes.Status404NotFound*/)]
		public async Task<ActionResult<ProductToReturnDto>> GetProductById (int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);

			var product = await _productRepo.GetByIdWithSpecAsync (spec);

			if (product is null)
				return NotFound(new ApiResponse(404));

			return Ok(_mapper.Map<Product , ProductToReturnDto>(product));
		}
	}
}
