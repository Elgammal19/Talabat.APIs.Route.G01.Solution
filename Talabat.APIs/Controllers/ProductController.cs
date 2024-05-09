using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
    public class ProductController : BaseApiController
	{
		private readonly IProductService _productService;

		///private readonly IGenericRepository<Product> _productRepo;
		///private readonly IGenericRepository<ProductBrand> _brandRepo;
		///private readonly IGenericRepository<ProductCategory> _categoryRepo;
		private readonly IMapper _mapper;

		// Inject the dependency of Product class that implement the IGenericRepository interface
		public ProductController(IProductService productService,
								 ///IGenericRepository<Product> productRepo ,
								 ///IGenericRepository<ProductBrand> brandRepo , 
								 ///IGenericRepository<ProductCategory> categoryRepo,
								 IMapper mapper )
        {
			_productService = productService;
			///_productRepo = productRepo;
			///_brandRepo = brandRepo;
			///_categoryRepo = categoryRepo;
			_mapper = mapper;
		}


		// 1. GetProducts
		[Authorize/*(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)*/]
		[HttpGet]			// BaseUrl/api/Product --> GET method
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{
			var products = await _productService.GetProductsAsync(specParams);

			var count = await _productService.GetCountAsync(specParams);

			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products); // --> Helper Method

			return Ok(new Pagination<ProductToReturnDto>(specParams.PageSize , specParams.PageIndex , count , data));  
		}


		// 2. GetProductById
		[HttpGet ("{id}")]     // BaseUrl/api/Product/id --> GET method
		[ProducesResponseType(typeof(ProductToReturnDto), 200)]
		[ProducesResponseType(typeof(ApiResponse) ,404 /*StatusCodes.Status404NotFound*/)]
		public async Task<ActionResult<ProductToReturnDto>> GetProductById (int id)
		{
			var product = await _productService.GetProductAsync(id);

			if (product is null)
				return NotFound(new ApiResponse(404));

			return Ok(_mapper.Map<Product , ProductToReturnDto>(product));
		}


		[HttpGet("brands")]     // BaseUrl/api/Product/brands
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrandsAsync()
		{
			// We are n't using specification here because there 's no navigational properties needed in Brand model
			var brands = await _productService.GetBrandsAsync(); 

			return Ok(brands);
		}


		[HttpGet("categories")]  //  BaseUrl/api/Product/categories
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategoriesAsync()
		{
			// We are n't using specification here because there 's no navigational properties needed in Brand model
			var categories = await _productService.GetCategoriesAsync();

			return Ok(categories);
		} 
	}
}
