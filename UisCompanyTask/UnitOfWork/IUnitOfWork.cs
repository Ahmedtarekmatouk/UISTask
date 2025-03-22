using Microsoft.EntityFrameworkCore;
using UisCompanyTask.Repository;

namespace UisCompanyTask.UnitOfWork
{
    public interface IUnitOfWork<TContext> :IDisposable where TContext : DbContext
    {
        IGenericRepository<T, TContext> Repository<T>() where T : class;
        int Complete();
    }
}
