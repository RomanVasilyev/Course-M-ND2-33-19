using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        public Category GetCategoryById(int id)
        {
            return _repository.GetAllCategories().FirstOrDefault(x => x.Id == id);
        }

        public Item GetItemById(int id)
        {
            return _repository.GetAllItems().FirstOrDefault(x => x.Id == id);
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
            itemDetailsViewModel.ItemDetails = this.GetItemDetails(itemId, categoryId);
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
        //public ItemDetailsViewModel IncrementArticleRating(double rate, int id, int catid)
        //{
        //    var itemDetailsViewModel = BuildItemDetailsViewModel(catid, id);
        //    itemDetailsViewModel.ItemDetails.Rating += rate;
        //    itemDetailsViewModel.ItemDetails.TotalRaters += 1;
        //    itemDetailsViewModel.ItemDetails.AverageRating =
        //        itemDetailsViewModel.ItemDetails.Rating / Convert.ToDouble(itemDetailsViewModel.ItemDetails.TotalRaters);
        //    Save(itemDetailsViewModel);
        //    return itemDetailsViewModel;
        //}

        public ItemDetailsDto IncrementArticleRating(double score, int id)
        {
            var itemDetailsViewModel = GetItemDtoById(id);
            itemDetailsViewModel.Rating += score;
            itemDetailsViewModel.TotalRaters += 1;
            itemDetailsViewModel.AverageRating =
                itemDetailsViewModel.Rating / Convert.ToDouble(itemDetailsViewModel.TotalRaters);
            Save(itemDetailsViewModel);
            return itemDetailsViewModel;
        }

        public void Add(ItemDetailsDto viewModel)
        {
            //viewModel.Id = unitOfWork.GetAll<Book>().OrderBy(x => x.Id).Last().Id + 1;
            Item item = new Item();
            Mapper.Map(viewModel, item);
            item.Category = GetCategoryById(viewModel.CategoryId);
            _repository.Add(item);
            _repository.Add(item.ItemContent);
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    _repository.Save();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public void Save(ItemDetailsDto viewModel)
        {
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    Item item = GetItemById(viewModel.Id);
                    //if (book.LongVersion != viewModel.LongVersion)
                    //{
                    //    throw new Exception("Version redact error");
                    //}
                    Mapper.Map(viewModel, item);
                    _repository.Save();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
        }

        public ItemDetailsViewModel IncrementArticleRating(ItemDetailsViewModel vm)
        {

            var item = GetItemDetails(vm.ItemDetails.Id, vm.ItemDetails.CategoryId);
            item.Rating += vm.ItemDetails.Rating;
            item.TotalRaters += 1;
            var itemDetailsViewModel = new ItemDetailsViewModel();
            itemDetailsViewModel.TopMenu = GetCategoryMenu(item.CategoryId);
            itemDetailsViewModel.ItemDetails = item;
            itemDetailsViewModel.ItemDetails.AverageRating =
                item.Rating / Convert.ToDouble(item.TotalRaters);
            Save(itemDetailsViewModel);
            //var category = this.GetCategoryById(item.CategoryId);
            //itemDetailsViewModel.CategoryName = category.Name;
            return itemDetailsViewModel;
        }

        public ItemDetailsDto IncrementLike(int itemId, string userId, bool islike)
        {
            Like like = new Like { DateTime = DateTime.Now, UserId = userId, ItemId = itemId};
            var item = GetItemDtoById(itemId);
            like.IsLike = islike;
            if (islike)
            {
                item.TotalLikes++;
            }
            else
            {
                item.TotalDislikes++;
            }
            _repository.Add(like);
            Save(item);
            return item;
        }

        public void Save(ItemDetailsViewModel viewModel)
        {
            using (var transaction = _repository.BeginTransaction())
            {
                try
                {
                    Item item = GetItemById(viewModel.ItemDetails.Id);
                    //if (book.LongVersion != viewModel.LongVersion)
                    //{
                    //    throw new Exception("Version redact error");
                    //}
                    Mapper.Map(viewModel, item);
                    _repository.Save();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
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

        public IEnumerable<ItemSummaryDto> GetAllItems()
        {
            var queryable = _repository.GetAllItems();
            return ConvertToItemSummaryDtoQuery(
                queryable.OrderByDescending(item => item.CreatedDate));
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

        public ItemDetailsDto GetItemDetails(int itemId, int catId)
        {
            return (from item in _repository.GetAllItems()
                    where item.Id == itemId
                    select new ItemDetailsDto
                    {
                        Id = itemId,
                        CategoryId = catId,
                        Title = item.ItemContent.Title,
                        ShortDescription = item.ItemContent.ShortDescription,
                        Content = item.ItemContent.Content,
                        SmallImageUrl = item.ItemContent.SmallImage,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedDate = item.ModifiedDate,
                        Rating = item.Rating,
                        TotalRaters = item.TotalRaters,
                        AverageRating = item.AverageRating,
                        TotalLikes = item.TotalLikes,
                        TotalDislikes = item.TotalDislikes,
                    }).FirstOrDefault();
        }

        public ItemDetailsDto GetItemDtoById(int itemId)
        {
            var item = _repository.GetItems(x => x.Id == itemId).FirstOrDefault();

            if(item != null)
            {
                return new ItemDetailsDto
                {
                    Id = item.Id,
                    CategoryId = item.Category.Id,
                    Title = item.ItemContent.Title,
                    ShortDescription = item.ItemContent.ShortDescription,
                    Content = item.ItemContent.Content,
                    SmallImageUrl = item.ItemContent.SmallImage,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedDate = item.ModifiedDate,
                    Rating = item.Rating,
                    TotalRaters = item.TotalRaters,
                    AverageRating = item.AverageRating,
                    TotalLikes = item.TotalLikes,
                    TotalDislikes = item.TotalDislikes,
                };
            }

            return null;

            //return _repository.GetItems(x => x.Id == itemId).FirstOrDefault();
            //return (from item in _repository.GetAllItems()
            //        where item.Id == itemId
            //        select new ItemDetailsDto
            //        {
            //            Id = itemId,
            //            CategoryId = catId,
            //            Title = item.ItemContent.Title,
            //            ShortDescription = item.ItemContent.ShortDescription,
            //            Content = item.ItemContent.Content,
            //            SmallImageUrl = item.ItemContent.SmallImage,
            //            CreatedBy = item.CreatedBy,
            //            CreatedDate = item.CreatedDate,
            //            ModifiedBy = item.ModifiedBy,
            //            ModifiedDate = item.ModifiedDate,
            //            Rating = item.Rating,
            //            TotalRaters = item.TotalRaters,
            //            AverageRating = item.AverageRating
            //        }).FirstOrDefault();
        }

        private IEnumerable<ItemSummaryDto> ConvertToItemSummaryDtoQuery(IQueryable<Item> sourceQuery,
            int? numOfItemOnHomePage = null)
        {
            var queryableResult = sourceQuery.Select(
                item => new ItemSummaryDto
                {
                    Id = item.Id,
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
