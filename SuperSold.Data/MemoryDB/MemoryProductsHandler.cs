using OneOf.Types;
using OneOf;
using SuperSold.Data.DBInteractions;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;
using SuperSold.Data.Extensions;

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

    public Task<OneOf<Success, AlreadyExists>> CreateProduct(ProductModel product, string sellerUserName) => InternalCreateProduct(product, sellerUserName).AsTask();
    private OneOf<Success, AlreadyExists> InternalCreateProduct(ProductModel product, string sellerUserName) {

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
        return new Success();

    }

    public Task<OneOf<Success, NotFound>> DeleteProduct(Guid productId) => InternalDeleteProduct(productId).AsTask();
    public OneOf<Success, NotFound> InternalDeleteProduct(Guid productId) {
        return _db.ProductsTable.Remove(productId) ? new Success() : new NotFound();
    }

    public Task<OneOf<Success, NotFound>> EditProduct(Guid productId, ProductModel updatedValues) => InternalEditProduct(productId, updatedValues).AsTask();
    private OneOf<Success, NotFound> InternalEditProduct(Guid productId, ProductModel updatedValues) {
        
        if(!_db.ProductsTable.TryGetValue(productId, out var product)) {
            return new NotFound();
        }

        product.Title = updatedValues.Title;
        product.Description = updatedValues.Description;
        product.ImageUrl = updatedValues.ImageUrl;
        product.Price = updatedValues.Price;
        return new Success();

    }

    public Task<OneOf<ProductModel, NotFound>> GetProduct(Guid productId) => InternalGetProduct(productId).AsTask();
    private OneOf<ProductModel, NotFound> InternalGetProduct(Guid productId) {

        if(!_db.ProductsTable.TryGetValue(productId, out var value)) {
            return new NotFound();
        }

        return value;

    }

    public IQueryable<ProductModel> QueryAllProducts() {
        return _db.ProductsTable.Values.AsQueryable();   
    }

    public IQueryable<ProductModel> QueryProductsBySellerId(Guid sellerId) {
        return _db.ProductsTable.Values.Where(x => x.IdSellerAccount == sellerId).AsQueryable();        
    }

}
