using ExamProject.Data.ShopDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Description> Description { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Телефоны" },
            new Category { Id = 2, Name = "Телевизоры" },
            new Category { Id = 3, Name = "Холодильники" });

        modelBuilder.Entity<Description>().HasData(
            new Description { Id = 1, Name = "Samsung" },
            new Description { Id = 2, Name = "Toshiba" });

        //    // --- Category -> Products (1 : Many) ---
        //    modelBuilder.Entity<Category>()
        //        .HasMany(c => c.Products)
        //        .WithOne(p => p.Category)
        //        .HasForeignKey(p => p.CategoryId);

        //    // --- Description -> Products (1 : Many) ---
        //    modelBuilder.Entity<Description>()
        //        .HasMany(d => d.Products)
        //        .WithOne(p => p.Description)
        //        .HasForeignKey(p => p.DescriptionId);

        //    // --- Cart -> CartItems (1 : Many) ---
        //    modelBuilder.Entity<Cart>()
        //        .HasMany(c => c.Items)
        //        .WithOne(ci => ci.Cart)
        //        .HasForeignKey(ci => ci.CartId);

        //    // --- CartItem -> Products (1 : Many) ---
        //    modelBuilder.Entity<CartItem>()
        //        .HasMany(ci => ci.Products)
        //        .WithOne(p => p.CartItem)
        //        .HasForeignKey(p => p.CartItemId);

        // --- User -> Cart (1 : 1) ---

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.ApplicationUserId);
    }
}
