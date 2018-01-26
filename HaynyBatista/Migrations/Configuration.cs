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
            AutomaticMigrationsEnabled = false;
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
        }
    }
}
