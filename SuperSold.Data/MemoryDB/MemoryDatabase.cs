using SuperSold.Data.Models;

namespace SuperSold.Data.MemoryDB;

/// <summary>
/// Mocks a database. Does not retain information between sessions—use only for test purposes.
/// </summary>
public class MemoryDatabase {

    public MemoryDatabase() : this(new(), new()) { }
    public MemoryDatabase(Dictionary<string, AccountModel> accountsTable, Dictionary<Guid, ProductModel> productsTable) {
        AccountsTable = accountsTable;
        ProductsTable = productsTable;
    }

    public Dictionary<string, AccountModel> AccountsTable { get; init; }
    public Dictionary<Guid, ProductModel> ProductsTable { get; init; }
}