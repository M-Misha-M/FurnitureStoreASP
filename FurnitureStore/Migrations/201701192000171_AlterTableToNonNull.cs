namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableToNonNull : DbMigration
    {
        public override void Up()
        {

            AlterColumn("dbo.Furnitures", "ImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Furnitures", "ImageId");
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Furnitures", "ImageId", c => c.Int(nullable: true));
            DropIndex("dbo.Furnitures", new[] { "ImageId" });

        }
    }
}
