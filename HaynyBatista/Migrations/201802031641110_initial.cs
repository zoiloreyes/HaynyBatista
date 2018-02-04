namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articulo",
                c => new
                    {
                        IdArticulo = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(),
                        Titulo = c.String(nullable: false, unicode: false),
                        Contenido = c.String(nullable: false, unicode: false),
                        FechaSubida = c.DateTime(nullable: false),
                        IdImagen = c.Int(),
                    })
                .PrimaryKey(t => t.IdArticulo)
                .ForeignKey("dbo.Imagen", t => t.IdImagen)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdImagen);
            
            CreateTable(
                "dbo.EtiquetaArticulo",
                c => new
                    {
                        IdEtiquetaArticulo = c.Int(nullable: false, identity: true),
                        IdEtiqueta = c.Int(nullable: false),
                        IdArticulo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEtiquetaArticulo)
                .ForeignKey("dbo.Etiqueta", t => t.IdEtiqueta)
                .ForeignKey("dbo.Articulo", t => t.IdArticulo)
                .Index(t => t.IdEtiqueta)
                .Index(t => t.IdArticulo);
            
            CreateTable(
                "dbo.Etiqueta",
                c => new
                    {
                        IdEtiqueta = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.IdEtiqueta);
            
            CreateTable(
                "dbo.Imagen",
                c => new
                    {
                        IdImagen = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(),
                        Formato = c.String(nullable: false, maxLength: 5, unicode: false),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Title = c.String(),
                        FechaSubida = c.DateTime(nullable: false),
                        TipoCita_IdTipoCita = c.Int(),
                    })
                .PrimaryKey(t => t.IdImagen)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .ForeignKey("dbo.TipoCitas", t => t.TipoCita_IdTipoCita)
                .Index(t => t.IdUsuario)
                .Index(t => t.TipoCita_IdTipoCita);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        IdCita = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(),
                        IdEstadoCita = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        HoraInicio = c.Time(nullable: false, precision: 7),
                        HoraFin = c.Time(nullable: false, precision: 7),
                        Costo = c.Double(nullable: false),
                        Mensaje = c.String(),
                        IdTipoCita = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCita)
                .ForeignKey("dbo.EstadoCitas", t => t.IdEstadoCita)
                .ForeignKey("dbo.TipoCitas", t => t.IdTipoCita)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdEstadoCita)
                .Index(t => t.IdTipoCita);
            
            CreateTable(
                "dbo.EstadoCitas",
                c => new
                    {
                        IdEstadoCita = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.IdEstadoCita);
            
            CreateTable(
                "dbo.TipoCitas",
                c => new
                    {
                        IdTipoCita = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdTipoCita);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Usuario_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_IdUsuario)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Usuario_IdUsuario);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Usuario_IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Citas", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Imagen", "TipoCita_IdTipoCita", "dbo.TipoCitas");
            DropForeignKey("dbo.Citas", "IdTipoCita", "dbo.TipoCitas");
            DropForeignKey("dbo.Citas", "IdEstadoCita", "dbo.EstadoCitas");
            DropForeignKey("dbo.Imagen", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Articulo", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Articulo", "IdImagen", "dbo.Imagen");
            DropForeignKey("dbo.EtiquetaArticulo", "IdArticulo", "dbo.Articulo");
            DropForeignKey("dbo.EtiquetaArticulo", "IdEtiqueta", "dbo.Etiqueta");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Usuario_IdUsuario" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Citas", new[] { "IdTipoCita" });
            DropIndex("dbo.Citas", new[] { "IdEstadoCita" });
            DropIndex("dbo.Citas", new[] { "IdUsuario" });
            DropIndex("dbo.Imagen", new[] { "TipoCita_IdTipoCita" });
            DropIndex("dbo.Imagen", new[] { "IdUsuario" });
            DropIndex("dbo.EtiquetaArticulo", new[] { "IdArticulo" });
            DropIndex("dbo.EtiquetaArticulo", new[] { "IdEtiqueta" });
            DropIndex("dbo.Articulo", new[] { "IdImagen" });
            DropIndex("dbo.Articulo", new[] { "IdUsuario" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TipoCitas");
            DropTable("dbo.EstadoCitas");
            DropTable("dbo.Citas");
            DropTable("dbo.Usuario");
            DropTable("dbo.Imagen");
            DropTable("dbo.Etiqueta");
            DropTable("dbo.EtiquetaArticulo");
            DropTable("dbo.Articulo");
        }
    }
}
