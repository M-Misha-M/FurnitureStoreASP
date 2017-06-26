using FurnitureStore.Concrete;
using FurnitureStore.Entities;
using FurnitureStore.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class CartController : Controller
    {
        private DataBaseEntity de = new DataBaseEntity();





        public ViewResult Index(Cart cart, string returnUrl)
        {
            /*
            var furniture = de.FurnitureImages.Find(id);
            IEnumerable<FurnitureImages> images = furniture.Images;
            FurnitureImages mainImage = images.Where(x => x.IsMainImage).FirstOrDefault();
            */
            Cart cart1 = GetCart();

            return View(new CartIndexViewModel
            {
                Cart = GetCart(), 
                ReturnUrl = returnUrl , 

              
            });
        }


       


        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl, int quantity = 0)
        {
            Furniture furniture = de.Furnitures.FirstOrDefault(f => f.FurnitureId == Id);


            CartLine line = cart.Lines.FirstOrDefault(l => l.Furniture.FurnitureId == furniture.FurnitureId);

            int q = quantity == 0 ? (line == null ? 1 : ++line.Quantity) : quantity;

            if (furniture != null)
            {
                GetCart().AddItem(furniture, q);
               
            }

            return RedirectToAction("Index", new { returnUrl });

        }


        public RedirectToRouteResult RemoveFromCart(int Id, string returnUrl)
        {
            Furniture pr = de.Furnitures.FirstOrDefault(g => g.FurnitureId == Id);

            if (pr != null)
            {
                GetCart().RemoveLine(pr);
            }
            return RedirectToAction("Index", new { returnUrl });
        }



        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }


        public PartialViewResult CartSummary(Cart cart)
        {
            cart = GetCart();
            return PartialView(cart);
        }
    }
}