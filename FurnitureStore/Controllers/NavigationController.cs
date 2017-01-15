using FurnitureStore.Abstract;
using FurnitureStore.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class NavigationController : Controller
    {
        private IFurnitureRepo repo;

        public NavigationController(IFurnitureRepo repo)
        {
            this.repo = repo;
        }

    
        public PartialViewResult Menu(string category)
        {
            ViewBag.div = "<div class=\"menu_block\"></div>";
            IEnumerable<string> categories = repo.Categories
                 .Select(game => game.Name)
                 .Distinct()
                 .OrderBy(x => x);
            return PartialView("Menu",categories);  
        }


        [ChildActionOnly]
        public ActionResult GenreMenu(string category)
        {
            ViewBag.div = "<div class=\"menu_block\">";
            ViewBag.closeDiv = "</div>";
            DataBaseEntity context = new DataBaseEntity();
            var genres = context.Categories.ToList();
            return PartialView(genres);
        }


    }
}