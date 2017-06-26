using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FurnitureStore.Entities
{
    public class Furniture
    {
     
      
        [Key]
        public int FurnitureId { get; set; }

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
      
        public virtual Category Category { get; set; }


        [NotMapped]
        public virtual FurnitureImages Image { get; set; }

        public virtual ICollection<FurnitureImages> Images { get; set; }

        [Display(Name = "Чи присутній товар на складі")]
        public bool IsAvailable { get; set; }
    }
}