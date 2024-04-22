using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository _basketRepo;

		public BasketController(IBasketRepository basketRepo)
        {
			_basketRepo = basketRepo;
		}

		[HttpGet("{id}")] // Get : --> BaseUrl/api/basket/id
		public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
		{
			var basket = await _basketRepo.GetBasketAsync(basketId);

			return Ok(basket ?? new CustomerBasket(basketId));
		}

		[HttpPost] // Post :   --> BaseUrl/api/basket
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{
			var createdOrUpdatedBasket = await _basketRepo.UpdateBasketAsync(basket);

			//if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));

			return Ok(createdOrUpdatedBasket);
		}

		[HttpDelete]  // Delete :   --> BaseUrl/api/basket?id=
		public async Task DeleteBasket(string basketId)
		{
			await _basketRepo.DeleteBasketAsync(basketId);
		}


	}
}
