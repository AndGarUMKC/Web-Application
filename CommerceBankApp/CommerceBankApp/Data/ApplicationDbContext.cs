using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CommerceBankApp.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using CommerceBankApp.Models;

namespace CommerceBankApp.Data
{
    public partial class ApplicationDbContext : AuthDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity<Organization>().HasMany(p => p.Payment);
            //builder.Entity<Organization>().HasMany(p => p.DonationType).WithMany(d => d.Organization);
        }

        public DbSet<CommerceBankApp.Models.DonationType> DonationType { get; set; }

        public DbSet<CommerceBankApp.Models.Payment> Payment { get; set; }

        public DbSet<CommerceBankApp.Models.PaymentInfo> PaymentInfo { get; set; }

        public DbSet<CommerceBankApp.Models.Organization> Organization { get; set; }
    }
}
