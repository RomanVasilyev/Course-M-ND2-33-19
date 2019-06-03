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
            MapItemDetailsToItem();
            MapItemDetailsDtoToItem();
            MapItemToItemDetailsDto();
        }

        private void MapItemToItemDetailsDto()
        {
            CreateMap<ItemDetailsDto, Item>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, c => c.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedDate, c => c.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ModifiedBy, c => c.MapFrom(src => src.ModifiedBy))
                .ForMember(dest => dest.ModifiedDate, c => c.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.Rating, c => c.MapFrom(src => src.Rating))
                .ForMember(dest => dest.TotalRaters, c => c.MapFrom(src => src.TotalRaters))
                .ForMember(dest => dest.AverageRating, c => c.MapFrom(src => src.AverageRating))
                .ForAllOtherMembers(c => c.Ignore());
        }

        private void MapItemDetailsDtoToItem()
        {
            CreateMap<Item, ItemDetailsDto>();
        }

        private void MapItemDetailsToItem()
        {
            CreateMap<Item, ItemDetailsViewModel>()
                .ForMember(dest => dest.ItemDetails, c => c.MapFrom(src => src))
                //.ForMember(dest => dest.AverageRating, c => c.MapFrom(src => src.ItemDetails.AverageRating))
                //.ForMember(dest => dest.Rating, c => c.MapFrom(src => src.ItemDetails.Rating))
                //.ForMember(dest => dest.CreatedBy, c => c.MapFrom(src => src.ItemDetails.CreatedBy))
                //.ForMember(dest => dest.CreatedDate, c => c.MapFrom(src => src.ItemDetails.CreatedDate))
                //.ForMember(dest => dest.TotalRaters, c => c.MapFrom(src => src.ItemDetails.TotalRaters))
                //.ForMember(dest => dest.ModifiedBy, c => c.MapFrom(src => src.ItemDetails.ModifiedBy))
                //.ForMember(dest => dest.ModifiedDate, c => c.MapFrom(src => src.ItemDetails.ModifiedDate))
                .ForAllOtherMembers(c => c.Ignore()); ;
        }

        private void MapItemToItemDetails()
        {
            CreateMap<ItemDetailsViewModel, Item>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.ItemDetails.Id))
                .ForMember(dest => dest.AverageRating, c => c.MapFrom(src => src.ItemDetails.AverageRating))
                .ForMember(dest => dest.Rating, c => c.MapFrom(src => src.ItemDetails.Rating))
                .ForMember(dest => dest.CreatedBy, c => c.MapFrom(src => src.ItemDetails.CreatedBy))
                .ForMember(dest => dest.CreatedDate, c => c.MapFrom(src => src.ItemDetails.CreatedDate))
                .ForMember(dest => dest.TotalRaters, c => c.MapFrom(src => src.ItemDetails.TotalRaters))
                .ForMember(dest => dest.ModifiedBy, c => c.MapFrom(src => src.ItemDetails.ModifiedBy))
                .ForMember(dest => dest.ModifiedDate, c => c.MapFrom(src => src.ItemDetails.ModifiedDate))
                .ForAllOtherMembers(c => c.Ignore());
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