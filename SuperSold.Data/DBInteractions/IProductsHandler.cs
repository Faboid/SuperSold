using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.DBInteractions;
public interface IProductsHandler {

    IQueryable<ProductModel> QueryAllProducts();
    IQueryable<ProductModel> QueryProductsBySellerUserName(string userName);
    Task<OneOf<Success, NotFound>> EditProduct(Guid productId, ProductModel updatedValues);
    Task<OneOf<Success, NotFound>> DeleteProduct(Guid productId);
    Task<OneOf<Success, AlreadyExists>> CreateProduct(ProductModel product, string sellerUserName);

}
