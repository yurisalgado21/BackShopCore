using BackShopCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BackShopCore.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=BackShopCoreDB;User=SA;Password=Password123;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString: connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>
            (
                entity => 
                {
                    entity.HasKey(c => c.CustomerId);

                    entity.Property<string>("_firstName")
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("FirstName");

                    entity.Property<string>("_lastName")
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("LastName");

                    entity.HasIndex("_email")
                    .IsUnique();

                    entity.Property<string>("_email")
                    .IsRequired()
                    .HasColumnName("Email");

                    entity.Property<DateOnly>("_dateOfBirth")
                    .IsRequired()
                    .HasDefaultValue(DateOnly.FromDateTime(DateTime.Now))
                    .HasColumnName("DateOfBirth");
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}