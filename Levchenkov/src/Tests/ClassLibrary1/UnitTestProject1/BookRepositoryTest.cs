using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class BookRepositoryTest
    {
        [TestMethod]
        public void Ctor_NoParameters_LoadFile()
        {
            var fileHandlerMock = new Mock<IFileHandler>();

            var subject = new BookRepository(fileHandlerMock.Object);

            fileHandlerMock.Verify(x => x.Load(), Times.Once);
        }

        [TestMethod]
        public void Get_BookExists_ShouldReturn()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            fileHandlerMock.Setup(x => x.Load()).Returns(new List<Book> {new Book() {Id = 10, Title = "10"}});
            
            var subject = new BookRepository(fileHandlerMock.Object);

            var result = subject.Get(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Get_BookDoesNotExists_ExpectedException()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            fileHandlerMock.Setup(x => x.Load()).Returns(new List<Book> { new Book() { Id = 10, Title = "10" } });

            var subject = new BookRepository(fileHandlerMock.Object);

            var result = subject.Get(100);
        }

        [TestMethod]
        public void Add_AddNotNull_ShouldExist()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            fileHandlerMock.Setup(x => x.Load()).Returns(new List<Book> {  });

            var subject = new BookRepository(fileHandlerMock.Object);

            subject.Add(new Book {Id = 42, Title = "42"});

            var result = subject.Get(42);
            Assert.IsNotNull(result);
            Assert.AreEqual(42, result.Id);
        }

        [TestMethod]
        public void Edit_BookExists_ShouldBeChanged()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            fileHandlerMock.Setup(x => x.Load()).Returns(new List<Book> { });

            var subject = new BookRepository(fileHandlerMock.Object);

            subject.Add(new Book { Id = 42, Title = "42" });

            subject.Edit(new Book { Id = 42, Title = "new 42"});

            var result = subject.Get(42);
            Assert.IsNotNull(result);
            Assert.AreEqual("new 42", result.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Edit_BookDoesNotExist_ExpectedException()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            fileHandlerMock.Setup(x => x.Load()).Returns(new List<Book> { });

            var subject = new BookRepository(fileHandlerMock.Object);

            subject.Edit(new Book { Id = 42, Title = "new 42" });
        }

        [TestMethod]
        public void Delete_BookExists_NoException()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            fileHandlerMock.Setup(x => x.Load()).Returns(new List<Book> { new Book() { Id = 10, Title = "10" } });

            var subject = new BookRepository(fileHandlerMock.Object);
            subject.Delete(10);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Delete_BookDoesNotExist_ExpectedException()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            fileHandlerMock.Setup(x => x.Load()).Returns(new List<Book> { });

            var subject = new BookRepository(fileHandlerMock.Object);
            subject.Delete(10);
        }

        [TestMethod]
        public void SaveChanges_BookAdded_ShouldSaveWithBook()
        {
            var fileHandlerMock = new Mock<IFileHandler>();
            var subject = new BookRepository(fileHandlerMock.Object);
            subject.Add(new Book {Id = 42, Title = "42"});
            subject.SaveChanges();

            fileHandlerMock.Verify(x => x.Save(It.Is<List<Book>>(list => list.Any(y => y.Id == 42))), Times.Once);
        }

        
    }
}
