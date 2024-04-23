using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
		// Create an Obj. --> Get All Products 
		public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams) : base(P => 

                (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
                (!specParams.BrandId.HasValue    || P.BrandId == specParams.BrandId) &&
                (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId)
        )    
        {
                Includes.Add(P => P.Brand);
                Includes.Add(P => P.Category);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch(specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                            break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            else 
                AddOrderBy(p => p.Name);

			ApplyPagination(specParams.PageSize*(specParams.PageIndex -1) , specParams.PageSize);

		}

		// Create an Obj. --> Get Product By Id 
		public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id) // Criteria        
		{
                Includes.Add(P => P.Brand);
                Includes.Add(P => P.Category);
        }

		
    }
}
