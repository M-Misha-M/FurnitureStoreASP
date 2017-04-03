using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.ModelView
{
    public class ImageVM
    {

        public int? Id { get; set; }
        public string Path { get; set; }
        public string DisplayName { get; set; }
        public bool IsMainImage { get; set; }



    }
}