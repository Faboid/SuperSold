using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.DBInteractions;
public interface IWishlistHandler {

    IQueryable<ProductModel> QueryWishlistedProductsByUserId(Guid userId);
    Task<OneOf<Success, AlreadyExists>> WishlistProduct(Guid userId, Guid productId);
    Task<OneOf<Success, NotFound>> RemoveWishlistProduct(Guid userId, Guid productId);
    Task<OneOf<Success, NotFound>> MoveToCart(Guid userId, Guid productId);

}