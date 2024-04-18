using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Infrastructure
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity> spec)
		{
			var query = inputQuery;   //  _context.Set<T>()

			if (spec.Criteria is not null)			 // p => p.Id == id
				query= query.Where(spec.Criteria);  //  _context.Set<Product>().Where(p => p.Id == id)

			// Include Expressions --> 1. P => P.Brand  , 2. P => P.Category

			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => (currentQuery.Include(includeExpression)));

			//  _context.Set<Product>().Where(p => p.Id == id).Include(P => P.Brand)
			//  _context.Set<Product>().Where(p => p.Id == id).Include(P => P.Category)

			return query;
		}
	}
}
