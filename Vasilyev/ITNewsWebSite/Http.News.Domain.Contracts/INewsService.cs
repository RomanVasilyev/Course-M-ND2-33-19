using System.Collections.Generic;
using Http.News.Data.Contracts.Entities;
using Http.News.Domain.Contracts.Dtos;
using Http.News.Domain.Contracts.ViewModels;

namespace Http.News.Domain.Services
{
    public interface INewsService
    {
        List<CategorySummaryDto> GetCategoryForMenu();
        Category GetCategoryById(int id);
        Item GetItemById(int id);
        IEnumerable<ItemSummaryDto> GetItemSummaries();
        IEnumerable<ItemSummaryDto> GetHottestItems(int numOfItemOnHomePage);
        IEnumerable<ItemSummaryDto> GetLatestItems(int numOfItemOnHomePage);
        IEnumerable<ItemSummaryDto> GetItemsByCategoryId(int categoryId);
        ItemDetailsDto GetItemDetails(int itemId, int catId);
        HomePageViewModel BuildHomePageViewModel(int p);
        ItemDetailsViewModel BuildItemDetailsViewModel(int categoryId, int itemId);
        CategoryPageViewModel BuildCategoryPageViewModel(int id);
        ItemDetailsViewModel IncrementArticleRating(double rate, int id, int catid);
        void Save(ItemDetailsViewModel viewModel);
        ItemDetailsViewModel IncrementArticleRating(ItemDetailsViewModel viewModel);
    }
}