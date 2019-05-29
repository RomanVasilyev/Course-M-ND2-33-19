using System.Collections.Generic;
using Http.News.Domain.Contracts.Dtos;
using Http.News.Domain.Contracts.ViewModels;

namespace Http.News.Domain.Services
{
    public interface INewsService
    {
        List<CategorySummaryDto> GetCategoryForMenu();
        CategorySummaryDto GetCategoryById(int id);
        IEnumerable<ItemSummaryDto> GetItemSummaries();
        IEnumerable<ItemSummaryDto> GetHottestItems(int numOfItemOnHomePage);
        IEnumerable<ItemSummaryDto> GetLatestItems(int numOfItemOnHomePage);
        IEnumerable<ItemSummaryDto> GetItemsByCategoryId(int categoryId);
        ItemDetailsDto GetItemDetails(int itemId);
        HomePageViewModel BuildHomePageViewModel(int p);
        ItemDetailsViewModel BuildItemDetailsViewModel(int categoryId, int itemId);
        CategoryPageViewModel BuildCategoryPageViewModel(int id);
        ItemDetailsViewModel IncrementArticleRating(int rate, int id);
    }
}