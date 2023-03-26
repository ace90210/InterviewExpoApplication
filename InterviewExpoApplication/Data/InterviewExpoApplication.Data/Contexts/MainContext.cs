using InterviewExpoApplication.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewExpoApplication.Data.Contexts
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserCompany> UserCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.HasKey(uc => new { uc.UserId, uc.CompanyId });

                entity.HasOne(uc => uc.User)
                        .WithMany(u => u.UserCompanies)
                        .HasForeignKey(uc => uc.UserId); 
                entity.HasOne(uc => uc.Company)
                      .WithMany(c => c.UserCompanies)
                      .HasForeignKey(uc => uc.CompanyId);
            });
        }
    }
}
