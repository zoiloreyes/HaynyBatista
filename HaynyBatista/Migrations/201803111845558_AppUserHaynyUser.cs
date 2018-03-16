namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserHaynyUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Usuario_IdUsuario" });
            AlterColumn("dbo.AspNetUsers", "Usuario_IdUsuario", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Usuario_IdUsuario");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Usuario_IdUsuario" });
            AlterColumn("dbo.AspNetUsers", "Usuario_IdUsuario", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Usuario_IdUsuario");
        }
    }
}
