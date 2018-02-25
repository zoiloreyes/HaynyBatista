namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class color : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoCitas", "Color", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoCitas", "Color");
        }
    }
}
