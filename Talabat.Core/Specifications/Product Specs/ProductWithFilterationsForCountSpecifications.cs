using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithFilterationsForCountSpecifications : BaseSpecifications<Product>
	{
		public ProductWithFilterationsForCountSpecifications(ProductSpecParams specParams) : base(P =>
				(!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId) &&
				(!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId)
		)
		{
		}
	}
}
