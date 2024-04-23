using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		// GetAll
		// IIReadOnlyList --> retrive data from DB without any filteration or needing to iterating on items
		Task<IReadOnlyList<T>> GetAllAsync();

		// GetById
		Task<T?> GetByIdAsync(int id);

		// GetAllWithSpec
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

		// GetByIdWithSpec
		Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec);

		Task<int> GetCountAsync(ISpecifications<T> spec);
	}
}
