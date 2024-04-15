using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext)
        {
            #region BrandSeeding
            
			if(_dbContext.ProductBrands.Count() == 0)
			{
				// Brands :
				// 1. Read Data from jsaon file
				var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");

				// 2 . Convert json string to ProductBrand type 
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				// 3. Add data to Brand table
				if (brands?.Count() > 0)
				{
					foreach (var brand in brands)
					{
						_dbContext.Set<ProductBrand>().Add(brand);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

            #endregion

            #region CategorySeeding
            
            if(_dbContext.ProductCategories.Count() == 0)
            {
				// Category
				// 1. Read data from json file
				var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");

				// 2. Convert json string to ProductCategory type
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

				// 3. Add data to Category table
				if (categories?.Count() > 0)
				{
					foreach (var category in categories)
					{
						_dbContext.Set<ProductCategory>().Add(category);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

            #endregion

            #region ProductSeeding
            
            if(_dbContext.Products.Count() == 0)
            {
				// Product
				// 1. Read data from json file
				var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");

				// 2. Convert json strings to Product type
				var products = JsonSerializer.Deserialize<List<Product>>(productData);

				// 3. Add data to Product table
				if (products?.Count() > 0)
				{
					foreach (var product in products)
					{
						_dbContext.Set<Product>().Add(product);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

			#endregion

		}
	}
}
