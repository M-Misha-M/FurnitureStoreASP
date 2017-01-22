namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Furnitures", new[] { "ImageId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Furnitures", "ImageId");

        }
    }
}
