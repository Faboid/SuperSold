﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class CartController : Controller {

    private readonly ICartHandler _cartHandler;
    private readonly IMapper _mapper;

    public CartController(ICartHandler cartHandler, IMapper mapper) {
        _cartHandler = cartHandler;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public async Task<IActionResult> IndexPartial(int page = 0) {

        var userId = User.GetIdentity();
        var products = await _cartHandler.QueryCartedProductsByUserId(userId)
            .SkipToPage(page, 3)
            .ProjectTo<ProductWithSavedRelationship>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        return this.RelationshipListPartialView(PartialViewNames.MyCartRow, products);
    }

    [HttpGet]
    public IActionResult Buy() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _cartHandler.AddToCart(userId, productId);

        return result.Match<IActionResult>(
            success => StatusCode(StatusCodes.Status201Created),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );
    }

    [HttpPost]
    public async Task<IActionResult> MoveToWishlist(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _cartHandler.MoveToWishlist(userId, productId);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _cartHandler.RemoveFromCart(userId, productId);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

}
