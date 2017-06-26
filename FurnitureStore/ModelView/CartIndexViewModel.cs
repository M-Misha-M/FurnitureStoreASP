using FurnitureStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.ModelView
{
    public class CartIndexViewModel
    {

        public CartIndexViewModel()
        {
            this.MainImage = new ImageVM();
        }
       
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public ImageVM MainImage { get; set; }


    }
}