using System.Linq;
using Http.News.Data.Contracts.Entities;

namespace Http.News.Data.Contracts
{
    public interface INewsRepository
    {
        IQueryable<Category> GetAllCategories();

        IQueryable<Item> GetAllItems();

        IQueryable<Comment> GetAllComments();

        void Add(Item item);

        void Add(ItemContent itemContent);

        void Add(Comment comment);

        void Save();

        ITransaction BeginTransaction();
    }
}