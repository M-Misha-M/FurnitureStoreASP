using FurnitureStore.Abstract;
using FurnitureStore.Concrete;
using FurnitureStore.Entities;
using FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class FurnitureController : Controller
    {
        DataBaseEntity context = new DataBaseEntity();
        private IFurnitureRepo repository;
        public int pageSize = 5;

        public FurnitureController(IFurnitureRepo repo)
        {
            repository = repo;
        }
        
        
        public ViewResult List(string category, int page = 1)
        {
          
            ListViewModel model = new ListViewModel
             {
                 Furnitures = repository.Furnitures
                 .Where(p => category == null || p.Category.Name== category)
                 .OrderBy(f => f.FurnitureId)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize),
                 InfoPages = new InfoPage
                 {
                     CurrentPage = page,
                     ItemsPerPage = pageSize,
                     TotalItems =  category == null  ?  repository.Furnitures.Count() :
                     repository.Furnitures.Where(furniture => furniture.Category.Name == category).Count()
                 },
                 CurrentCategory = category
             };

            return View(model);
        }






        public ActionResult Details(int id) 
        {
            var furniture = repository.Details(id);

            return View(furniture);
        }


        public PartialViewResult Test2(string category)
        {
         


            ListViewModel model = new ListViewModel
            {
                Furnitures = repository.Furnitures
               .Where(x => category == null ? true : x.Category.Name.Equals(category))
                .OrderBy(r => Guid.NewGuid()).Take(1) , 

            
            };
            return PartialView(model);
            
        }



        public FileResult GetImage(int categoryId)
        {
            Category category = repository.Categories
                .FirstOrDefault(g => g.CategoryId == categoryId);

            if(category != null)
            {
                return File(category.ImageData, category.ImageMimeType);
            }else
            {
                return null;
            }
        }
        

    }
}