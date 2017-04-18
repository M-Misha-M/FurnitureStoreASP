using FurnitureStore.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FurnitureStore.Concrete
{
    public class DataBaseEntity : IdentityDbContext<ApplicationUser>
    {
        public DataBaseEntity():
            base("DataBaseEntity")
        { }

        public static DataBaseEntity Create()
        {
            return new DataBaseEntity();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Description>().ToTable("Description");
        }



        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }
        public DbSet<FurnitureImages>FurnitureImages { get; set; }
        public DbSet<Description> Description { get; set; }
     

        public System.Data.Entity.DbSet<FurnitureStore.Entities.ApplicationRole> IdentityRoles { get; set; }


        
    }
}