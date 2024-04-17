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
        public ProductWithBrandAndCategorySpecifications() : base()    // Create an Obj. --> Get All Products 
        {
                Includes.Add(P => P.Brand);
                Includes.Add(P => P.Category);
        }
    }
}
