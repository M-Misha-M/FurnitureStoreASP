using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FurnitureStore.Concrete;
using FurnitureStore.Entities;
using FurnitureStore.Abstract;
using System.IO;
using FurnitureStore.ModelView;

namespace FurnitureStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class FurnituresController : Controller
    {
        private DataBaseEntity db = new DataBaseEntity();

        IFurnitureRepo repo;


        public FurnituresController(IFurnitureRepo repo)
        {
            this.repo = repo;
        }




       
        public ActionResult Index()
        {
            var furnitures = db.Furnitures.Include(f => f.Category);
            return View(furnitures.ToList());
        }

        // GET: Furnitures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Furniture furniture = db.Furnitures.Find(id);
            if (furniture == null)
            {
                return HttpNotFound();
            }
            return View(furniture);
        }



        // GET: Furnitures/Create
        public ActionResult Create()
        {

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(new FurnitureVM());

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FurnitureVM model)
        {

            if (model.MainFile != null && model.MainFile.ContentLength > 0)
            {
                string displayName = model.MainFile.FileName;
                string extension = Path.GetExtension(displayName);
                string fileName = string.Format("{0}{1}", Guid.NewGuid(), extension);
                string path = "/Upload/" + fileName;
                model.MainFile.SaveAs(Server.MapPath(path));
                model.MainImage = new ImageVM() { Path = path, DisplayName = displayName };
            }

            foreach (HttpPostedFileBase file in model.SecondaryFiles)
            {

                if (file != null && file.ContentLength > 0)
                {
                    string displayName = file.FileName;
                    string extension = Path.GetExtension(displayName);
                    string fileName = string.Format("{0}{1}", Guid.NewGuid(), extension);
                    var path = "/Upload/" + fileName;
                    file.SaveAs(Server.MapPath(path));
                    model.SecondaryImages.Add(new ImageVM { Path = path, DisplayName = displayName });
                }
            }

            //   FurnitureImages main = furniture.Images.Where(x => x.IsMainImage).FirstOrDefault();

            FurnitureImages images = new FurnitureImages();
            Furniture furniture = new Furniture
            {
                Name = model.Name,
                Description = model.Description,
                Size = model.Size,
                Price = model.Price,
                IsAvailable = model.IsAvailable,
                Manufacturer = model.Manufacturer,
                CategoryId = model.CategoryId,                 
            };


            db.Furnitures.Add(furniture);



            if (model.MainImage != null && !model.MainImage.Id.HasValue)
            {
                FurnitureImages image = new FurnitureImages
                {
                    Path = model.MainImage.Path,
                    DisplayName = model.MainImage.DisplayName,
                    IsMainImage = true
                };
                db.FurnitureImages.Add(image);
                db.Entry(furniture).State = EntityState.Added;


            }


            IEnumerable<ImageVM> newImages = model.SecondaryImages.Where(x => x.Id == null);

            foreach (ImageVM image in newImages)
            {

                FurnitureImages imagesSecondary = new FurnitureImages
                {

                    DisplayName = image.DisplayName,
                    Path = image.Path,
                    IsMainImage = false
                };
                db.FurnitureImages.Add(imagesSecondary);

            }




            if (!ModelState.IsValid)
            {
                return View(model);

            }


            db.SaveChanges();
          
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
         return RedirectToAction("Index");
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var furniture = db.Furnitures.Find(id);
            IEnumerable<FurnitureImages> images = furniture.Images;
            if (furniture == null)
            {
                return HttpNotFound();
            }
            FurnitureImages mainImage = images.Where(x => x.IsMainImage).FirstOrDefault();
            IEnumerable<FurnitureImages> secondaryImages = images.Where(x => !x.IsMainImage);

            FurnitureVM model = new FurnitureVM()
            {
                Name = furniture.Name,
                Description = furniture.Description,
                Price = furniture.Price,
                Size = furniture.Size,
                IsAvailable = furniture.IsAvailable,
                CategoryId = furniture.CategoryId,
                Manufacturer = furniture.Manufacturer,

                MainImage = new ImageVM
                {
                    Id = mainImage.Id , 
                    Path = mainImage.Path,
                    DisplayName = mainImage.DisplayName,
                    IsMainImage = mainImage.IsMainImage
                    
                },
               

                SecondaryImages = secondaryImages.Select(x => new ImageVM()
                {
                    Id = x.Id,
                    Path  = x.Path , 
                    DisplayName = x.DisplayName
                }).ToList()
                };
                       
                           
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FurnitureVM model)
        {
            if (model.MainFile != null && model.MainFile.ContentLength > 0)
            {
                string displayName = model.MainFile.FileName;
                string extension = Path.GetExtension(displayName);
                string fileName = string.Format("{0}{1}", Guid.NewGuid(), extension);
                string path = "/Upload/" + fileName;
                model.MainFile.SaveAs(Server.MapPath( path));
                model.MainImage = new ImageVM() { Path = path, DisplayName = displayName };
            }

          
           foreach (HttpPostedFileBase file in model.SecondaryFiles)
            {
               
                if (file != null && file.ContentLength > 0)
                {
                    string displayName = file.FileName;
                    string extension = Path.GetExtension(displayName);
                    string fileName = string.Format("{0}{1}", Guid.NewGuid(), extension);
                    var path = "/Upload/" + fileName;
                    file.SaveAs(Server.MapPath(path));
                    model.SecondaryImages.Add(new ImageVM { Path = path, DisplayName = displayName });
                }
            }

            if (!ModelState.IsValid)
            {
                model.CategoryList = new SelectList(db.Categories, "CategoryId", "Name"); // repopulate the SelectList
                return View(model);
            }

            Furniture furniture = db.Furnitures.Where(x => x.FurnitureId == model.ID).FirstOrDefault();
            FurnitureImages main = furniture.Images.Where(x => x.IsMainImage).FirstOrDefault();
            furniture.Name = model.Name;
            furniture.Description = model.Description;
            furniture.Manufacturer = model.Manufacturer;
            furniture.Price = model.Price;
            furniture.IsAvailable = model.IsAvailable;
            furniture.CategoryId = model.CategoryId;
            furniture.Size = model.Size;       
              main.DisplayName = model.MainImage.DisplayName;
              main.Path = model.MainImage.Path;
              main.IsMainImage = model.MainImage.IsMainImage;
            

          

         

            if (model.MainImage != null && !model.MainImage.Id.HasValue)
            {
                FurnitureImages image = new FurnitureImages
                {
                    Path = model.MainImage.Path,
                    DisplayName = model.MainImage.DisplayName,
                    IsMainImage = true
                };
                furniture.Images.Add(image);
                db.Entry(furniture).State = EntityState.Modified;
                


            }

            // Update secondary images
            IEnumerable<ImageVM> newImages = model.SecondaryImages.Where(x => x.Id == null);

            foreach (ImageVM image in newImages)
            {

                FurnitureImages images = new FurnitureImages
                {

                    DisplayName = image.DisplayName,
                    Path =  image.Path , 
                    IsMainImage = false
                };
                furniture.Images.Add(images);
                
            }
             ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);


            db.Entry(furniture).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = string.Format("Зміни в товарі \"{0}\" були збережені", model.Name);
            return RedirectToAction("Index");


        }
    
            
       


        [HttpPost]
        public ActionResult Delete(int furntitureId)
        {
           Furniture deleteFurniture = repo.DeleteFurniture(furntitureId);

            if (deleteFurniture != null)
            {
                TempData["message"] = string.Format("Товар\"{0}\" був успішно видалений",
                    deleteFurniture.Name);
            }
            return RedirectToAction("Index");
        }





        [HttpPost]
        public JsonResult DeleteFile(int Id)
        {

            ImageVM vm = new ImageVM();
           
            try
            {
                vm.Id = Id;
               
                FurnitureImages furnitureImages = db.FurnitureImages.Find(Id);
                if (furnitureImages == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FurnitureImages.Remove(furnitureImages);
                db.SaveChanges();
               
                //Delete file from the file system
                var path = Server.MapPath(furnitureImages.Path);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!ERROR!!!!" +  ex.Message);
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
