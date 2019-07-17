using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Businessdevweb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }                  
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageFile { get; set; }
        public bool IsNews { get; set; }

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
        static ApplicationDbContext()
        {
            System.Data.Entity.Database.SetInitializer(
                new System.Data.Entity.MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
        }

        public System.Data.Entity.DbSet<Businessdevweb.Models.ProductCategory> ProductCategories { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.Product> Products { get; set; }
      
        public System.Data.Entity.DbSet<Businessdevweb.Models.ProductImage> ProductImages { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.Setting> Settings { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.ProductFeatures> ProductFeatures { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.ContactUs> ContactUs { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.Slider> Sliders { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.SliderImage> SliderImages { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.AboutMe> AboutMe { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.ContactUsPage> ContactUsPage { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.OrderItem> OrderItems { get; set; }
        public System.Data.Entity.DbSet<Businessdevweb.Models.Payment> Payments { get; set; }


        protected override void OnModelCreating
    (DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ProductCategory.Configuration());
            modelBuilder.Configurations.Add(new Comment.Configuration());
            modelBuilder.Configurations.Add(new Order.Configuration());
            modelBuilder.Configurations.Add(new OrderItem.Configuration());
            modelBuilder.Configurations.Add(new Payment.Configuration());
            modelBuilder.Configurations.Add(new ProductImage.Configuration());
            modelBuilder.Configurations.Add(new Product.Configuration());
            modelBuilder.Configurations.Add(new ProductFeatures.Configuration());
            modelBuilder.Configurations.Add(new AboutMe.Configuration());
            modelBuilder.Configurations.Add(new ContactUsPage.Configuration());
            modelBuilder.Configurations.Add(new Setting.Configuration());
            modelBuilder.Configurations.Add(new ContactUs.Configuration());
            modelBuilder.Configurations.Add(new Slider.Configuration());
            modelBuilder.Configurations.Add(new SliderImage.Configuration());

        }
    }

}