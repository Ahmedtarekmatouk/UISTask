using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UisCompanyTask.Repository
{
    public interface IGenericRepository<T,Tcontext> where T : class where Tcontext : DbContext
    {
        Task<IEnumerable<TResult>> GetAll<TResult>(int? pageNumber=null, int? pageSize=null, Expression<Func<T, bool>>? filter = null, Expression<Func<T, TResult>>? selection = default, bool asSplitQuery = false, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false, params string[] properties);
        Task<TResult?> Get<TResult>(Expression<Func<T, bool>> filter, bool tracked = false, bool asSplitQuery = false, Expression<Func<T, TResult>>? selection = default, params string[] properties);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
