namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaCompra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "FechaCreacion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "FechaCreacion");
        }
    }
}
