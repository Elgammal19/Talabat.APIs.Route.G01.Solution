using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }   // Basket Id
		public List<BasketItemDto> Items { get; set; }
	}
}
