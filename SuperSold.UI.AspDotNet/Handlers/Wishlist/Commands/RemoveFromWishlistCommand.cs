using MediatR;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.Wishlist.Commands;

public record RemoveFromWishlistCommand(Guid AccountId, Guid ProductId) : IRequest<OneOf<Success, NotFound>>;

public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlistCommand, OneOf<Success, NotFound>> {

    private readonly IWishlistHandler _wishlistHandler;

    public RemoveFromWishlistHandler(IWishlistHandler wishlistHandler) {
        _wishlistHandler = wishlistHandler;
    }

    public async Task<OneOf<Success, NotFound>> Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken) {
        return await _wishlistHandler.RemoveWishlistProduct(request.AccountId, request.ProductId);
    }
}