namespace BookLibrary
{
    public interface IBookService
    {
        Book Get(int id);
        void Add(Book book);
        void Change(int id, string title);
        void Delete(int id);
    }
}