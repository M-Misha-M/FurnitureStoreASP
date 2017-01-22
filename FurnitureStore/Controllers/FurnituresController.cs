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

namespace FurnitureStore.Controllers
{
    public class FurnituresController : Controller
    {
        private DataBaseEntity db = new DataBaseEntity();

        IFurnitureRepo repo;


        public FurnituresController(IFurnitureRepo repo)
        {
            this.repo = repo;
        }




        // GET: Furnitures
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
            return View("Edit", new Furniture());
        
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FurnitureId,Name,Description,Price,Manufacturer,Size,CategoryId,ImageData,ImageMimeType")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                db.Furnitures.Add(furniture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
            return View(furniture);
        }

        // GET: Furnitures/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
            return View(furniture);
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Furniture furniture , HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                var updateImage = false;
                if (image != null)
                {
                    updateImage = true;
                    furniture.ImageMimeType = image.ContentType;
                    furniture.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(furniture.ImageData, 0, image.ContentLength);
                }

                repo.SaveFurniture(furniture,updateImage);
                TempData["message"] = string.Format("Changes in \"{0}\" has been saved", furniture.Name);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
                return View(furniture);
            }
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



        public FileContentResult GetImageFurniture(int gameId)
        {
            Furniture game = repo.Furnitures
                .FirstOrDefault(g => g.FurnitureId== gameId);

            if (game != null)
            {
                return File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
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
