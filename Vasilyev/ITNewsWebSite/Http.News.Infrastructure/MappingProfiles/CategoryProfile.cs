using AutoMapper;
using Http.News.Data.Contracts.Entities;
using Http.News.Domain.Contracts.Dtos;
using Http.News.Domain.Contracts.ViewModels;

namespace Http.News.Infrastructure.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            MapCategoryToSumDto();
            MapSumDtoToCategory();
            MapItemToItemDetails();
            MatItemDetailsToItem();
        }

        private void MatItemDetailsToItem()
        {
            CreateMap<Item, ItemDetailsViewModel>();
        }

        private void MapItemToItemDetails()
        {
            CreateMap<ItemDetailsViewModel, Item>();
        }

        private void MapCategoryToSumDto()
        {
            CreateMap<Category, CategorySummaryDto>();
        }

        private void MapSumDtoToCategory()
        {
            CreateMap<CategorySummaryDto, Category>();
        }
    }
}