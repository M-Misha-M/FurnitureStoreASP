using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Entities
{
    public class FurnitureImages
    {
        public int? Id { get; set; }
        public string Path { get; set; }
        public string DisplayName { get; set; }
        public bool IsMainImage { get; set; }
        public int FurnitureId { get; set; }

        public virtual Furniture Furniture { get; set; }
    }
}