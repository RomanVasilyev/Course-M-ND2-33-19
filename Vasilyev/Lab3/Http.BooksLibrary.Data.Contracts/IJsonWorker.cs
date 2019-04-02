using System.Collections.Generic;

namespace Http.BooksLibrary.Data.Contracts
{
    public interface IJsonWorker
    {
        IEnumerable<Book> Load(string path);
        void Save(string path, IEnumerable<Book> books);
    }
}
