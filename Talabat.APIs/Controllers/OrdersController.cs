using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
	
	public class OrdersController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController( IOrderService orderService , IMapper mapper)
        {
			_orderService = orderService;
			_mapper = mapper;
		}

		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost] // BaseUrl/api/orders  --> POST :
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{
			var address = _mapper.Map<OrderAddressDto, OrderAddress>(orderDto.ShippingAddress);

			var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

			if (order is null)
				return BadRequest(new ApiResponse(400));

			return Ok(_mapper.Map<Order , OrderToReturnDto>(order));
		}


		[HttpGet] // BaseUrl/api/orders?email="" (Query String)  --> GET :
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string email)
		{
			var orders = await _orderService.GetOrdersForUserAsync(email);

			return Ok(_mapper.Map<IReadOnlyList<Order> , IReadOnlyList<OrderToReturnDto>>(orders));
		}


		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")] // BaseUrl/api/orders/id?email="" (Query String)  --> GET : 
		public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id , string email)
		{
			var order = await _orderService.GetOrderByIdForUserAsync(email , id);

			if (order is null) return NotFound(new ApiResponse(404));

			return Ok(_mapper.Map<Order , OrderToReturnDto>(order));
		}


		[HttpGet("deliveryMethods")] // BaseUrl/api/orders/deliveryMethods --> GET : 
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
		{
			var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

			return Ok(deliveryMethods);
		}

	}
}
