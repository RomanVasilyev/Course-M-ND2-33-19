namespace BookLibrary
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> repository;

        public BookService(IRepository<Book> repository)
        {
            this.repository = repository;
        }

        public Book Get(int id)
        {
            return repository.Get(id);
        }

        public void Add(Book book)
        {
            repository.Add(book);
        }

        public void Change(int id, string title)
        {
            repository.Change(id, title);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }        
    }
}