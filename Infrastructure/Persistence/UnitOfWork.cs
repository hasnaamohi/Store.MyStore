using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDBContext _context;

       private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreDBContext context)
        {
            _context = context;
          _repositories = new ConcurrentDictionary<string,object>();
        }
        public IGenericRepositry<TEntity, TKey> GetRepositry<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

          return(IGenericRepositry<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepositry<TEntity, TKey>(_context));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
