namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        FurnitureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Furnitures", t => t.FurnitureId, cascadeDelete: true)
                .Index(t => t.FurnitureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "FurnitureId", "dbo.Furnitures");
            DropIndex("dbo.Files", new[] { "FurnitureId" });
            DropTable("dbo.Files");
        }
    }
}
