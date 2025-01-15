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

                    entity.HasMany(a => a.Addresses)
                    .WithOne(c => c.Customer)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
                }
            );

            modelBuilder.Entity<Address>(entity =>
                {
                    entity.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("ZipCode");

                    entity.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Street");

                    entity.Property(a => a.Number)
                    .IsRequired()
                    .HasColumnName("Number");

                    entity.Property(a => a.Neighborhood)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Neighborhood");

                    entity.Property(a => a.AddressComplement)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("AddressComplement");

                    entity.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("City");

                    entity.Property(a => a.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("State");

                    entity.Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Country");

                    entity.HasOne(a => a.Customer)
                    .WithMany(a => a.Addresses)
                    .HasForeignKey(a => a.CustomerId);
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}