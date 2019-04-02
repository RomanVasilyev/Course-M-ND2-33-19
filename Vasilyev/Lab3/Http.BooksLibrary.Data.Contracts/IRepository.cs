namespace Http.BooksLibrary.Data.Contracts
{
    public interface IRepository<T>
    {
        T Get(int id);
        void Add(T book);
        void Change(T book);
        void Delete(int id);
    }
}