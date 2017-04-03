using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FurnitureStore.Entities
{
    public class Category
    {
       

        public string Name { get; set; }
       
        public int CategoryId { get; set; }
     
        public List<Furniture> Furnitures { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

    }
}