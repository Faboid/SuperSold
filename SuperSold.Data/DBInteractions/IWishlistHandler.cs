using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;

namespace SuperSold.Data.DBInteractions;
public interface IWishlistHandler {

    IQueryable<SavedRelationshipWithProduct> QueryWishlistedProductsByUserId(Guid userId);
    Task<OneOf<Success, Error>> WishlistProduct(Guid userId, Guid productId);
    Task<OneOf<Success, NotFound>> RemoveWishlistProduct(Guid userId, Guid productId);
    Task<OneOf<Success, NotFound>> MoveToCart(Guid userId, Guid productId);

}