namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageTable : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.Furnitures", "ImageId", c => c.Int(nullable: true));
            Sql("Insert INTO dbo.Images (ImageData, ImageMimeType) VALUES (0x89504E470D0A1A0A0000000D49484452000000270000002708060000008CA35135000000097048597300000B1300000B1301009A9C18000001366943435050686F746F73686F70204943432070726F66696C65000078DAAD8EB14AC3501440CF8BA2E2502B04717078932828B6EA60C6A42D4510ACD521C9D6A4A14A69125E5E, 'image/png')");
            Sql("UPDATE dbo.Furnitures SET ImageId = 1 WHERE ImageId IS NULL");
            CreateIndex("dbo.Furnitures", "ImageId");
            AddForeignKey("dbo.Furnitures", "ImageId", "dbo.Images", "ImageId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Furnitures", "ImageId", "dbo.Images");
            DropIndex("dbo.Furnitures", new[] { "ImageId" });
            DropColumn("dbo.Furnitures", "ImageId");
            DropTable("dbo.Images");
        }
    }
}
