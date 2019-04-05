using Http.BooksLibrary.Domain.Contracts.ViewModels;

namespace Http.BooksLibrary.Domain.Contracts
{
    public interface IPostService
    {
        PostViewModel Get(int id);
        void Save(PostViewModel viewModel);
    }
}