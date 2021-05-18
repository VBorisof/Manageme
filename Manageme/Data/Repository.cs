using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Manageme.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Manageme.Data.Models
{
    public class Repository<T> : IRepository<T> where T : ModelBase
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }


        public T GetOrDefault(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAsQueryable(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public IEnumerable<T> GetAsEnumerable()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.ToList();
        }

        public Task<List<T>> GetAsEnumerableAsync()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.ToListAsync();
        }

        public T Add(T item)
        {
            return _context.Set<T>().Add(item).Entity;
        }

        public async Task<T> AddAsync(T item)
        {
            return (await _context.Set<T>().AddAsync(item)).Entity;    
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public void Remove(long id)
        {
            _context.Set<T>().Remove(GetOrDefault(id));
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool Any(Func<T, bool>? filter = null)
        {
            var query = _context.Set<T>();
            return filter != null ? query.Any(filter) : query.Any();
        }
        public bool None(Func<T, bool>? filter = null)
        {
            var query = _context.Set<T>();
            return filter != null ? query.None(filter) : query.None();
        }

        public Task<bool> AnyAsync(Func<T, bool>? filter = null)
        {
            var query = _context.Set<T>();
            return filter != null ? query.AnyAsync(x => filter(x)) : query.AnyAsync();
        }
        
        async Task<T> SingleOrDefaultAsync(Func<T, bool> objectFilter)
        {
            return await GetAsQueryable().SingleOrDefaultAsync(o => objectFilter(o));
        }
    }
}
