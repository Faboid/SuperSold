using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;
public interface IProductsHandler {

    IQueryable<ProductModel> QueryAllProducts();
    IQueryable<ProductModel> QueryProductsBySellerUserName(string userName);
    Task<bool> EditProduct(Guid productId, ProductModel updatedValues);
    Task<bool> DeleteProduct(Guid productId);
    Task<bool> CreateProduct(ProductModel product, string sellerUserName);

}
