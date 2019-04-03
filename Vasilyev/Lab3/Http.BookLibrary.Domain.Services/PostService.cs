using System;
using AutoMapper;
using Http.BookLibrary.Domain.Contracts;
using Http.BookLibrary.Domain.Contracts.ViewModels;
using Http.BooksLibrary.Data.Contracts;
using Http.BooksLibrary.Data.Contracts.Entities;

namespace Http.BookLibrary.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public PostViewModel Get(int id)
        {
            Book book = unitOfWork.Get<Book>(id);
            var result = Mapper.Map<PostViewModel>(book);
            return result;
        }

        public void Save(PostViewModel viewModel)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                try
                {
                    Book book = unitOfWork.Get<Book>(viewModel.Id);
                    if (true) // TODO: check version of post
                    {
                        
                    }

                    Mapper.Map(viewModel, book);
                    unitOfWork.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}