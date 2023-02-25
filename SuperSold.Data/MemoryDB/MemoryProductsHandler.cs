using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;

namespace SuperSold.Data.MemoryDB;

/// <summary>
/// A mock interface for handling products in memory. The underlying database does not retain information between sessions—use only for test purposes.
/// </summary>
public class MemoryProductsHandler : IProductsHandler {

    private readonly MemoryDatabase _db;

    public MemoryProductsHandler() : this(new()) { }
    public MemoryProductsHandler(MemoryDatabase db) {
        _db = db;
    }

    public Task<bool> CreateProduct(ProductModel product, string sellerUserName) {

        //deep copy to ensure it's not modified externally after saving it
        var toCache = new ProductModel() {
            Title = product.Title,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            IdProduct = product.IdProduct,
            IdSellerAccount = product.IdSellerAccount,
            Price = product.Price,
        };

        _db.ProductsTable.Add(toCache.IdProduct, toCache);
        return Task.FromResult(true);

    }

    public Task<bool> DeleteProduct(Guid productId) {
        return Task.FromResult(_db.ProductsTable.Remove(productId));
    }

    public Task<bool> EditProduct(Guid productId, ProductModel updatedValues) {
        
        if(!_db.ProductsTable.TryGetValue(productId, out var product)) {
            return Task.FromResult(false);
        }

        product.Title = updatedValues.Title;
        product.Description = updatedValues.Description;
        product.ImageUrl = updatedValues.ImageUrl;
        product.Price = updatedValues.Price;
        return Task.FromResult(true);

    }

    public IQueryable<ProductModel> QueryAllProducts() {
        return _db.ProductsTable.Values.AsQueryable();   
    }

    public IQueryable<ProductModel> QueryProductsBySellerUserName(string userName) {
        
        if(!_db.AccountsTable.TryGetValue(userName, out var account)) {
            return Enumerable.Empty<ProductModel>().AsQueryable();
        }

        return _db.ProductsTable.Values.Where(x => x.IdSellerAccount == account.IdAccount).AsQueryable();        
    }

}
