using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class PurchaseController : Controller {

    private readonly IProductsHandler _productsHandler;
    private readonly ICartHandler _cartHandler;
    private readonly IMapper _mapper;

    public PurchaseController(ICartHandler cartHandler, IMapper mapper, IProductsHandler productsHandler) {
        _cartHandler = cartHandler;
        _mapper = mapper;
        _productsHandler = productsHandler;
    }

    public async Task<IActionResult> Index() {

        var userId = User.GetIdentity();
        var list = await _cartHandler
            .QueryCartedProductsByUserId(userId)
            .ProjectTo<ProductWithSavedRelationship>(_mapper.ConfigurationProvider)
            .ToListAsyncSafe();

        return View(list);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitPurchase(PurchaseInfoModel info, decimal price, string products) {

        if(!info.WarningCheckbox) {
            return BadRequest("Must tick the disclaimer checkbox.");
        }

        var objProducts = products
            .Split(',')
            .Select(ProductWithSavedRelationship.ParseBasicString)
            .ToList();

        var oneofProducts = await objProducts
            .Select(async x => await _productsHandler.GetProduct(x.guid))
            .ToListAsync();

        if(oneofProducts.Any(x => x.IsT1)) {
            return StatusCode(412, "One or more of the products in the cart is not on sale anymore.");
        }

        var dbProducts = oneofProducts.Select(x => x.AsT0).ToList();
        var updatedPrice = dbProducts.Select(x => objProducts.Single(y => x.IdProduct == y.guid).guantity * x.Price).Sum();

        if(price != updatedPrice) {
            return StatusCode(412, "The price has changed for one or more items. Please refresh the page.");
        }

        return Accepted(value: "The purchase request has been received, but it is only simulated, so the card hasn't been billed.");

    }

}
