using Microsoft.EntityFrameworkCore;
using SuperSold.Data.Models;

namespace SuperSold.Data.EfCoreDB;

public class EfCoreDBContext : DbContext {

    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<SavedRelationshipModel> SavedRelationships { get; set; }
    public DbSet<RollbackModel> Rollbacks { get; set; }
    public DbSet<AccountRoleModel> Accounts_Roles { get; set; }
    public DbSet<AccountRestrictionModel> Accounts_Restrictions { get; set; }

    public EfCoreDBContext(DbContextOptions<EfCoreDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

    }
}

