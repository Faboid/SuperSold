using OneOf;
using OneOf.Types;
using SuperSold.Data.Models;
using SuperSold.Data.Models.ResponseTypes;

namespace SuperSold.Data.DBInteractions;

public interface ICartHandler {

    IQueryable<ProductModel> QueryCartedProductsByUserId(Guid userId);
    Task<OneOf<Success, Error>> AddToCart(Guid userId, Guid productId);
    Task<OneOf<Success, NotFound>> RemoveFromCart(Guid userId, Guid productId);
    Task<OneOf<Success, NotFound>> MoveToWishlist(Guid userId, Guid productId);

}