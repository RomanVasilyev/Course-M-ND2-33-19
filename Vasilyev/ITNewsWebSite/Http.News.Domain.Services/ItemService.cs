using System.Collections.Generic;
using System.Linq;
using Http.News.Data.Contracts;
using Http.News.Data.Contracts.Entities;
using Http.News.Domain.Contracts.Dtos;

namespace Http.News.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly INewsRepository _repository;

        public ItemService(INewsRepository repository)
        {
            //Guard.ArgumentNotNull(repository, "Repository");

            _repository = repository;
        }

        public IEnumerable<ItemSummaryDto> GetItemSummaries()
        {
            var queryable = _repository.GetAllItems();
            return ConvertToItemSummaryDtoQuery(queryable);
        }

        public IEnumerable<ItemSummaryDto> GetHottestItems(int numOfItemOnHomePage)
        {
            var queryable = _repository.GetAllItems();
            return ConvertToItemSummaryDtoQuery(
                queryable.OrderByDescending(item => item.CreatedDate),
                numOfItemOnHomePage);
        }

        public IEnumerable<ItemSummaryDto> GetLatestItems(int numOfItemOnHomePage)
        {
            var queryable = _repository.GetAllItems();

            return ConvertToItemSummaryDtoQuery(
                queryable.OrderByDescending(item => item.ItemContent.NumOfView),
                numOfItemOnHomePage);
        }

        public IEnumerable<ItemSummaryDto> GetItemsByCategoryId(int categoryId)
        {
            var queryable = _repository.GetAllItems();
            return ConvertToItemSummaryDtoQuery(
                    queryable.OrderByDescending(item => item.CreatedDate))
                .Where(item => item.CategoryId == categoryId);
        }

        public ItemDetailsDto GetItemDetails(int itemId)
        {
            return (from item in _repository.GetAllItems()
                where item.Id == itemId
                select new ItemDetailsDto
                {
                    Title = item.ItemContent.Title,
                    Content = item.ItemContent.Content,
                    SmallImageUrl = item.ItemContent.SmallImage
                }).FirstOrDefault();
        }

        private IEnumerable<ItemSummaryDto> ConvertToItemSummaryDtoQuery(IQueryable<Item> sourceQuery,
            int? numOfItemOnHomePage = null)
        {
            var queryableResult = sourceQuery.Select(
                item => new ItemSummaryDto
                {
                    CategoryId = item.Category.Id,
                    CategoryName = item.Category.Name,
                    ItemId = item.Id,
                    Title = item.ItemContent.Title,
                    AvatarImage = item.ItemContent.SmallImage,
                    ShortDescription = item.ItemContent.ShortDescription,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedDate = item.ModifiedDate
                });

            if (numOfItemOnHomePage.HasValue) queryableResult = queryableResult.Take(numOfItemOnHomePage.Value);

            return queryableResult;
        }
    }
}