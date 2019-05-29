using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Http.News.Data.Contracts;
using Http.News.Data.Contracts.Entities;
using Http.News.Domain.Contracts.Dtos;
using Http.News.Domain.Contracts.ViewModels;

namespace Http.News.Domain.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            //Guard.ArgumentNotNull(repository, "Repository");

            _repository = repository;
        }

        public List<CategorySummaryDto> GetCategoryForMenu()
        {
            return _repository.GetAllCategories().ToList().MapTo<CategorySummaryDto>();
        }

        public CategorySummaryDto GetCategoryById(int id)
        {
            return _repository.GetAllCategories().FirstOrDefault(x => x.Id == id).MapTo<CategorySummaryDto>();
        }

        public ItemSummaryDto GetItemById(int id)
        {
            return _repository.GetAllItems().FirstOrDefault(x => x.Id == id).MapTo<ItemSummaryDto>();
        }

        /*TODO : Реализовать все методы "типо" UnitOfWork здесь и убрать все что касается UnitOfWork. Вместо этого реализовать работу непосредственно с сервисами в контроллере*/
        #region Base methods

        public HomePageViewModel BuildHomePageViewModel(int numOfItemOnHomePage)
        {
            var homePageViewModel = new HomePageViewModel();
            homePageViewModel.TopMenu = this.GetCategoryMenu(0);
            homePageViewModel.HottestItems = this.GetHottestItems(numOfItemOnHomePage).ToList();
            homePageViewModel.LatestItems = this.GetLatestItems(numOfItemOnHomePage).ToList();

            return homePageViewModel;
        }

        public CategoryPageViewModel BuildCategoryPageViewModel(int id)
        {
            var categoryPageViewModel = new CategoryPageViewModel();
            categoryPageViewModel.CategoryId = id;
            categoryPageViewModel.TopMenu = GetCategoryMenu(id);
            categoryPageViewModel.ItemsConverting(this.GetItemsByCategoryId(id).ToList());

            return categoryPageViewModel;
        }

        public ItemDetailsViewModel BuildItemDetailsViewModel(int categoryId, int itemId)
        {
            var itemDetailsViewModel = new ItemDetailsViewModel();
            itemDetailsViewModel.TopMenu = GetCategoryMenu(categoryId);
            itemDetailsViewModel.ItemDetails = this.GetItemDetails(itemId);
            var category = this.GetCategoryById(categoryId);
            itemDetailsViewModel.CategoryName = category.Name;
            return itemDetailsViewModel;
        }

        public CategoryMenuViewModel GetCategoryMenu(int id)
        {
            var viewModel = new CategoryMenuViewModel();
            var categories = this.GetAllCategorySummaries();
            var realCategories = new List<CategorySummaryDto>();
            realCategories.Add(new CategorySummaryDto
            {
                Id = 0,
                Name = "Home",
                IsCurrentPage = id == 0
            });

            foreach (var categorySummaryDto in categories)
            {
                if (categorySummaryDto.Id == id)
                    categorySummaryDto.IsCurrentPage = true;
                realCategories.Add(categorySummaryDto);
            }

            viewModel.Categories = realCategories;

            return viewModel;
        }

        /*TODO : Реализовать все методы "типо" UnitOfWork здесь и убрать все что касается UnitOfWork. Вместо этого реализовать работу непосредственно с сервисами в контроллере*/
        public ItemDetailsViewModel IncrementArticleRating(int rate, int id)
        {
            var item = GetItemById(id);
            item.Rating += rate;
            item.TotalRaters += 1;
            _repository.Save();
            var itemDetailsViewModel = new ItemDetailsViewModel();
            itemDetailsViewModel.TopMenu = GetCategoryMenu(item.CategoryId);
            itemDetailsViewModel.ItemDetails = this.GetItemDetails(id);
            itemDetailsViewModel.ItemDetails.AverageRating =
                Convert.ToDouble(item.Rating) / Convert.ToDouble(item.TotalRaters);
            var category = this.GetCategoryById(item.CategoryId);
            itemDetailsViewModel.CategoryName = category.Name;
            return itemDetailsViewModel;
            //var detailsViewModel = new ItemDetailsViewModel()
            //{
            //    ItemDetails = new ItemDetailsDto
            //    {
            //        Id = item.Id,
                    
            //        Rating = item.Rating,
            //        TotalRaters = item.TotalRaters,
            //        AverageRating = Convert.ToDouble(item.Rating) / Convert.ToDouble(item.TotalRaters)
            //    },
            //    CategoryName = item.Category.Name,
            //    TopMenu = GetCategoryMenu(id)
            //};
            //return detailsViewModel;
        }

        private IEnumerable<CategorySummaryDto> GetAllCategorySummaries()
        {
            return this.GetCategoryForMenu();
        }

        #endregion

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
                        SmallImageUrl = item.ItemContent.SmallImage,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedDate = item.ModifiedDate,
                        Rating = item.Rating,
                        TotalRaters = item.TotalRaters,
                        AverageRating = item.AverageRating
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
