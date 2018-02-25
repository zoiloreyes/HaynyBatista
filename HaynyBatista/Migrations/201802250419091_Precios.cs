namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Precios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoCitas", "Costo", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoCitas", "Costo");
        }
    }
}
