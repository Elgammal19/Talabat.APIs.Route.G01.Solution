using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                    .ForMember(D => D.Brand, O => O.MapFrom(P => P.Brand.Name))
                    .ForMember(D => D.Category, O => O.MapFrom(P => P.Category.Name));
        }
    }
}
