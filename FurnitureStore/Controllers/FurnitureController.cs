using FurnitureStore.Abstract;
using FurnitureStore.Concrete;
using FurnitureStore.Entities;
using FurnitureStore.Models;
using FurnitureStore.ModelView;
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
        public int pageSize = 9;

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
                 .Take(pageSize).ToList() , 
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
            IEnumerable<FurnitureImages> images = furniture.Images;
            FurnitureImages mainImage = images.Where(x => x.IsMainImage).FirstOrDefault();
            IEnumerable<FurnitureImages> secondaryImages = images.Where(x => !x.IsMainImage);

            Category categories = furniture.Category;

            FurnitureVM model = new FurnitureVM
            {
              
                Name = furniture.Name,
                Description = furniture.Description , 
                Manufacturer = furniture.Manufacturer , 
                Price = furniture.Price , 
                Size = furniture.Size , 
                CategoryId = furniture.CategoryId ,

                MainImage = new ImageVM
                {
                    Id = mainImage.Id,
                    Path = mainImage.Path,
                    DisplayName = mainImage.DisplayName,
                    IsMainImage = mainImage.IsMainImage

                },

                Category = new CategoryVM
                {
                    Name = categories.Name
                },

                SecondaryImages = secondaryImages.Select(x => new ImageVM()
                {
                    Id = x.Id,
                    Path = x.Path,
                    DisplayName = x.DisplayName
                }).ToList()
            
        };

            return View(model);
        }


        public PartialViewResult SimilarProducts(string category)
        {
         


            ListViewModel model = new ListViewModel
            {
                Furnitures = repository.Furnitures
               .Where(x => category == null ? true : x.Category.Name.Equals(category))
                .OrderBy(r => Guid.NewGuid()).Take(4) , 

            
            };
            return PartialView(model);
            
        }

        public FileResult GetImage(int categoryId)
        {
            Category category = repository.Categories
                .FirstOrDefault(g => g.CategoryId == categoryId);

            if (category != null)
            {
                return File(category.ImageData, category.ImageMimeType);
            }
            else
            {
                return null;
            }
        }



        public JsonResult Search(string term)
        {
           // var data = context.Furnitures.Where(p => p.Name.StartsWith(term))
               // .Select(p => new { p.FurnitureId, p.Name }).ToList();

            var test = context.FurnitureImages.Where(p => p.IsMainImage == true).
                Join(context.Furnitures.Where(p => p.Name.Contains(term)),
                o => o.FurnitureId, od => od.FurnitureId,
                (o, od) => new
                {
                    od.FurnitureId,
                    od.Name,
                    od.Price,
                    o.Path
                }).Select(

                p => new
                {

                    p.FurnitureId , 
                    p.Price,
                    p.Name , 
                    p.Path,
                    
                })
                .ToList();


            return Json(test, JsonRequestBehavior.AllowGet);
            
        }
     }
}