using System;

namespace Http.News.Domain.Contracts
{
    public class DtoBase
    {
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; } 

        public int Raiting { get; set; }
    }
}