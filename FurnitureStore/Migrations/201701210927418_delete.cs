namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Furnitures", "ImageId", "dbo.Images");
            DropIndex("dbo.Furnitures", new[] { "ImageId" });
            DropColumn("dbo.Furnitures", "ImageId");
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            AddColumn("dbo.Furnitures", "ImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Furnitures", "ImageId");
            AddForeignKey("dbo.Furnitures", "ImageId", "dbo.Images", "ImageId", cascadeDelete: true);
        }
    }
}
