using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace Http.News.Domain.Contracts.Dtos
{
    public class ItemDetailsDto : DtoBase
    {
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public string Title { get; set; }

        [DisplayName("Short description")]
        public string ShortDescription { get; set; }

        public string Content { get; set; }

        [DisplayName("Upload image")]
        public string SmallImageUrl { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public string MediumImageUrl { get; set; }

        public string BigImageUrl { get; set; }

        public long NumOfView { get; set; }

        public List<SelectListItem> CatList { get; set; }
    }
}