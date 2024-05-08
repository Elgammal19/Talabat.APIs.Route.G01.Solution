using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Product;
using static System.Net.WebRequestMethods;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile : Profile
	{
		//private readonly IConfiguration _configuration;

		public MappingProfile(/*IConfiguration configuration*/)
        {
			//_configuration = configuration;

			CreateMap<Product, ProductToReturnDto>()
					.ForMember(D => D.Brand, O => O.MapFrom(P => P.Brand.Name))
					.ForMember(D => D.Category, O => O.MapFrom(P => P.Category.Name))
					//.ForMember(D => D.PictureUrl , O => O.MapFrom(P => $"{_configuration["BaseApiUrl"]}{P.PictureUrl}"));
					.ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPicturUrlResolver>());

			CreateMap<CustomerBasketDto, CustomerBasket>();

			CreateMap<BasketItemDto, BasketItem>();

			CreateMap<Address, AddressDto>().ReverseMap();

			CreateMap<OrderAddressDto, OrderAddress>();

			CreateMap<Order, OrderToReturnDto>()
				    .ForMember(d => d.DeliveryMethod , o => o.MapFrom(s => s.DeliveryMethod.ShortName))
					.ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

			CreateMap<OrderItem, OrderItemDto>()
					.ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
					.ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
					.ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
					.ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
		}

    }
}
