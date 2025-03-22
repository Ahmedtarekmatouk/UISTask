using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UisCompanyTask.Repository
{
    public class GenericRepository<T, TContext>(TContext context) : IGenericRepository<T, TContext> where T : class where TContext : DbContext
    {
        public void Add(T entity)
        {
            context.Add(entity);
        }

        public async Task<TResult?> Get<TResult>(Expression<Func<T, bool>> filter, bool tracked = false, bool asSplitQuery = false, Expression<Func<T, TResult>>? selection = null, params string[] properties)
        {
            IQueryable<T> query = context.Set<T>();
            query = query.Where(filter);
            if (asSplitQuery)
                query = query.AsSplitQuery();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            if (tracked)
                query = query.AsTracking();
            var x = query.FirstOrDefault();
            if (typeof(T) != typeof(List<TResult>) && selection is not null)
                return await query.Select(selection).FirstOrDefaultAsync();
            else
            {
                IQueryable<TResult> resultQuery = (IQueryable<TResult>)query;
                return await resultQuery.FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>(int? pageNumber=null, int? pageSize=null, Expression<Func<T, bool>>? filter = null, Expression<Func<T, TResult>>? selection = null, bool asSplitQuery = false, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false, params string[] properties)
        {
            IQueryable<T> query = context.Set<T>();
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            if (properties != null && properties.Any())
            {
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            if (orderBy is not null)
            {
                query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }
            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            if (asSplitQuery)
            {
                query = query.AsSplitQuery();
            }
            if (selection is not null)
            {
                return await query.Select(selection).ToListAsync();
            }
            else
            {
                return await query.Cast<TResult>().ToListAsync();
            }
        }

        public void Remove(T entity)
        {
            context.Remove(entity);
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}
