using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Entities
{
    public class FileDetail
    {
        public Guid Id { get; set; }
        public string NameFile { get; set; }
        public string Extension { get; set; }
        public int FurnitureId { get; set; }
        public bool IsMainImage { get; set; }
        public virtual Furniture Furniture { get; set; }
    }
}