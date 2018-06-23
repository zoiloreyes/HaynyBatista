namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumeroVisitas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articulo", "Visitas", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articulo", "Visitas");
        }
    }
}
