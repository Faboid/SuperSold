using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Handlers.Wishlist.Commands;
using SuperSold.UI.AspDotNet.Handlers.Wishlist.Queries;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
public class WishlistController : Controller {

    private readonly IMediator _mediator;

    public WishlistController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> IndexPartial(int page = 0) {

        var userId = User.GetIdentity();
        var query = new GetWishlistedProducts(userId, page, 3);
        var products = await _mediator.Send(query);

        return this.RelationshipListPartialView(PartialViewNames.WishlistRow, products);
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(Guid productId) {

        var userId = User.GetIdentity();
        var command = new AddToWishlistCommand(userId, productId);
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            success => StatusCode(StatusCodes.Status201Created),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );

    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid productId) {

        var userId = User.GetIdentity();
        var command = new RemoveFromWishlistCommand(userId, productId);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

    [HttpPost]
    public async Task<IActionResult> MoveToCart(Guid productId) {

        var userId = User.GetIdentity();
        var command = new MoveWishlistedItemToCartCommand(userId, productId);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

}
