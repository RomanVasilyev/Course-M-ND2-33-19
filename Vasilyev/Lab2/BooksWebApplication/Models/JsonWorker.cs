using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Models
{
    public class JsonWorker : IJsonWorker
    {
        public IEnumerable<Book> Load(string path)
        {
            List<Book> books = new List<Book>();
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(books.GetType());
            books = ser.ReadObject(fs) as List<Book>;
            fs.Close();
            return books;
        }

        public void Save(string path, IEnumerable<Book> books)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<Book>));
            ser.WriteObject(fs, books);
            fs.Close();
        }
    }
}
