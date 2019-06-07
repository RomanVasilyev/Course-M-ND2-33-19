using System.Linq;
using Http.News.Data.Contracts.Entities;

namespace Http.News.Data.Contracts
{
    public interface INewsRepository
    {
        IQueryable<Category> GetAllCategories();

        IQueryable<Item> GetAllItems();

        void Add(Item item);

        void Add(ItemContent itemContent);

        void Save();

        ITransaction BeginTransaction();
    }
}