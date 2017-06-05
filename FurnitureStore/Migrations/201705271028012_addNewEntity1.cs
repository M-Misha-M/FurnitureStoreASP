namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewEntity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Furnitures", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Furnitures", "IsAvailable");
        }
    }
}
