using MediatR;
using OneOf;
using OneOf.Types;
using SuperSold.Data.DBInteractions;

namespace SuperSold.UI.AspDotNet.Handlers.Wishlist.Commands;

public record AddToWishlistCommand(Guid AccountId, Guid ProductId) : IRequest<OneOf<Success, Error>>;

public class AddToWishlistHandler : IRequestHandler<AddToWishlistCommand, OneOf<Success, Error>> {

    private readonly IWishlistHandler _wishlistHandler;

    public AddToWishlistHandler(IWishlistHandler wishlistHandler) {
        _wishlistHandler = wishlistHandler;
    }

    public async Task<OneOf<Success, Error>> Handle(AddToWishlistCommand request, CancellationToken cancellationToken) {
        return await _wishlistHandler.WishlistProduct(request.AccountId, request.ProductId);
    }
}