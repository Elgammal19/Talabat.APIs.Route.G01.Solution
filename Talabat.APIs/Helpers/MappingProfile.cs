using AutoMapper;
using Talabat.APIs.DTOs;
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
		}

    }
}
