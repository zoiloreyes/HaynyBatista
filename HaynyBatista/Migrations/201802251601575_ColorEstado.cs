namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColorEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EstadoCitas", "Color", c => c.String(nullable: false));
            AlterColumn("dbo.EstadoCitas", "Nombre", c => c.String(nullable: false));
            DropColumn("dbo.TipoCitas", "Color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoCitas", "Color", c => c.String(nullable: false));
            AlterColumn("dbo.EstadoCitas", "Nombre", c => c.String());
            DropColumn("dbo.EstadoCitas", "Color");
        }
    }
}
