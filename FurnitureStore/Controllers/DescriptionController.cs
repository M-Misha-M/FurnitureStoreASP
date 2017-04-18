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

namespace FurnitureStore.Controllers
{
    public class DescriptionController : Controller
    {
        private DataBaseEntity db = new DataBaseEntity();

        
       
        public ActionResult Details(int? id)
        {
           
            Description description = db.Description.Find(id);
            if (description == null)
            {
                return HttpNotFound();
            }
            return PartialView(description);
        }

      

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Description description = db.Description.Find(id);
            if (description == null)
            {
                return HttpNotFound();
            }
            return View(description);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text")] Description description)
        {
            if (ModelState.IsValid)
            {
                db.Entry(description).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List" , "Furniture");
            }
            return View(description);
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
