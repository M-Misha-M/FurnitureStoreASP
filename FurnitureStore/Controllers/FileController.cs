using FurnitureStore.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.Controllers
{
    public class FileController : Controller
    {
        DataBaseEntity db = new DataBaseEntity();
        // GET: File
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content , fileToRetrieve.ContentType);
        }
    }
}