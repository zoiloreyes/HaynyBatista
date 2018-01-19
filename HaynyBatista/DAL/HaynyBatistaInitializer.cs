using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaynyBatista.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace HaynyBatista.DAL
{
    public class HaynyBatistaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
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
                var user = new ApplicationUser { UserName = "zoiloreyes", Email = "zoiloismaelreyes1@gmail.com", PhoneNumber = "8096991872" };

                manager.Create(user, "Admin123");
                manager.AddToRole(user.Id, "Administrador");
            }
        }
    }
}