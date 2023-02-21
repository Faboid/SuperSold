using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;
internal interface IProductsHandler {

    Task<IQueryable<ProductModel>> QueryAllProducts();
    Task<IQueryable<ProductModel>> QueryProductsBySellerUserName(string userName);
    Task<bool> EditProduct(Guid productId, ProductModel updatedValues);
    Task<bool> DeleteProduct(Guid productId);
    Task<bool> CreateProduct(ProductModel product, string sellerUserName);

}
