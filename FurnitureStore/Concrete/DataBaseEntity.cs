using FurnitureStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FurnitureStore.Concrete
{
    public class DataBaseEntity : DbContext
    {
        public DataBaseEntity():
            base("DataBaseEntity")
        { }

        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Category> Categories { get; set; }
      
    }
}