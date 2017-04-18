using FurnitureStore.Entities;
using FurnitureStore.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{
    public class ListViewModel
    {
      

        public IEnumerable<Furniture> Furnitures { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public InfoPage InfoPages { get; set; }
        public string CurrentCategory { get; set; }

      

    }
}