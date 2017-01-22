namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Furnitures", "ImageData", c => c.Binary());
            AddColumn("dbo.Furnitures", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Furnitures", "ImageMimeType");
            DropColumn("dbo.Furnitures", "ImageData");
        }
    }
}
