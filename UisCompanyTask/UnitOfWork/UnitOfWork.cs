using Microsoft.EntityFrameworkCore;
using System.Collections;
using UisCompanyTask.Repository;

namespace UisCompanyTask.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext context;
        private Hashtable? _repositories;
        public UnitOfWork(TContext context)
        {
            this.context = context;
        }
        public int Complete()
        {
           return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Repository.IGenericRepository<T, TContext> Repository<T>() where T : class
        {
            if (_repositories is null)
                _repositories = new Hashtable();
            var entityType = typeof(T);
            var contextType = typeof(TContext);
            if (!_repositories.ContainsKey($"{contextType.Name} - {entityType.Name}"))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(entityType, contextType), context);
                _repositories[$"{contextType.Name} - {entityType.Name}"] = repositoryInstance;
            }
            return (IGenericRepository<T, TContext>)_repositories[$"{contextType.Name} - {entityType.Name}"]!;
        }
    }
}
