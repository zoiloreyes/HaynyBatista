namespace HaynyBatista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tienda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProductoID = c.Int(nullable: false, identity: true),
                        Titulo = c.Int(nullable: false),
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
                "dbo.ArticuloCarritoes",
                c => new
                    {
                        ArticuloCarritoID = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        ProductoID = c.Int(nullable: false),
                        Carrito_CarritoID = c.Int(),
                    })
                .PrimaryKey(t => t.ArticuloCarritoID)
                .ForeignKey("dbo.Productoes", t => t.ProductoID, cascadeDelete: true)
                .ForeignKey("dbo.Carritoes", t => t.Carrito_CarritoID)
                .Index(t => t.ProductoID)
                .Index(t => t.Carrito_CarritoID);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaID);
            
            CreateTable(
                "dbo.Pagoes",
                c => new
                    {
                        PagoId = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioID = c.Int(nullable: false),
                        FormaPagoID = c.Int(nullable: false),
                        CarritoID = c.Int(nullable: false),
                        Usuario_IdUsuario = c.Int(),
                    })
                .PrimaryKey(t => t.PagoId)
                .ForeignKey("dbo.Carritoes", t => t.CarritoID, cascadeDelete: true)
                .ForeignKey("dbo.FormaPagoes", t => t.FormaPagoID, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.Usuario_IdUsuario)
                .Index(t => t.FormaPagoID)
                .Index(t => t.CarritoID)
                .Index(t => t.Usuario_IdUsuario);
            
            CreateTable(
                "dbo.Carritoes",
                c => new
                    {
                        CarritoID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CarritoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pagoes", "Usuario_IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Pagoes", "FormaPagoID", "dbo.FormaPagoes");
            DropForeignKey("dbo.Pagoes", "CarritoID", "dbo.Carritoes");
            DropForeignKey("dbo.ArticuloCarritoes", "Carrito_CarritoID", "dbo.Carritoes");
            DropForeignKey("dbo.Productoes", "IdImagen", "dbo.Imagen");
            DropForeignKey("dbo.Productoes", "CategoriaID", "dbo.Categorias");
            DropForeignKey("dbo.ArticuloCarritoes", "ProductoID", "dbo.Productoes");
            DropIndex("dbo.Pagoes", new[] { "Usuario_IdUsuario" });
            DropIndex("dbo.Pagoes", new[] { "CarritoID" });
            DropIndex("dbo.Pagoes", new[] { "FormaPagoID" });
            DropIndex("dbo.ArticuloCarritoes", new[] { "Carrito_CarritoID" });
            DropIndex("dbo.ArticuloCarritoes", new[] { "ProductoID" });
            DropIndex("dbo.Productoes", new[] { "CategoriaID" });
            DropIndex("dbo.Productoes", new[] { "IdImagen" });
            DropTable("dbo.Carritoes");
            DropTable("dbo.Pagoes");
            DropTable("dbo.Categorias");
            DropTable("dbo.ArticuloCarritoes");
            DropTable("dbo.Productoes");
        }
    }
}
