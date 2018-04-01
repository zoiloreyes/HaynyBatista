using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HaynyBatista.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Email", this.Email.ToString()));
            userIdentity.AddClaim(new Claim("PhoneNumber", this.PhoneNumber.ToString()));
            userIdentity.AddClaim(new Claim("HaynyUsuarioId", this.Usuario.IdUsuario.ToString()));
            userIdentity.AddClaim(new Claim("FirstName", this.Usuario.Nombre.ToString()));
            userIdentity.AddClaim(new Claim("LastName", this.Usuario.Apellido.ToString()));


            return userIdentity;
        }
        public virtual Usuario Usuario { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<Etiqueta> Etiquetas { get; set; }
        public virtual DbSet<EtiquetaArticulo> EtiquetaArticulo { get; set; }
        public virtual DbSet<Imagen> Imagenes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Cita> Citas { get; set; }
        public virtual DbSet<TipoCita> TiposCita { get; set; }
        public virtual DbSet<EstadoCita> EstadosCita { get; set; }
        public virtual DbSet<FormaPago> FormasPago { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Carrito> Carritos { get; set; }
        public virtual DbSet<ArticuloCarrito> ArticulosCarrito { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Articulo>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Articulo>()
                .Property(e => e.Contenido)
                .IsUnicode(false);

            modelBuilder.Entity<Articulo>()
                .HasMany(e => e.EtiquetaArticulo)
                .WithRequired(e => e.Articulo)
                .WillCascadeOnDelete(false);
            

            modelBuilder.Entity<TipoCita>()
                .HasMany(e => e.Cita)
                .WithRequired(e => e.TipoCita)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstadoCita>()
                .HasMany(e => e.Cita)
                .WithRequired(e => e.EstadoCita)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etiqueta>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<FormaPago>()
                .HasMany(e => e.Citas)
                .WithRequired(e => e.FormaPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etiqueta>()
                .HasMany(e => e.EtiquetaArticulo)
                .WithRequired(e => e.Etiqueta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Imagen>()
                .Property(e => e.Formato)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany<Cita>(u => u.Citas)
                .WithMany(c => c.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("UsuarioId");
                    cs.MapRightKey("CitaId");
                    cs.ToTable("UsuarioCita");
                });
            modelBuilder.Entity<Usuario>()
                .HasRequired<ApplicationUser>(u => u.AppUser)
                .WithRequiredPrincipal(u => u.Usuario);
        }
    }
}