namespace Businessdevweb.Migrations
{
    using Areas.Admin.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Businessdevweb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Businessdevweb.Models.ApplicationDbContext";
        }

        protected override void Seed(Businessdevweb.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser { UserName = "Admin@Email.com", Email = "admin@Email.com" };

            
            context.Roles.AddOrUpdate(m => m.Name,
               new IdentityRole { Name = Roles.Admin },
               new IdentityRole { Name = Roles.User }
                );
            if (context.Users.Where(m => m.Email == user.Email).Count() == 0)
            {
                userManager.Create(user, "Adm!n123");
                userManager.AddToRole(user.Id, Roles.Admin);
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
