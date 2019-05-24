using AutoMapper;
using Http.News.Data.Contracts.Entities;
using Http.News.Domain.Contracts.Dtos;

namespace Http.News.Infrastructure.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            MapCategoryToSumDto();
            MapSumDtoToCategory();
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