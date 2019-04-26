﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Http.BooksLibrary.Data.Contracts.Entities;

namespace Http.BooksLibrary.Domain.Contracts.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime Created { get; set; }

        public Genre Genre { get; set; }

        public bool IsPaper { get; set; }

        public int[] Languages { get; set; }

        [Display(Name = "Delivery required")]
        public bool DeliveryRequired { get; set; }

        public long LongVersion { get; set; }

        public List<SelectListItem> AvailableLanguages { get; set; }

        public List<SelectListItem> AvailableGenres { get; set; }
    }
}