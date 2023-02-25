using SuperSold.Data.DBInteractions;
using SuperSold.Data.MemoryDB;
using SuperSold.Data.Models;

namespace SuperSold.UI.AspDotNet.HostBuilders;

public static class AddDatabaseHostBuilderExtensions {

    public static void AddMemoryDatabase(this IServiceCollection services) {

        //temporary fake data
        var memoryDB = new MemoryDatabase();
        var account = new AccountModel() { Email = "some@email.com", UserName = "admin", HashedPassword = "password", IdAccount = Guid.Parse("3c0751d1-47e4-429b-a2ba-3a8c1781c413") };
        memoryDB.AccountsTable.Add("admin", account);
        memoryDB.ProductsTable.Add((Guid)account.IdAccount, new() { IdProduct = Guid.NewGuid(), Title = "XBox 360", Description = "A used xbox with some scratches.", ImageUrl = null, IdSellerAccount = (Guid)account.IdAccount, Price = 300M });

        services.AddSingleton(memoryDB);
        services.AddSingleton<IAccountsHandler, MemoryAccountsHandler>();
        services.AddSingleton<IProductsHandler, MemoryProductsHandler>();

    }

}
