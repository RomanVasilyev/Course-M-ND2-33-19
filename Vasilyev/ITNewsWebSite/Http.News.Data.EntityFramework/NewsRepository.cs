using System.Linq;
using Http.News.Data.Contracts;
using Http.News.Data.Contracts.Entities;

namespace Http.News.Data.EntityFramework
{
    public class NewsRepository : INewsRepository
    {
        private readonly INewsDbContext _dbContext;

        public NewsRepository(INewsDbContext dbContext)
        {
            //Guard.ArgumentNotNull(dbContext, "DbContext");

            _dbContext = dbContext;
        }

        public IQueryable<Category> GetAllCategories()
        {
            return _dbContext.Categories;
        }

        public IQueryable<Item> GetAllItems()
        {
            return _dbContext.Items;
        }
    }
}