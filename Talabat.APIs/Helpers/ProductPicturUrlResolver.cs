using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class ProductPicturUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPicturUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!String.IsNullOrEmpty(source.PictureUrl))
				return $"{_configuration["BaseApiUrl"]}/{source.PictureUrl}";

			return String.Empty;
		}
	}
}
