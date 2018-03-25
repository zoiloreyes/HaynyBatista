namespace HaynyBatista.Migrations
{
    using HaynyBatista.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HaynyBatista.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HaynyBatista.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Administrador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrador" };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Visitante"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Visitante" };

                manager.Create(role);
            }
            if (!context.Users.Any(u => u.UserName == "zoiloreyes"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "zoiloreyes", Email = "zoiloismaelreyes1@gmail.com", PhoneNumber = "8096991872", Usuario = new Usuario() { Nombre = "Zoilo", Apellido = "Reyes" } };

                manager.Create(user, "Admin123");
                manager.AddToRole(user.Id, "Administrador");
            }
            if (!context.EstadosCita.Any(e => e.Nombre == "Pendiente de aprobar"))
            {
                context.EstadosCita.Add(new EstadoCita() { Nombre = "Pendiente de aprobar", Color = "#e1bee7" });
                context.SaveChanges();
            }
            if (!context.EstadosCita.Any(e => e.Nombre == "Aprobada"))
            {
                context.EstadosCita.Add(new EstadoCita() { Nombre = "Aprobada", Color = "#4285F4" });
                context.SaveChanges();
            }
            if (!context.EstadosCita.Any(e => e.Nombre == "Cancelada"))
            {
                context.EstadosCita.Add(new EstadoCita() { Nombre = "Cancelada", Color = "#CC0000" });
                context.SaveChanges();
            }
            if (!context.EstadosCita.Any(e => e.Nombre == "Pagada"))
            {
                context.EstadosCita.Add(new EstadoCita() { Nombre = "Pagada", Color = "#00C851" });
                context.SaveChanges();
            }
            if (!context.EstadosCita.Any(e => e.Nombre == "Cumplida"))
            {
                context.EstadosCita.Add(new EstadoCita() { Nombre = "Cumplida", Color = "#f8bbd0" });
                context.SaveChanges();
            }
            if (!context.EstadosCita.Any(e => e.Nombre == "Ausente"))
            {
                context.EstadosCita.Add(new EstadoCita() { Nombre = "Ausente", Color = "#ffeb3b" });
                context.SaveChanges();
            }

            if (!context.TiposCita.Any(e => e.Nombre == "Pareja"))
            {
                context.TiposCita.Add(new TipoCita() { Nombre = "Pareja" });
                context.SaveChanges();
            }
            if (!context.TiposCita.Any(e => e.Nombre == "Individual"))
            {
                context.TiposCita.Add(new TipoCita() { Nombre = "Individual" });
                context.SaveChanges();
            }
            if (!context.TiposCita.Any(e => e.Nombre == "Skype"))
            {
                context.TiposCita.Add(new TipoCita() { Nombre = "Skype" });
                context.SaveChanges();
            }
            if (!context.FormasPago.Any(e => e.Nombre == "Efectivo"))
            {
                context.FormasPago.Add(new FormaPago() { FormaPagoID = 1, Nombre = "Efectivo" });
                context.SaveChanges();
            }
            if (!context.FormasPago.Any(e => e.Nombre == "PayPal"))
            {
                context.FormasPago.Add(new FormaPago() { FormaPagoID = 2, Nombre = "PayPal" });
                context.SaveChanges();
            }

        }
    }
}