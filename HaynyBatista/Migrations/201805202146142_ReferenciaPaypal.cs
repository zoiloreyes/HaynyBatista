namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReferenciaPaypal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "ReferenciaPaypal", c => c.String());
            AddColumn("dbo.Compras", "DireccionPaypal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "DireccionPaypal");
            DropColumn("dbo.Compras", "ReferenciaPaypal");
        }
    }
}
