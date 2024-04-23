using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Infrastructure;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _context;

		// Ask CLR for creating an object from DbContext impilictly
		public GenericRepository(StoreContext context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			if(typeof(T) == typeof(Product))
				return (IReadOnlyList<T>) await _context.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
			return await _context.Set<T>().ToListAsync();  // Using ToList() operator to make the exectution immediately
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			//return await SpecificationsEvaluator<T>.GetQuery(_context.Set<T>() , spec).AsNoTracking().ToListAsync();
			return await ApplySpecifications(spec).AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			if (typeof(T) == typeof(Product))
				return  await _context.Set<Product>().Where(p => p.Id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec)
		{
			//return await SpecificationsEvaluator<T>.GetQuery(_context.Set<T>() , spec).FirstOrDefaultAsync();
			return await ApplySpecifications(spec).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<int> GetCountAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).CountAsync();
		}

		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
		{
			 return SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), spec);
		}	
	}
}
