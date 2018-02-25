namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Citas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Citas", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.Citas", new[] { "IdUsuario" });
            CreateTable(
                "dbo.UsuarioCita",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        CitaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.CitaId })
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Citas", t => t.CitaId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.CitaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioCita", "CitaId", "dbo.Citas");
            DropForeignKey("dbo.UsuarioCita", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.UsuarioCita", new[] { "CitaId" });
            DropIndex("dbo.UsuarioCita", new[] { "UsuarioId" });
            DropTable("dbo.UsuarioCita");
            CreateIndex("dbo.Citas", "IdUsuario");
            AddForeignKey("dbo.Citas", "IdUsuario", "dbo.Usuario", "IdUsuario");
        }
    }
}
