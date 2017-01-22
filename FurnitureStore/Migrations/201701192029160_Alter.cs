namespace FurnitureStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Furnitures", "ImageId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Furnitures", "ImageId", c => c.Int(nullable: true));
        }
    }
}
