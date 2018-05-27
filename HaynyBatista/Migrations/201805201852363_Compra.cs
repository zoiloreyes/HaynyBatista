namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Compra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "Direccion", c => c.String());
            AddColumn("dbo.Compras", "Provincia", c => c.String());
            AddColumn("dbo.Compras", "Sector", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "Sector");
            DropColumn("dbo.Compras", "Provincia");
            DropColumn("dbo.Compras", "Direccion");
        }
    }
}
