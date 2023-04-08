using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
public class WishlistController : Controller {

    private readonly IWishlistHandler _wishlistHandler;
    private readonly IMapper _mapper;

    public WishlistController(IWishlistHandler wishlistHandler, IMapper mapper) {
        _wishlistHandler = wishlistHandler;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> IndexPartial(int page = 0) {

        var userId = User.GetIdentity();
        var products = await _wishlistHandler.QueryWishlistedProductsByUserId(userId)
            .SkipToPage(page, 3)
            .ProjectTo<ProductWithSavedRelationship>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        return this.RelationshipListPartialView(PartialViewNames.WishlistRow, products);
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _wishlistHandler.WishlistProduct(userId, productId);

        return result.Match<IActionResult>(
            success => StatusCode(StatusCodes.Status201Created),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );

    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _wishlistHandler.RemoveWishlistProduct(userId, productId);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

    [HttpPost]
    public async Task<IActionResult> MoveToCart(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _wishlistHandler.MoveToCart(userId, productId);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

}
