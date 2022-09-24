using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CommerceBankApp.Models;

namespace CommerceBankApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CommerceBankApp.Models.DonationType> DonationType { get; set; }
        public DbSet<CommerceBankApp.Models.Account> Account { get; set; }
        public DbSet<CommerceBankApp.Models.Donor> Donor { get; set; }
        public DbSet<CommerceBankApp.Models.DonorInfo> DonorInfo { get; set; }
        public DbSet<CommerceBankApp.Models.Organization> Organization { get; set; }
        public DbSet<CommerceBankApp.Models.Payment> Payment { get; set; }
    }
}