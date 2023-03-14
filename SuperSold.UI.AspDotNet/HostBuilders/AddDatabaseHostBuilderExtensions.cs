using Microsoft.EntityFrameworkCore;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.EfCoreDB;
using SuperSold.Data.MemoryDB;
using SuperSold.Data.Models;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddDatabaseHostBuilderExtensions {

    public static void AddMemoryDatabase(this IServiceCollection services) {

        //temporary fake data
        var memoryDB = new MemoryDatabase();
        var account = new AccountModel() { Email = "some@email.com", UserName = "admin", HashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword("password"), IdAccount = Guid.Parse("3c0751d1-47e4-429b-a2ba-3a8c1781c413") };
        var product = new ProductModel() { IdProduct = Guid.NewGuid(), Title = "XBox 360", Description = "A used xbox with some scratches.", ImageUrl = null, IdSellerAccount = (Guid)account.IdAccount, Price = 300M };
        memoryDB.AccountsTable.Add("admin", account);
        memoryDB.ProductsTable.Add(product.IdProduct, product);

        services.AddSingleton(memoryDB);
        services.AddScoped<IAccountsHandler, MemoryAccountsHandler>();
        services.AddScoped<IProductsHandler, MemoryProductsHandler>();

    }

    public static void AddMySqlDatabase(this IServiceCollection services, string connectionString) {
        services.AddMySql<EfCoreDBContext>(connectionString, ServerVersion.AutoDetect(connectionString));
        services.AddScoped<IAccountsHandler, EfCoreAccountsHandler>();
        services.AddScoped<IProductsHandler, EfCoreProductsHandler>();
        services.AddScoped<IWishlistHandler, EfCoreWishlistHandler>();
        services.AddScoped<ICartHandler, EFCoreCartHandler>();
    }

}
