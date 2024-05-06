using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core
{
	public interface IUnitOfWork :IAsyncDisposable
	{
		// Class that responsable for working with DB through DbContext

		// Property Signature For Each Repository
		///	public IGenericRepository<Product> ProductsRepo { get; set; }
		///	public IGenericRepository<ProductBrand> BrandsRepo { get; set; }
		///	public IGenericRepository<ProductCategory> CategoriesRepo { get; set; }
		///	public IGenericRepository<DeliveryMethod> DeliveryMethodsRepo { get; set; }
		///	public IGenericRepository<OrderItem> OrderItemsRepo { get; set; }
		///	public IGenericRepository<Order> OrdersRepo { get; set; }
		///	

		// Signature for method that return GenericRepository<TEntity>
		// To apply open for extensions & close for modifications principle
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

		Task<int> CompleteAsync();


	}
}
