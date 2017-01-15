using FurnitureStore.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FurnitureStore.Entities;
using System.Data.Entity;

namespace FurnitureStore.Concrete
{
    public class FurnitureRepository : IFurnitureRepo
    {
        DataBaseEntity context = new DataBaseEntity();

       public IEnumerable<Category> Categories
        {
            get
            {
                return context.Categories.ToList();
            }
        }

        public IEnumerable<File> Files
        {
            get
            {
                return context.Files;
            }

           
        }

        public IEnumerable<Furniture> Furnitures
        {
            get
            {
                return context.Furnitures;
            }
        }

        public Category BrowseResult(string category)
        {
            var model = context.Categories.Include("Furnitures")
                .SingleOrDefault(g => g.Name == category);

         


            return model;
        }



        public Category DeleteCategory(int categoryId)
        {
            Category dbEntry = context.Categories.Find(categoryId);

            if(dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }



        public Furniture Details(int id)
        {
            var funitures = context.Furnitures.Find(id);

            return funitures;
        }

        public Furniture FindById(int? id)
        {


            return context.Furnitures.Find(id);
        }

        public Furniture IncludeFurniture(int? id)
        {
            Furniture furniture = context.Furnitures.Include(s => s.Files).SingleOrDefault(s => s.FurnitureId == id);


            return furniture;
        }



        public void SaveCategory(Category category , bool updateImage = false)
        {
             if (category.CategoryId == 0)
                 context.Categories.Add(category);
             else
             {

                 Category dbEntry = context.Categories.Attach(category);

                if (dbEntry != null)
                 {

                   
                    var entry = context.Entry(category);
                    // mark product as modified

                    entry.State = EntityState.Modified;


                    entry.Property(e => e.ImageData).IsModified = updateImage;
                    entry.Property(e => e.ImageMimeType).IsModified = updateImage;

                    context.SaveChanges();

                }
             }
             context.SaveChanges();

           


        }

        public void SaveFurniture(Furniture furniture)
        {
            context.Furnitures.Add(furniture);
            context.SaveChanges();
        }
    }
}