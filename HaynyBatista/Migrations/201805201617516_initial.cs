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
                .ForeignKey("dbo.TipoCitas", t => t.TipoCita_IdTipoCita)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .Index(t => t.IdUsuario)
                .Index(t => t.TipoCita_IdTipoCita);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProductoID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Descripcion = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cantidad = c.Int(nullable: false),
                        IdImagen = c.Int(nullable: false),
                        CategoriaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoID)
                .ForeignKey("dbo.Categorias", t => t.CategoriaID, cascadeDelete: true)
                .ForeignKey("dbo.Imagen", t => t.IdImagen, cascadeDelete: true)
                .Index(t => t.IdImagen)
                .Index(t => t.CategoriaID);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaID);
            
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
                        Usuario_IdUsuario = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        IdCita = c.Int(nullable: false, identity: true),
                        IdEstadoCita = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        HoraInicio = c.Time(nullable: false, precision: 7),
                        HoraFin = c.Time(nullable: false, precision: 7),
                        Costo = c.Double(nullable: false),
                        Mensaje = c.String(),
                        IdTipoCita = c.Int(nullable: false),
                        IdFormaPago = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCita)
                .ForeignKey("dbo.EstadoCitas", t => t.IdEstadoCita)
                .ForeignKey("dbo.FormaPagoes", t => t.IdFormaPago)
                .ForeignKey("dbo.TipoCitas", t => t.IdTipoCita)
                .Index(t => t.IdEstadoCita)
                .Index(t => t.IdTipoCita)
                .Index(t => t.IdFormaPago);
            
            CreateTable(
                "dbo.EstadoCitas",
                c => new
                    {
                        IdEstadoCita = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Color = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdEstadoCita);
            
            CreateTable(
                "dbo.FormaPagoes",
                c => new
                    {
                        FormaPagoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FormaPagoID);
            
            CreateTable(
                "dbo.Pagoes",
                c => new
                    {
                        PagoId = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioID = c.Int(nullable: false),
                        FormaPagoID = c.Int(nullable: false),
                        Usuario_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.PagoId)
                .ForeignKey("dbo.FormaPagoes", t => t.FormaPagoID, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.Usuario_IdUsuario)
                .Index(t => t.FormaPagoID)
                .Index(t => t.Usuario_IdUsuario);
            
            CreateTable(
                "dbo.TipoCitas",
                c => new
                    {
                        IdTipoCita = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Costo = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdTipoCita);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        CompraID = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompraID)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.ItemCompras",
                c => new
                    {
                        ItemCompraID = c.Int(nullable: false, identity: true),
                        CompraID = c.Int(nullable: false),
                        ProductoID = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ItemCompraID)
                .ForeignKey("dbo.Compras", t => t.CompraID, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProductoID, cascadeDelete: true)
                .Index(t => t.CompraID)
                .Index(t => t.ProductoID);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ItemCompras", "ProductoID", "dbo.Productoes");
            DropForeignKey("dbo.ItemCompras", "CompraID", "dbo.Compras");
            DropForeignKey("dbo.Compras", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Imagen", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioCita", "CitaId", "dbo.Citas");
            DropForeignKey("dbo.UsuarioCita", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Imagen", "TipoCita_IdTipoCita", "dbo.TipoCitas");
            DropForeignKey("dbo.Citas", "IdTipoCita", "dbo.TipoCitas");
            DropForeignKey("dbo.Pagoes", "Usuario_IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Pagoes", "FormaPagoID", "dbo.FormaPagoes");
            DropForeignKey("dbo.Citas", "IdFormaPago", "dbo.FormaPagoes");
            DropForeignKey("dbo.Citas", "IdEstadoCita", "dbo.EstadoCitas");
            DropForeignKey("dbo.Articulo", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.AspNetUsers", "Usuario_IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Productoes", "IdImagen", "dbo.Imagen");
            DropForeignKey("dbo.Productoes", "CategoriaID", "dbo.Categorias");
            DropForeignKey("dbo.Articulo", "IdImagen", "dbo.Imagen");
            DropForeignKey("dbo.EtiquetaArticulo", "IdArticulo", "dbo.Articulo");
            DropForeignKey("dbo.EtiquetaArticulo", "IdEtiqueta", "dbo.Etiqueta");
            DropIndex("dbo.UsuarioCita", new[] { "CitaId" });
            DropIndex("dbo.UsuarioCita", new[] { "UsuarioId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ItemCompras", new[] { "ProductoID" });
            DropIndex("dbo.ItemCompras", new[] { "CompraID" });
            DropIndex("dbo.Compras", new[] { "IdUsuario" });
            DropIndex("dbo.Pagoes", new[] { "Usuario_IdUsuario" });
            DropIndex("dbo.Pagoes", new[] { "FormaPagoID" });
            DropIndex("dbo.Citas", new[] { "IdFormaPago" });
            DropIndex("dbo.Citas", new[] { "IdTipoCita" });
            DropIndex("dbo.Citas", new[] { "IdEstadoCita" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Usuario_IdUsuario" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Productoes", new[] { "CategoriaID" });
            DropIndex("dbo.Productoes", new[] { "IdImagen" });
            DropIndex("dbo.Imagen", new[] { "TipoCita_IdTipoCita" });
            DropIndex("dbo.Imagen", new[] { "IdUsuario" });
            DropIndex("dbo.EtiquetaArticulo", new[] { "IdArticulo" });
            DropIndex("dbo.EtiquetaArticulo", new[] { "IdEtiqueta" });
            DropIndex("dbo.Articulo", new[] { "IdImagen" });
            DropIndex("dbo.Articulo", new[] { "IdUsuario" });
            DropTable("dbo.UsuarioCita");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ItemCompras");
            DropTable("dbo.Compras");
            DropTable("dbo.TipoCitas");
            DropTable("dbo.Pagoes");
            DropTable("dbo.FormaPagoes");
            DropTable("dbo.EstadoCitas");
            DropTable("dbo.Citas");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Usuario");
            DropTable("dbo.Categorias");
            DropTable("dbo.Productoes");
            DropTable("dbo.Imagen");
            DropTable("dbo.Etiqueta");
            DropTable("dbo.EtiquetaArticulo");
            DropTable("dbo.Articulo");
        }
    }
}
