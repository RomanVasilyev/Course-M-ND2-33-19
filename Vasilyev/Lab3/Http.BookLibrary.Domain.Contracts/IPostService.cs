using Http.BookLibrary.Domain.Contracts.ViewModels;

namespace Http.BookLibrary.Domain.Contracts
{
    public interface IPostService
    {
        PostViewModel Get(int id);
        void Save(PostViewModel viewModel);
    }
}