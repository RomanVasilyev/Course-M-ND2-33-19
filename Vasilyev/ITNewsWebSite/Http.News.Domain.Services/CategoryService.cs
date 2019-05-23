using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Http.News.Data.Contracts;
using Http.News.Domain.Contracts.Dtos;

namespace Http.News.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly INewsRepository _repository;

        public CategoryService(INewsRepository repository)
        {
            //Guard.ArgumentNotNull(repository, "Repository");

            _repository = repository;
        }

        public List<CategorySummaryDto> GetCategoryForMenu()
        {
            return _repository.GetAllCategories().ToList().MapTo<CategorySummaryDto>();
        }
    }
}
