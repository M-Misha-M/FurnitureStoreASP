using FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Entities
{
    public class Description
    {

      public int Id { get; set; }

        [AllowHtml]
        public string Text { get; set; }
    }
}