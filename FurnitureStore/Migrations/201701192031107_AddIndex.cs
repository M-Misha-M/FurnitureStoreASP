namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Furnitures", "ImageId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Furnitures", new[] { "ImageId" });
        }
    }
}
