namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mogr1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Furnitures",
                c => new
                    {
                        FurnitureId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Manufacturer = c.String(),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.FurnitureId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Furnitures");
        }
    }
}
