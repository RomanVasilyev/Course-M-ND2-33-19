using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Http.BooksLibrary.Data.Contracts
{
    public class JsonWorker : IJsonWorker
    {
        public IEnumerable<Book> Load(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<Book> books = (List<Book>)serializer.Deserialize(file, typeof(List<Book>));
                return books;
            }            
        }

        public void Save(string path, IEnumerable<Book> books)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, books);
            }
        }
    }
}
