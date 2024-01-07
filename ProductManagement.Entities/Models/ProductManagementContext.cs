using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Entities.Models {
    public class ProductManagementContext : DbContext{
        public ProductManagementContext() {}
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<MainPage> MainPages { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {optionsBuilder.UseSqlServer(
                    @"Data Source=.\SQLEXPRESS;Initial Catalog=ProductManagementDb3;Integrated Security=True;TrustServerCertificate=true;",
                    providerOptions => { providerOptions.EnableRetryOnFailure(); });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>()
                .HasOne(a => a.ProductDetail)
                .WithOne(b => b.Product)
                .HasForeignKey<ProductDetail>(b => b.ProductId);
        }
    }
}
