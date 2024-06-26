﻿using System;
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
		public Expression<Func<T, object>>? OrderBy { get ; set; }
		public Expression<Func<T, object>>? OrderByDesc { get; set; }
		public int Take { get ; set; }
		public int Skip { get; set ; }
		public bool IsPaginationEnabled { get; set; }

		public BaseSpecifications()
        {
            Criteria = null;   //  _context.Set<T>().ToListAsync()
			OrderBy = null;
			OrderByDesc = null;
		}

        public BaseSpecifications(Expression<Func<T,bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;  //  _context.Set<Product>().Where(p => p.Id == id)
		}

		public void AddOrderBy(Expression<Func<T, object>>? orderByExpression)
		{
			OrderBy = orderByExpression;
		}

		public void AddOrderByDesc(Expression<Func<T, object>>? orderByDescExpression) 
		{ 
			OrderByDesc = orderByDescExpression; 
		}

		public void ApplyPagination(int _skip , int _take)
		{
			IsPaginationEnabled = true;
			Skip = _skip ;
			Take = _take ;

		}

    }
}
 