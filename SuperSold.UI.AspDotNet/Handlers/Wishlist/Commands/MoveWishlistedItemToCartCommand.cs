using MediatR;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.Wishlist.Commands;

public record MoveWishlistedItemToCartCommand(Guid AccountId, Guid ProductId) : IRequest<OneOf<Success, NotFound>>;

public class MoveWishlistedItemToCartHandler : IRequestHandler<MoveWishlistedItemToCartCommand, OneOf<Success, NotFound>> {

    private readonly IWishlistHandler _wishlistHandler;

    public MoveWishlistedItemToCartHandler(IWishlistHandler wishlistHandler) {
        _wishlistHandler = wishlistHandler;
    }

    public async Task<OneOf<Success, NotFound>> Handle(MoveWishlistedItemToCartCommand request, CancellationToken cancellationToken) {
        return await _wishlistHandler.MoveToCart(request.AccountId, request.ProductId);
    }
}
