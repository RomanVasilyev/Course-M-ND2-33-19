using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary
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

        public void Add(Book book)
        {
            if (!data.Contains(book))
            {
                data.Add(book);
            }
            else
                throw new Exception("Element alredy exists");
        }

        public void Change(int id, string newtitle)
        {
            try
            {
                var book = data.FirstOrDefault(x => x.Id == id);
                book.Title = newtitle;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var book = data.FirstOrDefault(x => x.Id == id);
                data.Remove(book);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}