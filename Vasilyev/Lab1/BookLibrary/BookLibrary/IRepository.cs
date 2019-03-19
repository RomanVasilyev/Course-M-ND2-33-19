namespace BookLibrary
{
    public interface IRepository<T>
    {
        T Get(int id);
        void Add(T book);
        void Change(int id, string title);
        void Delete(int id);
    }
}