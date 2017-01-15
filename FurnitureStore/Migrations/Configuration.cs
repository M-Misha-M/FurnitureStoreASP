namespace FurnitureStore.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FurnitureStore.Concrete.DataBaseEntity>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FurnitureStore.Concrete.DataBaseEntity context)
        {
           /* var categories = new List<Category>
            {
                new Category {Name = "Комоди" } ,
                 new Category {Name = "Кухні" } ,
                  new Category {Name = "Ліжка" } ,
                   new Category {Name = "Прихожі" } ,
                    new Category {Name = "Трюмо" } ,
                    new Category {Name = "Шафи-Купе" } ,

            };

            categories.ForEach(a => context.Categories.Add(a));
            context.SaveChanges();



            var furnitures = new List<Furniture>
            {
                new Furniture {Name = "Шафа-Купе \"ПРЕМІУМ\" " , Description = "Шафа купе власного виробництва.Вартість вказана за базову комплектацію шафи" ,  Manufacturer = "VIP Master" , Size = "2214/2400(2200)/600" , Price = 4500 , Category = categories.Single(c => c.Name == "Шафи-Купе") } ,
                 new Furniture {Name = "Комод KOM1W2D2S" , Description = "Комод з чотирма висувними ящиками і одним закритим відділенням з двома полками всередині з модульної системи Вушер" ,  Manufacturer = "Gerbor" , Size = "1500/935/450" , Price = 2815 , Category = categories.Single(c => c.Name == "Комоди") } ,
                new Furniture {Name = "Кухня PRESTIGE" , Description = "Корпус: дуб \"молочний\", горіх, венге" ,  Manufacturer = "Mebel-Star" , Size = "1500/935/450" , Price = 3000 , Category = categories.Single(c => c.Name == "Кухні") } ,
                 new Furniture {Name = "Ліжко LOZ90 (каркас)" , Description = "Ліжко" ,  Manufacturer = "Gerbor" , Size = "2035/660/950" , Price = 890 , Category = categories.Single(c => c.Name == "Ліжка") } ,
                 new Furniture {Name = "Прихожа PPK \"Непо\"" , Description = "Прихожа" ,  Manufacturer = "Gerbor" , Size = "900/1840/305" , Price = 1340 , Category = categories.Single(c => c.Name == "Прихожі") } ,
                 new Furniture {Name = "Трюмо Мілано" , Description = "Трюмо від виробника «Мілано» прикрасить будь-яке приміщення. Його цікавий, але в той же час стриманий дизайн, дозволяють поставити його в домашній спальні, або в шикарному готельному номері. Виріб виготовлено з ДСП, тому прослужить Вам багато років." ,  Manufacturer = "Мілано" , Size = "900/1840/305" , Price = 1220 , Category = categories.Single(c => c.Name == "Трюмо") }


            };


            furnitures.ForEach(f => context.Furnitures.Add(f));
            context.SaveChanges();*/
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
