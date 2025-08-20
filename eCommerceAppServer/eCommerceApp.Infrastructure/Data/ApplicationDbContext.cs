using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Achieve> CheckoutAchieves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles with consistent GUIDs
            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "1A2B3C4D-5E6F-7G8H-9I0J-K1L2M3N4O5P6", // Fixed GUID for Admin
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Id = "7P6O5N4M-3L2K-1J0I-9H8G-7F6E5D4C3B2A", // Fixed GUID for User
                        Name = "User",
                        NormalizedName = "USER"
                    });

            builder.Entity<PaymentMethod>()
                .HasData(
                    new PaymentMethod 
                    {
                        Id = Guid.Parse("B1F6C2D4-8A7E-4F93-BF17-5D1A9E8C3A2B"),
                        Name = "Credit Card"
                    });
        }
    }
}