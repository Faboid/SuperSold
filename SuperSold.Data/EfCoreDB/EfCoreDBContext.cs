using Microsoft.EntityFrameworkCore;
using SuperSold.Data.Models;

namespace SuperSold.Data.EfCoreDB;

public class EfCoreDBContext : DbContext {

    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<AccountWishlistModel> Wishlists { get; set; }
    public DbSet<AccountCartModel> Cart { get; set; }

    public EfCoreDBContext(DbContextOptions<EfCoreDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

    }
}

