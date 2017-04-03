using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{
    public class RegistryModel
    {
        [Required]
        [Display(Name = "UserName")]

        public string UserName { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        public int Year { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "UserRoles")]
        public string UserRoles { get; set; }
    }
}