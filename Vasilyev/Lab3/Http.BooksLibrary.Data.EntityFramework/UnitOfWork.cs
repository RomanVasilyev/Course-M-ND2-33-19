using Autofac;
using Http.BooksLibrary.Data.Contracts;

namespace Http.BooksLibrary.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IComponentContext componentContext;

        public UnitOfWork(ApplicationDbContext dbContext, IComponentContext componentContext)
        {
            this.dbContext = dbContext;
            this.componentContext = componentContext;
        }

        public T Get<T>(int id) where T : class
        {
            var repository = GetRepository<T>();
            var result = repository.Get(id);
            return result;
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var repository = componentContext.Resolve<IRepository<T>>();
            return repository;
        }

        public void Add<T>(T entity) where T : class
        {
            var repository = GetRepository<T>();
            repository.Add(entity);
        }

        public void Delete<T>(int id) where T : class
        {
            var repository = GetRepository<T>();
            repository.Delete(id);
        }

        public void Change<T>(T entity) where T : class
        {
            var repository = GetRepository<T>();
            repository.Change(entity);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public ITransaction BeginTransaction()
        {
            var transaction = new Transaction(dbContext.Database.BeginTransaction());
            return transaction;
        }
    }
}