using FurnitureStore.Entities;
using FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.ModelView
{
    public class FurnitureVM
    {
        public FurnitureVM()
        {
            this.MainImage = new ImageVM();
            this.SecondaryImages = new List<ImageVM>();
            this.Category = new CategoryVM();
            this.furnitureImages = new FurnitureImages();
         
        }


        public int? ID { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Будь ласка, введіть назву товару")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Опис")]
        [Required(ErrorMessage = "Будь ласка, введіть опис для даного товару")]
        public string Description { get; set; }
        [Display(Name = "Ціна (грн)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введіть додаткове значення для цього поля")]
        public decimal Price { get; set; }

        [Display(Name = "Виробник")]
        [Required(ErrorMessage = "Будь ласка, введіть назву виробника для даного товару")]
        public string Manufacturer { get; set; }
        [Display(Name = "Розмір")]
        [Required(ErrorMessage = "Будь ласка, введіть розмір для даного товару")]
        public string Size { get; set; }

        [Display(Name = "Категорія")]
        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public HttpPostedFileBase MainFile { get; set; }
        public IEnumerable<HttpPostedFileBase> SecondaryFiles { get; set; }
        public ImageVM MainImage { get; set; }
        public List<ImageVM> SecondaryImages { get; set; }
        public CategoryVM Category { get; set; }


        public IEnumerable<Furniture> Furnitures { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public FurnitureImages furnitureImages { get; set; }
     
        public string CurrentCategory { get; set; }
        [Display(Name = "Чи присутній товар на складі")]
        public bool IsAvailable { get; set; }

    }
}