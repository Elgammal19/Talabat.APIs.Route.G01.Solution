using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? Criteria { get ; set; }
		public List<Expression<Func<T, object>>> Includes { get ; set; } =  new List<Expression<Func<T, object>>>();

		public BaseSpecifications()
        {
            Criteria = null;   //  _context.Set<T>().ToListAsync()
		}

        public BaseSpecifications(Expression<Func<T,bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;  //  _context.Set<Product>().Where(p => p.Id == id)
		}
    }
}
