using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
    public class BookRepository : IRepository<Book>
    {
        private readonly IList<Book> data;

        public BookRepository()
        {
            data = new List<Book>
            {
                new Book { Id = 1, Title = "Title1" },
                new Book { Id = 2, Title = "Title2" },
                new Book { Id = 3, Title = "Title3" },
            };
        }

        public Book Get(int id)
        {
            var result = data.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                return result;
            }

            throw new Exception("Element not found");
        }
    }
}