using FurnitureStore.Abstract;
using FurnitureStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class MenuAdminController : Controller
    {
        IFurnitureRepo repo;


        public MenuAdminController(IFurnitureRepo repo)
        {
            this.repo = repo;
        }


        // GET: MenuAdmin
        public ViewResult Index()
        {
            return View(repo.Categories);
        }


        public ViewResult Edit(int categoryId)
        {
            Category category = repo.Categories
                    .FirstOrDefault(f => f.CategoryId == categoryId);
            return View(category);
               
        }


        [HttpPost]
        public ActionResult Edit(Category category , HttpPostedFileBase image = null)
        {
           
            if(ModelState.IsValid)
            {
                var updateImage = false;
                if (image != null)
                {
                    updateImage = true;
                 
                    category.ImageMimeType = image.ContentType;
                    category.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(category.ImageData, 0, image.ContentLength);
                }
                repo.SaveCategory(category, updateImage);
                TempData["message"] = string.Format("Changes in \"{0}\" has been saved", category.Name);
                return RedirectToAction("Index");
            }else
            {
               
                return View(category);
            }
        }


        public ViewResult Create()
        {
            return View("Edit", new Category());
        }


        [HttpPost]
        public ActionResult Delete(int categoryId)
        {
            Category deleteCategory = repo.DeleteCategory(categoryId);

            if(deleteCategory != null)
            {
                TempData["message"] = string.Format("Розділ менб\"{0}\" був успішно видалений",
                    deleteCategory.Name);
            }
            return RedirectToAction("Index");
        }
    }
}