namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImageData", c => c.Binary());
            AddColumn("dbo.Categories", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ImageMimeType");
            DropColumn("dbo.Categories", "ImageData");
        }
    }
}
