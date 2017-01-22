using FurnitureStore.Abstract;
using FurnitureStore.Concrete;
using FurnitureStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class FurnitureManagerController : Controller
    {
        DataBaseEntity db = new DataBaseEntity();
        IFurnitureRepo repo;


        public FurnitureManagerController(IFurnitureRepo repo)
        {
            this.repo = repo;
        }



        // GET: FurnitureManager
        public ActionResult Index()
        {
            return View(repo.Furnitures);
        }


        public ViewResult Edit(int furnitureId)
        {

            Furniture furniture = repo.Furnitures.FirstOrDefault(f => f.FurnitureId == furnitureId);

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
            return View(furniture);
        }


        [HttpPost]
        public ActionResult Edit(Furniture furniture, HttpPostedFileBase image = null)
        {
           
            furniture = new Furniture();
            
            if (ModelState.IsValid)
            {
                var updateImage = false;
                if (image != null)
                {
                    updateImage = true;

                    /*furniture.Images.ImageMimeType = image.ContentType;
                    furniture.Images.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(furniture.Images.ImageData, 0, image.ContentLength);*/
                }

             //   repo.SaveFurniture(furniture,updateImage);
                TempData["message"] = string.Format("Changes in \"{0}\" has been saved", furniture.Name);
              
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
                return View(furniture);
            }
        }


    }
}
