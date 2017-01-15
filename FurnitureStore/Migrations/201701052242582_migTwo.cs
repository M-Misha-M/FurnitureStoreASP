namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migTwo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Furnitures", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Furnitures", "CategoryId");
            AddForeignKey("dbo.Furnitures", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Furnitures", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Furnitures", new[] { "CategoryId" });
            DropColumn("dbo.Furnitures", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
