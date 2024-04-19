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
		public ProductWithBrandAndCategorySpecifications(string? sort) : base()    
        {
                Includes.Add(P => P.Brand);
                Includes.Add(P => P.Category);

            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
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
        }

		// Create an Obj. --> Get Product By Id 
		public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id) // Criteria        
		{
                Includes.Add(P => P.Brand);
                Includes.Add(P => P.Category);
        }
    }
}
