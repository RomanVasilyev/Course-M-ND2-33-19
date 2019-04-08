﻿using System.Collections.Generic;
using System.Linq;
using Http.BooksLibrary.Data.Contracts;

namespace Http.BooksLibrary.Data.EntityFramework
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T Get(int id)
        {
            var dbSet = dbContext.Set<T>();
            var result = dbSet.Find(id);
            return result;
        }

        public IList<T> GetList()
        {
            var dbSet = dbContext.Set<T>();
            var result = dbContext.Books.ToList(); //???
            return (IList<T>) result;
        }

        public void Add(T book)
        {
            var dbSet = dbContext.Set<T>();
            dbSet.Add(book);
        }

        public void Change(T book)
        {
        }

        public void Delete(int id)
        {
            var dbSet = dbContext.Set<T>();
            var book = dbSet.Find(id);
            dbSet.Remove(book);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}