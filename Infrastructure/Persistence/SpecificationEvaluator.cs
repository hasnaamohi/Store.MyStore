using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static  IQueryable<TEntity> GetQuary<TEntity,TKey>
            (IQueryable<TEntity>inputQuery,
            ISpecifications<TEntity,TKey> spec
            )
            where TEntity : BaseEntity<TKey>
        { 
            var query = inputQuery;
            if(spec.Criteria is not  null)
                query = query.Where(spec.Criteria);
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query=query.OrderByDescending(spec.OrderByDescending);
            if (spec.IsPagination)
                query=query.Skip(spec.Skip).Take(spec.Take);

                query = spec.IncludeExpression.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));


               return query;
        
         
        }
    }
}
