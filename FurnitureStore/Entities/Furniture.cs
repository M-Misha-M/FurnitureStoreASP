using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Entities
{
    public class Furniture
    {
       

        public int FurnitureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string Size { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


          public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}