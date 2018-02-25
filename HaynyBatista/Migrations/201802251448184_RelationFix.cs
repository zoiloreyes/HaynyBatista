namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Citas", "IdUsuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Citas", "IdUsuario", c => c.Int());
        }
    }
}
