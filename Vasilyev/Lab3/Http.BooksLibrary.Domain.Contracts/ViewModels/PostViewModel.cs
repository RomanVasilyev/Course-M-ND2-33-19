using System;
using BooksWebApplication.Models;

namespace Http.BooksLibrary.Domain.Contracts.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime Created { get; set; }

        public Genre Genre { get; set; }

        public bool IsPaper { get; set; }

        public int[] Languages { get; set; }

        public bool DeliveryRequired { get; set; }

        public long LongVersion { get; set; }
    }
}