using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FurnitureStore.Concrete;
using FurnitureStore.Entities;
using System.Data.Entity.Infrastructure;

namespace FurnitureStore.Controllers
{
    public class AdminFurnitureController : Controller
    {
        private DataBaseEntity db = new DataBaseEntity();

        // GET: AdminFurniture
        public ActionResult Index()
        {
            var furnitures = db.Furnitures.Include(f => f.Category);
            return View(furnitures.ToList());
        }

        // GET: AdminFurniture/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Furniture furniture = db.Furnitures.Include(s => s.Files).SingleOrDefault(s => s.FurnitureId == id);
            if (furniture == null)
            {
                return HttpNotFound();
            }
            return View(furniture);
        }

        // GET: AdminFurniture/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FurnitureId,Name,Description,Price,Manufacturer,Size,CategoryId")] Furniture furniture, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Image,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        furniture.Files = new List<File> { avatar };
                    }
                    db.Furnitures.Add(furniture);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(furniture);

        }




        // GET: AdminFurniture/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Furniture furniture = db.Furnitures.Include(s => s.Files).SingleOrDefault(s => s.FurnitureId == id);
            if (furniture== null)
            {
                return HttpNotFound();
            }
           
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
            return View(furniture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FurnitureId,Name,Description,Price,Manufacturer,Size,CategoryId")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(furniture).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", furniture.CategoryId);
            return View(furniture);
        }

        // GET: AdminFurniture/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Furniture furniture = await db.Furnitures.FindAsync(id);
            if (furniture == null)
            {
                return HttpNotFound();
            }
            return View(furniture);
        }

        // POST: AdminFurniture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Furniture furniture = await db.Furnitures.FindAsync(id);
            db.Furnitures.Remove(furniture);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
