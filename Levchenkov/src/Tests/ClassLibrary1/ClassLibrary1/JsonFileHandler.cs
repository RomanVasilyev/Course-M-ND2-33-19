using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class JsonFileHandler : IFileHandler
    {
        public IEnumerable<Book> Load()
        {
            throw new NotImplementedException();
        }

        public void Save(List<Book> books)
        {
            throw new NotImplementedException();
        }
    }
}
