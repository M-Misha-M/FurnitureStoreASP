using FurnitureStore.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FurnitureStore.Entities
{
    public class AppDbInitializer : DropCreateDatabaseAlways<DataBaseEntity>
    {
        protected override void Seed(DataBaseEntity context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role2 = new IdentityRole { Name = "user" };


            // добавляем роли в бд
            roleManager.Create(role2);
  

            // создаем пользователей
            var admin = new ApplicationUser { Email = "somemail@ukr.net", UserName = "somemail@ukr.net" };
            string password = "123456";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role2.Name);
                context.SaveChanges();

            }
           
            base.Seed(context);
        }
    }
}